using System.Text.RegularExpressions;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

namespace AnyRecordExporter;
using AnyRecordData.DataTypes;

public static partial class Exporter
{
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
            IDeserializer deserializer = new Deserializer();
            Config c = deserializer.Deserialize<Config>(config);
            
            // Edit program variables
            _pluginName = c.Plugin;
            _parentFolder = c.Path;
            _ignoreBookText = c.IgnoreBookText;
        }
        catch(Exception)
        {
            Console.Error.WriteLine($"ERR: Invalid or nonexistent config file: {configFilePath}");
            WaitToExit();
            return;
        }

        _patchFolder = _parentFolder + Path.DirectorySeparatorChar + StripPluginExtension().Replace(_pluginName, "");
        
        GetChanges(GameEnvironment.Typical.Skyrim(SkyrimRelease.SkyrimSE));
    }
    
    private static void GetChanges(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env)
    {
        if (!CheckForMod(env, _pluginName))
        {
            Console.WriteLine($"ERR: Mod: {_pluginName} not found or not enabled.");
            WaitToExit();
            return;
        }
        
        Console.WriteLine($"INFO: Creating config for mod: {_pluginName}");
        
        if (_ignoreBookText)
            Console.WriteLine("INFO: Saving of Book Text to config file disabled in config file");

        PatchChanges<DataAmmo, IAmmunitionGetter>(env); 
        PatchChanges<DataArmor, IArmorGetter>(env);
        PatchChanges<DataBook, IBookGetter>(env);
        PatchChanges<DataCell, ICellGetter>(env);
        PatchChanges<DataIngestible, IIngestibleGetter>(env);
        PatchChanges<DataIngredient, IIngredientGetter>(env);
        PatchChanges<DataLight, ILightGetter>(env);
        PatchChanges<DataMisc, IMiscItemGetter>(env);
        PatchChanges<DataPerk, IPerkGetter>(env);
        PatchChanges<DataScroll, IScrollGetter>(env);
        PatchChanges<DataSoulGem, ISoulGemGetter>(env);
        PatchChanges<DataShout, IShoutGetter>(env);
        PatchChanges<DataSpell, ISpellGetter>(env);
        PatchChanges<DataWeapon, IWeaponGetter>(env);
        
        Console.WriteLine("INFO: Done!");
        Thread.Sleep(500);
    }

    // Go through the list of comparable records and serialize the modifications to disk.
    private static void PatchChanges<TData, TMajorGetter>(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env)
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

        // Get override version of record we care about
        var targetMod = env.LoadOrder[_pluginName];
        if (targetMod.Mod is null)
            return compares;

        var newRefs = targetMod.Mod.EnumerateMajorRecords<TMajorGetter>().ToDictionary(newRef => newRef.FormKey);

        bool reached = false;
        foreach (var prevMod in env.LoadOrder.PriorityOrder)
        {
            if (prevMod.FileName.Equals(_pluginName))
            {
                reached = true;
                continue;
            }

            if (!reached || prevMod.Mod is null) 
                continue;

            var modRecords = typeof(TMajorGetter) == typeof(ICellGetter) 
                ? prevMod.Mod.Cells.EnumerateMajorRecords<TMajorGetter>()
                : prevMod.Mod.EnumerateMajorRecords<TMajorGetter>();

            foreach (TMajorGetter record in modRecords)
            {
                if (newRefs.TryGetValue(record.FormKey, out TMajorGetter? @ref))
                {
                    compares.Push((@ref, record));
                }
            }
        }
        
        return compares;
    }

    private static bool CheckForMod(IGameEnvironment<ISkyrimMod, ISkyrimModGetter> env, string modName)
    {
        return env.LoadOrder.ListedOrder.Any(mod => mod.FileName.Equals(modName));
    }

    private static void WaitToExit()
    {
        Console.Error.WriteLine("INFO: Press any key to exit...");
        Console.ReadKey();
    }

    [GeneratedRegex("\\.es(p|m)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex StripPluginExtension();
}