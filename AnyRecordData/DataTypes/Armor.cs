using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataArmor : BaseItem, IHasName, IHasKeywords, IHasObjectBounds, IHasWeightValue, IHasPickUpPutDownSound, IHasEquipmentType, IHasEnchantInfo, IHasDescription
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }

    public string? EquipmentType { get; set; }
    public bool? EquipmentTypeDeleted { get; set; }

    public string? ObjectEffect { get; set; }
    public bool? ObjectEffectDeleted { get; set; }
    public ushort? EnchantmentAmount { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }
    
    // Armor Specific
    [YamlMember] public float? ArmorRating { get; set; }
    [YamlMember] public string? BashImpact { get; set; }
    [YamlMember] public string? AlternativeBlock { get; set; }
    [YamlMember] public uint? ArmorType { get; set; }
    [YamlMember] public uint? FirstPersonFlags { get; set; }
    [YamlMember] public string? Race { get; set; }
    [YamlMember] public string? TemplateArmor { get; set;}
    [YamlMember] public string[]? Armatures { get; set; }
    [YamlMember] public string? MaleModelPath { get; set; }
    [YamlMember] public AltTexSet[]? MaleModelTextures { get; set; }
    [YamlMember] public string? FemaleModelPath { get; set; }
    [YamlMember] public AltTexSet[]? FemaleModelTextures { get; set; }
    [YamlMember] public string? MaleIconLarge { get; set; }
    [YamlMember] public string? MaleIconSmall { get; set; }
    [YamlMember] public string? FemaleIconLarge { get; set; }
    [YamlMember] public string? FemaleIconSmall { get; set; }

    public DataArmor()
    {
        PatchFileName = "Armor";
    }
    
    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IArmorGetter x && oldRef is IArmorGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IArmorGetter newRef, IArmorGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEquipmentType)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEnchantInfo)this).SaveChangesInterface(newRef, oldRef);
        ((IHasDescription)this).SaveChangesInterface(newRef, oldRef);
        
        if (!Utility.AreEqual(newRef.ArmorRating, oldRef.ArmorRating))
        {
            ArmorRating = newRef.ArmorRating;
        }

        if (oldRef.BodyTemplate is null)
        {
            if (newRef.BodyTemplate is not null)
            {
                ArmorType = (uint)newRef.BodyTemplate.ArmorType;
                FirstPersonFlags = (uint)newRef.BodyTemplate.FirstPersonFlags;
            }
        }
        else
        {
            if (newRef.BodyTemplate is not null)
            {
                if (newRef.BodyTemplate.ArmorType != oldRef.BodyTemplate.ArmorType)
                {
                    ArmorType = (uint)newRef.BodyTemplate.ArmorType;
                }

                if (newRef.BodyTemplate.FirstPersonFlags.Equals(oldRef.BodyTemplate.FirstPersonFlags))
                {
                    FirstPersonFlags = (uint)newRef.BodyTemplate.FirstPersonFlags;
                }
            }
        }

        if (!oldRef.BashImpactDataSet.FormKey.ToString().Equals(newRef.BashImpactDataSet.FormKey.ToString()))
        {
            BashImpact = newRef.BashImpactDataSet.FormKey.ToString();
        }

        if (!oldRef.AlternateBlockMaterial.FormKey.ToString().Equals(newRef.AlternateBlockMaterial.FormKey.ToString()))
        {
            AlternativeBlock = newRef.AlternateBlockMaterial.FormKey.ToString();
        }

        if (oldRef.Race.FormKey != newRef.Race.FormKey)
        {
            Race = newRef.Race.FormKey.ToString();
        }
        
        if (newRef.TemplateArmor.FormKey != oldRef.TemplateArmor.FormKey)
        {
            TemplateArmor = newRef.TemplateArmor.FormKey.ToString();
        }

        var newSet = Utility.ToStringSet(newRef.Armature);
        var oldSet = Utility.ToStringSet(oldRef.Armature);
        if (!newSet.SetEquals(oldSet))
        {
            Armatures = newSet.ToArray();
        }

        // Models
        string newMaleModelPath = newRef.WorldModel?.Male?.Model?.File.RawPath ?? "";
        string oldMaleModelPath = newRef.WorldModel?.Male?.Model?.File.RawPath ?? "";
        if (newMaleModelPath != oldMaleModelPath)
        {
            MaleModelPath = newMaleModelPath;
        }
        
        var newMaleList = newRef.WorldModel?.Male?.Model?.AlternateTextures;
        var oldMaleList = oldRef.WorldModel?.Male?.Model?.AlternateTextures;
        MaleModelTextures = CompareTextureLists(newMaleList, oldMaleList);

        string newFemaleModelPath = newRef.WorldModel?.Female?.Model?.File.RawPath ?? "";
        string oldFemaleModelPath = newRef.WorldModel?.Female?.Model?.File.RawPath ?? "";
        if (newFemaleModelPath != oldFemaleModelPath)
        {
            FemaleModelPath = newFemaleModelPath;
        }
        
        var newFemaleList = newRef.WorldModel?.Female?.Model?.AlternateTextures;
        var oldFemaleList = oldRef.WorldModel?.Female?.Model?.AlternateTextures;
        FemaleModelTextures = CompareTextureLists(newFemaleList, oldFemaleList);

        // Icons
        string? newMaleIconLarge = newRef.WorldModel?.Male?.Icons?.LargeIconFilename.ToString();
        string? oldMaleIconLarge = oldRef.WorldModel?.Male?.Icons?.LargeIconFilename.ToString();
        if (newMaleIconLarge is not null && !newMaleIconLarge.Equals(oldMaleIconLarge))
        {
            MaleIconLarge = newMaleIconLarge;
        }
        
        string? newMaleIconSmall = newRef.WorldModel?.Male?.Icons?.SmallIconFilename?.ToString();
        string? oldMaleIconSmall = oldRef.WorldModel?.Male?.Icons?.SmallIconFilename?.ToString();
        if (newMaleIconSmall is not null && !newMaleIconSmall.Equals(oldMaleIconSmall))
        {
            MaleIconSmall = newMaleIconSmall;
        }
        
        string? newFemaleIconLarge = newRef.WorldModel?.Female?.Icons?.LargeIconFilename.ToString();
        string? oldFemaleIconLarge = oldRef.WorldModel?.Female?.Icons?.LargeIconFilename.ToString();
        if (newFemaleIconLarge is not null && !newFemaleIconLarge.Equals(oldFemaleIconLarge))
        {
            FemaleIconLarge = newFemaleIconLarge;
        }
        
        string? newFemaleIconSmall = newRef.WorldModel?.Female?.Icons?.SmallIconFilename?.ToString();
        string? oldFemaleIconSmall = oldRef.WorldModel?.Female?.Icons?.SmallIconFilename?.ToString();
        if (newFemaleIconSmall is not null && !newFemaleIconSmall.Equals(oldFemaleIconSmall))
        {
            FemaleIconSmall = newFemaleIconSmall;
        }
    }

    private static AltTexSet[]? CompareTextureLists(IReadOnlyList<IAlternateTextureGetter>? newList, IReadOnlyList<IAlternateTextureGetter>? oldList)
    {
        if (newList is null)
        {
            return oldList is null 
                ? null 
                : Array.Empty<AltTexSet>();
        }

        var newListTexArr = ReadAltTextures(newList);
        if (oldList is null)
        {
            return newListTexArr;
        }
            
        var oldListTexArr = ReadAltTextures(oldList).ToHashSet();
        return oldListTexArr.SetEquals(newListTexArr) 
            ? null 
            : newListTexArr;
    }

    private static AltTexSet[] ReadAltTextures(IReadOnlyList<IAlternateTextureGetter> list)
    {
        AltTexSet[] arr = new AltTexSet[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            arr[i] = new AltTexSet(list[i].Name, list[i].NewTexture.FormKey.ToString(), list[i].Index);
        }

        return arr;
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IArmor recArmor)
            Patch(recArmor);
    }

    public void Patch(IArmor rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasEquipmentType)this).PatchInterface(rec);
        ((IHasEnchantInfo)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);
        
        if (ArmorRating is not null)
            rec.ArmorRating = ArmorRating ?? 1.0f;
        
        if (BashImpact is not null)
        {
            rec.BashImpactDataSet =
                new FormLinkNullable<IImpactDataSetGetter>(FormKey.Factory(BashImpact));
        }
        
        if (AlternativeBlock is not null)
        {
            rec.AlternateBlockMaterial =
                new FormLinkNullable<IMaterialTypeGetter>(FormKey.Factory(AlternativeBlock));
        }

        if (ArmorType is not null)
        {
            rec.BodyTemplate ??= new BodyTemplate();
            rec.BodyTemplate.ArmorType = (ArmorType)ArmorType;
        }

        if (FirstPersonFlags is not null)
        {
            rec.BodyTemplate ??= new BodyTemplate();
            rec.BodyTemplate.FirstPersonFlags = (BipedObjectFlag)FirstPersonFlags;
        }

        if (Race is not null)
            rec.Race = new FormLinkNullable<IRaceGetter>(FormKey.Factory(Race));

        if (TemplateArmor is not null)
            rec.TemplateArmor = new FormLinkNullable<IArmorGetter>(FormKey.Factory(TemplateArmor));

        if (Armatures is not null)
        {
            rec.Armature.Clear();
            foreach (string arm in Armatures)
            {
                rec.Armature.Add(new FormLink<ArmorAddon>(FormKey.Factory(arm)));
            }
        }
        
        if (MaleModelPath is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();

            rec.WorldModel.Male.Model.File.RawPath = MaleModelPath;
        }
        
        if (MaleModelTextures is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            rec.WorldModel.Male.Model.AlternateTextures ??= new ExtendedList<AlternateTexture>();
            rec.WorldModel.Male.Model.AlternateTextures.Clear();

            foreach (AltTexSet t in MaleModelTextures)
            {
                rec.WorldModel.Male.Model.AlternateTextures.Add(new AlternateTexture
                {
                    Name = t.Name,
                    NewTexture = new FormLink<ITextureSetGetter>(t.Texture.ToFormKey()),
                    Index = t.Index
                });
            }
        }

        if (MaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            rec.WorldModel.Male.Icons ??= new();
            rec.WorldModel.Male.Icons.LargeIconFilename = MaleIconLarge;
        }

        if (MaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            rec.WorldModel.Male.Icons ??= new();
            rec.WorldModel.Male.Icons.SmallIconFilename = MaleIconSmall;
        }
        
        if (FemaleModelPath is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();

            rec.WorldModel.Female.Model.File.RawPath = FemaleModelPath;
        }
        
        if (FemaleModelTextures is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            rec.WorldModel.Female.Model.AlternateTextures ??= new();
            rec.WorldModel.Female.Model.AlternateTextures.Clear();

            foreach (AltTexSet t in FemaleModelTextures)
            {
                rec.WorldModel.Female.Model.AlternateTextures.Add(new AlternateTexture
                {
                    Name = t.Name,
                    NewTexture = new FormLink<ITextureSetGetter>(t.Texture.ToFormKey()),
                    Index = t.Index
                });
            }
        }

        if (FemaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            rec.WorldModel.Female.Icons ??= new();
            rec.WorldModel.Female.Icons.LargeIconFilename = FemaleIconLarge;
        }

        if (FemaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            rec.WorldModel.Female.Icons ??= new();
            rec.WorldModel.Female.Icons.SmallIconFilename = FemaleIconSmall;
        }
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               ((IHasEquipmentType)this).IsModifiedInterface() ||
               ((IHasEnchantInfo)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               ArmorRating is not null ||
               BashImpact is not null ||
               AlternativeBlock is not null ||
               ArmorType is not null ||
               FirstPersonFlags is not null ||
               Race is not null ||
               Armatures is not null ||
               TemplateArmor is not null ||
               MaleModelPath is not null ||
               MaleModelTextures is not null ||
               FemaleModelPath is not null ||
               FemaleModelTextures is not null ||
               MaleIconLarge is not null ||
               MaleIconSmall is not null ||
               FemaleIconLarge is not null ||
               FemaleIconSmall is not null;
    }
}