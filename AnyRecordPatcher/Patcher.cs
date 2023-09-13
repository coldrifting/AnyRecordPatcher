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
    private static string _patchDataPath = @"C:\Users\coldrifting\Desktop";
    private const string PatchIdentifier = "AnyRecordPatcher.esp";
    private static bool _errors;

    private static readonly Dictionary<FormKey, DataAmmo> Ammo = new();
    private static readonly Dictionary<FormKey, DataArmor> Armors = new();
    private static readonly Dictionary<FormKey, DataBook> Books = new();
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

        if (_errors)
        {
            Console.WriteLine("1 or more records could not be patched because there were missing identifiers.");
            Console.WriteLine("Please check your patch files...");
        }
    }

    // Reads a patch file contents, serializes the yaml data into objects,
    // and adds those objects into a dictionary of the specified type
    private static void ReadPatchFile<T>(string patchDir, IDictionary<FormKey, T> dictionary)
    where T : BaseItem, new()
    {
        string patchFileFullPath = patchDir + "\\" + new T().PatchFileName + ".yaml";
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
            
            using StreamReader input = new(yaml.Replace(": null", "Deleted: true").ToStream());
                        
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
        where T : BaseItem
    {
        foreach ((FormKey f, BaseItem data) in dict)
        {
            ISkyrimMajorRecord? xyz = null;
            
            switch (data)
            {
                case DataAmmo: 
                    if (state.LinkCache.TryResolve<IAmmunitionGetter>(f, out var ammoGetter))
                        xyz = state.PatchMod.Ammunitions.GetOrAddAsOverride(ammoGetter); 
                    break;
                case DataArmor: 
                    if (state.LinkCache.TryResolve<IArmorGetter>(f, out var armorGetter))
                        xyz = state.PatchMod.Armors.GetOrAddAsOverride(armorGetter); 
                    break;
                case DataBook: 
                    if (state.LinkCache.TryResolve<IBookGetter>(f, out var bookGetter))
                        xyz = state.PatchMod.Books.GetOrAddAsOverride(bookGetter); 
                    break;
                case DataIngestible: 
                    if (state.LinkCache.TryResolve<IIngestibleGetter>(f, out var ingestibleGetter))
                        xyz = state.PatchMod.Ingestibles.GetOrAddAsOverride(ingestibleGetter); 
                    break;
                case DataIngredient: 
                    if (state.LinkCache.TryResolve<IIngredientGetter>(f, out var ingredientGetter))
                        xyz = state.PatchMod.Ingredients.GetOrAddAsOverride(ingredientGetter); 
                    break;
                case DataLight: 
                    if (state.LinkCache.TryResolve<ILightGetter>(f, out var lightGetter))
                        xyz = state.PatchMod.Lights.GetOrAddAsOverride(lightGetter); 
                    break;
                case DataMisc: 
                    if (state.LinkCache.TryResolve<IMiscItemGetter>(f, out var miscGetter))
                        xyz = state.PatchMod.MiscItems.GetOrAddAsOverride(miscGetter); 
                    break;
                case DataPerk: 
                    if (state.LinkCache.TryResolve<IPerkGetter>(f, out var perkGetter))
                        xyz = state.PatchMod.Perks.GetOrAddAsOverride(perkGetter); 
                    break;
                case DataScroll: 
                    if (state.LinkCache.TryResolve<IScrollGetter>(f, out var scrollGetter))
                        xyz = state.PatchMod.Scrolls.GetOrAddAsOverride(scrollGetter); 
                    break;
                case DataSoulGem: 
                    if (state.LinkCache.TryResolve<ISoulGemGetter>(f, out var soulGemGetter))
                        xyz = state.PatchMod.SoulGems.GetOrAddAsOverride(soulGemGetter); 
                    break;
                case DataShout: 
                    if (state.LinkCache.TryResolve<IShoutGetter>(f, out var shoutGetter))
                        xyz = state.PatchMod.Shouts.GetOrAddAsOverride(shoutGetter); 
                    break;
                case DataSpell: 
                    if (state.LinkCache.TryResolve<ISpellGetter>(f, out var spellGetter))
                        xyz = state.PatchMod.Spells.GetOrAddAsOverride(spellGetter); 
                    break;
                case DataWeapon: 
                    if (state.LinkCache.TryResolve<IWeaponGetter>(f, out var weaponGetter))
                        xyz = state.PatchMod.Weapons.GetOrAddAsOverride(weaponGetter); 
                    break;
            }
            
            if (xyz is not null)
                data.Patch(xyz);
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