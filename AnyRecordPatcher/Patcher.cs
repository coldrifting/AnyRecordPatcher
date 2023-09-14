using System.Text.RegularExpressions;
using AnyRecordData;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Synthesis;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AnyRecordPatcher;
using AnyRecordData.DataTypes;

public static class Patcher
{
    private static string _patchDataPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private const string PatchIdentifier = "AnyRecordPatcher.esp";
    private static bool _errors;

    private static readonly Dictionary<FormKey, DataAmmo> Ammo = new();
    private static readonly Dictionary<FormKey, DataArmor> Armors = new();
    private static readonly Dictionary<FormKey, DataBook> Books = new();
    private static readonly Dictionary<FormKey, DataCell> Cells = new();
    private static readonly Dictionary<FormKey, DataIngestible> Ingestibles = new();
    private static readonly Dictionary<FormKey, DataIngredient> Ingredients = new();
    private static readonly Dictionary<FormKey, DataLight> Lights = new();
    private static readonly Dictionary<FormKey, DataMisc> Misc = new();
    private static readonly Dictionary<FormKey, DataPerk> Perks = new();
    private static readonly Dictionary<FormKey, DataScroll> Scrolls = new();
    private static readonly Dictionary<FormKey, DataSoulGem> SoulGems = new();
    private static readonly Dictionary<FormKey, DataShout> Shouts = new();
    private static readonly Dictionary<FormKey, DataSpell> Spells = new();
    private static readonly Dictionary<FormKey, DataWeapon> Weapons = new();
        
    public static async Task<int> Main(string[] args)
    {
        return await SynthesisPipeline.Instance
            .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
            .SetTypicalOpen(GameRelease.SkyrimSE, PatchIdentifier)
            .Run(args);
    }

    private static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
    {
        if (_patchDataPath is "")
        {
            if (state.ExtraSettingsDataPath is null)
                return;
            
            _patchDataPath = state.ExtraSettingsDataPath;
        }
        
        // Iterate over all patches
        string[] patchDirs = Directory.GetDirectories(_patchDataPath);
        foreach (string patchDir in patchDirs)
        {
            // Read in patch data
            Console.WriteLine($"Running Patch: {new DirectoryInfo(patchDir).Name}");

            // Read patch data
            ReadPatchFile(patchDir, Ammo);
            ReadPatchFile(patchDir, Armors);
            ReadPatchFile(patchDir, Books);
            ReadPatchFile(patchDir, Cells);
            ReadPatchFile(patchDir, Ingestibles);
            ReadPatchFile(patchDir, Ingredients);
            ReadPatchFile(patchDir, Lights);
            ReadPatchFile(patchDir, Misc);
            ReadPatchFile(patchDir, Perks);
            ReadPatchFile(patchDir, Scrolls);
            ReadPatchFile(patchDir, Shouts);
            ReadPatchFile(patchDir, SoulGems);
            ReadPatchFile(patchDir, Spells);
            ReadPatchFile(patchDir, Weapons);

            // Patch records
            Patch(state, Ammo);
            Patch(state, Armors);
            Patch(state, Books);
            Patch(state, Cells);
            Patch(state, Ingestibles);
            Patch(state, Ingredients);
            Patch(state, Lights);
            Patch(state, Misc);
            Patch(state, Perks);
            Patch(state, Scrolls);
            Patch(state, Shouts);
            Patch(state, SoulGems);
            Patch(state, Spells);
            Patch(state, Weapons);
        }

        if (!_errors) 
            return;
        
        Console.WriteLine("1 or more records could not be patched because there were missing identifiers.");
        Console.WriteLine("Please check your patch files...");
    }

    // Reads a patch file contents, serializes the yaml data into objects,
    // and adds those objects into a dictionary of the specified type
    private static void ReadPatchFile<T>(string patchDir, IDictionary<FormKey, T> dictionary)
    where T : DataBaseItem, new()
    {
        string patchFileFullPath = patchDir + Path.DirectorySeparatorChar + new T().PatchFileName + ".yaml";
        if (!File.Exists(patchFileFullPath)) 
            return;
        
        // Split YAML objects
        const string pattern = @"(\r\n|\r|\n)---(\r\n|\r|\n)";
        string fileContents = File.ReadAllText(patchFileFullPath);
        string[] splitFileContents = Regex.Split(fileContents, pattern);

        foreach (string yaml in splitFileContents)
        {
            if (yaml.Trim().Length == 0)
                continue;
            
            using StreamReader input = new(yaml.ToStream());
                        
            IDeserializer deserializer = new DeserializerBuilder()
               .WithNamingConvention(PascalCaseNamingConvention.Instance)
               .Build();
            T item = deserializer.Deserialize<T>(input);

            if (item.Id is null)
            {
                _errors = true;
                continue;
            }

            dictionary.Add(item.Id.ToFormKey(), item);
        }
    }

    private static void Patch<T>(IPatcherState<ISkyrimMod, ISkyrimModGetter> state, Dictionary<FormKey, T> dict)
        where T : DataBaseItem
    {
        bool first = true;
        foreach ((FormKey f, DataBaseItem data) in dict)
        {
            ISkyrimMajorRecord? recOverride = data switch
            {
                DataAmmo => state.PatchMod.Ammunitions.GetOrAddAsOverride(
                    state.LinkCache.Resolve<IAmmunitionGetter>(f)),
                DataArmor => state.PatchMod.Armors.GetOrAddAsOverride(
                    state.LinkCache.Resolve<IArmorGetter>(f)),
                DataBook => state.PatchMod.Books.GetOrAddAsOverride(
                    state.LinkCache.Resolve<IBookGetter>(f)),
                DataCell => state.LinkCache.ResolveContext<ICell, ICellGetter>(f).GetOrAddAsOverride(state.PatchMod),
                DataIngestible => state.PatchMod.Ingestibles.GetOrAddAsOverride(
                    state.LinkCache.Resolve<IIngestibleGetter>(f)),
                DataIngredient => state.PatchMod.Ingredients.GetOrAddAsOverride(
                    state.LinkCache.Resolve<IIngredientGetter>(f)),
                DataLight => state.PatchMod.Lights.GetOrAddAsOverride(
                state.LinkCache.Resolve<ILightGetter>(f)),
                DataMisc => state.PatchMod.MiscItems.GetOrAddAsOverride(
                state.LinkCache.Resolve<IMiscItemGetter>(f)),
                DataPerk => state.PatchMod.Perks.GetOrAddAsOverride(
                state.LinkCache.Resolve<IPerkGetter>(f)),
                DataScroll => state.PatchMod.Scrolls.GetOrAddAsOverride(
                state.LinkCache.Resolve<IScrollGetter>(f)),
                DataSoulGem => state.PatchMod.SoulGems.GetOrAddAsOverride(
                state.LinkCache.Resolve<ISoulGemGetter>(f)),
                DataShout => state.PatchMod.Shouts.GetOrAddAsOverride(
                state.LinkCache.Resolve<IShoutGetter>(f)),
                DataSpell => state.PatchMod.Spells.GetOrAddAsOverride(
                state.LinkCache.Resolve<ISpellGetter>(f)),
                DataWeapon => state.PatchMod.Weapons.GetOrAddAsOverride(
                state.LinkCache.Resolve<IWeaponGetter>(f)),
                _ => null
            };
            
            if (first)
            {
                Console.WriteLine($"Patching {data.PatchFileName}...");
                first = false;
            }

            if (recOverride is not null)
                data.Patch(recOverride);
        }

        // Clear database for next patch
        dict.Clear();
    }

    private static Stream ToStream(this string str)
    {
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write(str);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}