using System.Text.RegularExpressions;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AnyRecordExporter;
using AnyRecordData.DataTypes;

public static partial class Exporter
{
    private static readonly HashSet<string> RequiredMods = new();
    private static readonly HashSet<string> DefaultMasters = new(StringComparer.OrdinalIgnoreCase)
    {
        "Skyrim.esm", 
        "Update.esm", 
        "Dawnguard.esm", 
        "HearthFires.esm", 
        "Dragonborn.esm", 
        "ccBGSSSE001-Fish.esm", 
        "ccQDRSSE001-SurvivalMode.esl", 
        "ccBGSSSE025-AdvDSGS.esm", 
        "ccBGSSSE037-Curios.esl"
    };
    
    private static string _pluginName = "Skyrim.esm";
    private static string _parentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static string _patchFolder = "";

    private static bool _ignoreBookText;
    
    private static readonly ISerializer Serializer = new SerializerBuilder()
        .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
        .Build();
    
    public static void Main(string[] args)
    {
        // Default settings file is located next to the application
        string configFilePath = AppDomain.CurrentDomain.BaseDirectory + "Settings.yaml";
        try
        {
            if (args.Length > 0)
            {
                configFilePath = Path.GetFullPath(args[0]);
            }
            // Read config
            string config = File.ReadAllText(configFilePath);
            IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
            Config c = deserializer.Deserialize<Config>(config);
            
            // Edit program variables
            _ignoreBookText = c.IgnoreBookText;
            _parentFolder = c.Path;
            _pluginName = c.Plugin;
        }
        catch
        {
            Console.Error.WriteLine($"ERR: Invalid or nonexistent config file: {configFilePath}");
            WaitToExit();
            return;
        }

        GetChanges(GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE));
    }
    
    private static void GetChanges(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env)
    {
        if (_pluginName.Equals("LastEnabled"))
        {
            foreach (var x in env.LoadOrder.PriorityOrder)
            {
                if (!x.Enabled) 
                    continue;
                
                _pluginName = x.FileName;
                break;
            }
        }
        
        _patchFolder = _parentFolder + Path.DirectorySeparatorChar + StripPluginExtension().Replace(_pluginName, "");
        
        if (!CheckForMod(env, _pluginName))
        {
            Console.WriteLine($"ERR: Mod: {_pluginName} not found or not enabled.");
            WaitToExit();
            return;
        }
        
        Console.WriteLine($"INFO: Creating config for mod: {_pluginName}");
        
        if (_ignoreBookText)
            Console.WriteLine("INFO: Saving of Book Text to config file disabled in config file");

        GetChanges<DataAmmo, IAmmunitionGetter>(env); 
        GetChanges<DataArmor, IArmorGetter>(env);
        GetChanges<DataBook, IBookGetter>(env);
        GetChanges<DataCell, ICellGetter>(env);
        GetChanges<DataIngestible, IIngestibleGetter>(env);
        GetChanges<DataIngredient, IIngredientGetter>(env);
        GetChanges<DataLight, ILightGetter>(env);
        GetChanges<DataMisc, IMiscItemGetter>(env);
        GetChanges<DataPerk, IPerkGetter>(env);
        GetChanges<DataScroll, IScrollGetter>(env);
        GetChanges<DataSoulGem, ISoulGemGetter>(env);
        GetChanges<DataShout, IShoutGetter>(env);
        GetChanges<DataSpell, ISpellGetter>(env);
        GetChanges<DataWeapon, IWeaponGetter>(env);
        
        if (RequiredMods.Count > 0)
            File.WriteAllText($@"{_patchFolder}\_Required.txt", string.Join("\r\n", RequiredMods) + "\r\n");
        
        Console.WriteLine("INFO: Done!");
        Thread.Sleep(500);
    }

    // Go through the list of comparable records and serialize the modifications to disk.
    private static void GetChanges<TData, TMajorGetter>(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env)
        where TMajorGetter : class, ISkyrimMajorRecordGetter
        where TData : DataBaseItem, new()
    {
        var compares = GetModifiedRecords<TMajorGetter>(env);
        
        if (compares.Count < 0)
            return;

        string type = "Default";
        
        List<string> strings = new();
        bool first = true;
        bool modifiedCategory = false;
        foreach ((TMajorGetter newRef, TMajorGetter oldRef) in compares)
        {
            // Reset shared object
            TData data = new()
            {
                Id = newRef.FormKey.ToString()
            };
            type = data.PatchFileName;

            if (first)
            {
                Console.WriteLine($"Exporting {data.PatchFileName}...");
                first = false;
            }
            
            data.GetData(newRef, oldRef);

            if (data is DataBook dataBook)
            {
                if (_ignoreBookText)
                    dataBook.BookText = null;
            }
            
            if (!data.IsModified())
                continue;
            
            modifiedCategory = true;

            string cereal = Serializer.Serialize(data);
            if (cereal.Trim().Length > 0)
            {
                strings.Add(cereal);
            }
        }

        foreach (string s in strings)
        {
            MatchCollection m = FormKeyRegex().Matches(s);
            foreach (Match mx in m)
            {
                string master = mx.Value[7..];
                if (!DefaultMasters.Contains(master))
                    RequiredMods.Add(master);
            }
        }

        // Write text to file, if at least one thing is modified
        if (!modifiedCategory) return;
        
        if (!Directory.Exists(_patchFolder))
            Directory.CreateDirectory(_patchFolder);
            
        File.WriteAllText($@"{_patchFolder}\{type}.yaml", string.Join($"---{Environment.NewLine}", strings));
    }
    
    private static Stack<(TMajorGetter, TMajorGetter)> GetModifiedRecords<TMajorGetter>(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env)
        where TMajorGetter : class, ISkyrimMajorRecordGetter
    {
        Stack<(TMajorGetter, TMajorGetter)> compares = new();
        HashSet<string> seen = new();

        // Get override version of record we care about
        var targetMod = env.LoadOrder[_pluginName];
        if (targetMod.Mod is null)
            return compares;

        var newRefs = targetMod.Mod.EnumerateMajorRecords<TMajorGetter>().ToDictionary(newRef => newRef.FormKey);

        bool reached = false;
        foreach (var prevMod in env.LoadOrder.PriorityOrder.OnlyEnabled())
        {
            if (prevMod.FileName.Equals(_pluginName))
            {
                reached = true;
                continue;
            }

            if (!reached || prevMod.Mod is null) 
                continue;

            var modRecords = prevMod.Mod.EnumerateMajorRecords<TMajorGetter>();

            foreach (TMajorGetter record in modRecords)
            {
                string formKey = record.FormKey.ToString();
                if (!newRefs.TryGetValue(record.FormKey, out TMajorGetter? @ref) || seen.Contains(formKey)) 
                    continue;
                
                compares.Push((@ref, record));
                seen.Add(formKey);
            }
        }
        
        return compares;
    }

    private static void WaitToExit()
    {
        Console.Error.WriteLine("INFO: Press any key to exit...");
        Console.ReadKey();
    }
    
    private static bool CheckForMod(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env, string modName)
    {
        return env.LoadOrder.ListedOrder.Any(mod => mod.FileName.Equals(modName));
    }

    [GeneratedRegex("\\.es(p|m)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex StripPluginExtension();
    
    [GeneratedRegex("([a-f0-9]{6}:.*?\\.es[mpl])", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex FormKeyRegex();
}