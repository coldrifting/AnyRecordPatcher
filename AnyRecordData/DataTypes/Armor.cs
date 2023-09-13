using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataArmor : BaseItem,
                         IHasName,
                         IHasDescription,
                         IHasKeywords,
                         IHasObjectBounds,
                         IHasWeightValue,
                         IHasPickUpPutDownSound,
                         IHasEquipmentType,
                         IHasEnchantInfo,
                         IHasFlags
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }
    
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
    
    [YamlMember] public string[]? Flags { get; set; }
    
    // Armor Specific
    [YamlMember] public float? ArmorRating { get; set; }
    [YamlMember] public string? BashImpact { get; set; }
    [YamlMember] public string? AlternativeBlock { get; set; }
    [YamlMember] public string? ArmorType { get; set; }
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
        ((IHasFlags)this).SaveChangesInterface(newRef, oldRef);
        
        ArmorRating = Utility.GetChangesNumber(newRef.ArmorRating, oldRef.ArmorRating);
        BashImpact = Utility.GetChangesFormKey(newRef.BashImpactDataSet, oldRef.BashImpactDataSet);
        AlternativeBlock = Utility.GetChangesFormKey(newRef.AlternateBlockMaterial, oldRef.AlternateBlockMaterial);
        TemplateArmor = Utility.GetChangesFormKey(newRef.TemplateArmor, oldRef.TemplateArmor);
        Race = Utility.GetChangesFormKey(newRef.Race, oldRef.Race);
        ArmorType = Utility.GetChangesString(newRef.BodyTemplate?.ArmorType, oldRef.BodyTemplate?.ArmorType);
        Armatures = Utility.GetChangesSet(Utility.ToStringSet(newRef.Armature), Utility.ToStringSet(oldRef.Armature));

        // Models
        MaleModelPath = Utility.GetChangesString(
            newRef.WorldModel?.Male?.Model?.File.RawPath,
            newRef.WorldModel?.Male?.Model?.File.RawPath);
        FemaleModelPath = Utility.GetChangesString(
            newRef.WorldModel?.Female?.Model?.File.RawPath,
            newRef.WorldModel?.Female?.Model?.File.RawPath);
        
        var newMaleList = newRef.WorldModel?.Male?.Model?.AlternateTextures;
        var oldMaleList = oldRef.WorldModel?.Male?.Model?.AlternateTextures;
        MaleModelTextures = AltTexSet.CompareTextureLists(newMaleList, oldMaleList);

        var newFemaleList = newRef.WorldModel?.Female?.Model?.AlternateTextures;
        var oldFemaleList = oldRef.WorldModel?.Female?.Model?.AlternateTextures;
        FemaleModelTextures = AltTexSet.CompareTextureLists(newFemaleList, oldFemaleList);

        // Icons
        MaleIconLarge = Utility.GetChangesString(newRef.WorldModel?.Male?.Icons?.LargeIconFilename, oldRef.WorldModel?.Male?.Icons?.LargeIconFilename);
        MaleIconSmall = Utility.GetChangesString(newRef.WorldModel?.Male?.Icons?.SmallIconFilename, oldRef.WorldModel?.Male?.Icons?.SmallIconFilename);
        FemaleIconLarge = Utility.GetChangesString(newRef.WorldModel?.Female?.Icons?.LargeIconFilename, oldRef.WorldModel?.Female?.Icons?.LargeIconFilename);
        FemaleIconSmall = Utility.GetChangesString(newRef.WorldModel?.Female?.Icons?.SmallIconFilename, oldRef.WorldModel?.Female?.Icons?.SmallIconFilename);
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
        ((IHasFlags)this).PatchInterface(rec);
        
        rec.ArmorRating = ArmorRating ?? rec.ArmorRating;
        
        if (BashImpact is not null)
            rec.BashImpactDataSet.SetTo(BashImpact.ToFormKey());
        
        if (AlternativeBlock is not null)
            rec.AlternateBlockMaterial.SetTo(AlternativeBlock.ToFormKey());

        if (ArmorType is not null)
        {
            rec.BodyTemplate ??= new BodyTemplate();
            rec.BodyTemplate.ArmorType = Enum.Parse<ArmorType>(ArmorType);
        }

        if (Race is not null)
            rec.Race.SetTo(Race.ToFormKey());

        if (TemplateArmor is not null)
            rec.TemplateArmor.SetTo(TemplateArmor.ToFormKey());

        if (Armatures is not null)
        {
            rec.Armature.Clear();
            foreach (string arm in Armatures)
            {
                rec.Armature.Add(new FormLink<ArmorAddon>(arm.ToFormKey()));
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
                rec.WorldModel.Male.Model.AlternateTextures.Add(t.ToPatch());
            }
        }

        if (MaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            rec.WorldModel.Male.Icons ??= new Icons();
            rec.WorldModel.Male.Icons.LargeIconFilename = MaleIconLarge;
        }

        if (MaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            rec.WorldModel.Male.Icons ??= new Icons();
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
            rec.WorldModel.Female.Model.AlternateTextures ??= new ExtendedList<AlternateTexture>();
            rec.WorldModel.Female.Model.AlternateTextures.Clear();

            foreach (AltTexSet t in FemaleModelTextures)
            {
                rec.WorldModel.Female.Model.AlternateTextures.Add(t.ToPatch());
            }
        }

        if (FemaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            rec.WorldModel.Female.Icons ??= new Icons();
            rec.WorldModel.Female.Icons.LargeIconFilename = FemaleIconLarge;
        }

        if (FemaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            rec.WorldModel.Female.Icons ??= new Icons();
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
               ((IHasFlags)this).IsModifiedInterface() ||
               ArmorRating is not null ||
               BashImpact is not null ||
               AlternativeBlock is not null ||
               ArmorType is not null ||
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