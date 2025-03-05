using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataArmor : DataBaseItem,
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
    public string? Description { get; set; }
    public List<string>? Keywords { get; set; }
    public short[]? Bounds { get; set; }
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public string? EquipmentType { get; set; }
    public string? ObjectEffect { get; set; }
    public ushort? EnchantmentAmount { get; set; }
    public List<string>? Flags { get; set; }
    
    // Armor Specific
    [UsedImplicitly] public float? ArmorRating { get; set; }
    [UsedImplicitly] public string? BashImpact { get; set; }
    [UsedImplicitly] public string? AlternativeBlock { get; set; }
    [UsedImplicitly] public string? ArmorType { get; set; }
    [UsedImplicitly] public string? Race { get; set; }
    [UsedImplicitly] public string? TemplateArmor { get; set;}
    [UsedImplicitly] public List<string>? Armatures { get; set; }
    [UsedImplicitly] public string? MaleModelPath { get; set; }
    [UsedImplicitly] public List<DataAltTexSet>? MaleModelTextures { get; set; }
    [UsedImplicitly] public string? FemaleModelPath { get; set; }
    [UsedImplicitly] public List<DataAltTexSet>? FemaleModelTextures { get; set; }
    [UsedImplicitly] public string? MaleIconLarge { get; set; }
    [UsedImplicitly] public string? MaleIconSmall { get; set; }
    [UsedImplicitly] public string? FemaleIconLarge { get; set; }
    [UsedImplicitly] public string? FemaleIconSmall { get; set; }

    public DataArmor()
    {
        PatchFileName = "Armor";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IArmorGetter x && oldRef is IArmorGetter y)
            GetChanges(x, y);
    }

    private void GetChanges(IArmorGetter newRef, IArmorGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasEnchantInfo)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);
        
        ArmorRating = DataUtils.GetNumber(newRef.ArmorRating, oldRef.ArmorRating);
        BashImpact = DataUtils.GetString(newRef.BashImpactDataSet, oldRef.BashImpactDataSet);
        AlternativeBlock = DataUtils.GetString(newRef.AlternateBlockMaterial, oldRef.AlternateBlockMaterial);
        TemplateArmor = DataUtils.GetString(newRef.TemplateArmor, oldRef.TemplateArmor);
        Race = DataUtils.GetString(newRef.Race, oldRef.Race);
        ArmorType = DataUtils.GetString(newRef.BodyTemplate?.ArmorType, oldRef.BodyTemplate?.ArmorType);
        Armatures = DataUtils.GetDataFormLinkList(newRef.Armature, oldRef.Armature);

        // Models
        string? newMaleModelFile = newRef.WorldModel?.Male?.Model?.File.GivenPath;
        string? oldMaleModelFile = oldRef.WorldModel?.Male?.Model?.File.GivenPath;
        MaleModelPath = DataUtils.GetString(newMaleModelFile, oldMaleModelFile);
        
        string? newFemaleModelFile = newRef.WorldModel?.Female?.Model?.File.GivenPath;
        string? oldFemaleModelFile = oldRef.WorldModel?.Female?.Model?.File.GivenPath;
        FemaleModelPath = DataUtils.GetString(newFemaleModelFile, oldFemaleModelFile);
        
        var newMaleList = newRef.WorldModel?.Male?.Model?.AlternateTextures;
        var oldMaleList = oldRef.WorldModel?.Male?.Model?.AlternateTextures;
        MaleModelTextures = DataAltTexSet.GetTextureList(newMaleList, oldMaleList);

        var newFemaleList = newRef.WorldModel?.Female?.Model?.AlternateTextures;
        var oldFemaleList = oldRef.WorldModel?.Female?.Model?.AlternateTextures;
        FemaleModelTextures = DataAltTexSet.GetTextureList(newFemaleList, oldFemaleList);

        // Icons
        MaleIconLarge = DataUtils.GetString(newRef.WorldModel?.Male?.Icons?.LargeIconFilename, oldRef.WorldModel?.Male?.Icons?.LargeIconFilename);
        MaleIconSmall = DataUtils.GetString(newRef.WorldModel?.Male?.Icons?.SmallIconFilename, oldRef.WorldModel?.Male?.Icons?.SmallIconFilename);
        FemaleIconLarge = DataUtils.GetString(newRef.WorldModel?.Female?.Icons?.LargeIconFilename, oldRef.WorldModel?.Female?.Icons?.LargeIconFilename);
        FemaleIconSmall = DataUtils.GetString(newRef.WorldModel?.Female?.Icons?.SmallIconFilename, oldRef.WorldModel?.Female?.Icons?.SmallIconFilename);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IArmor recArmor)
            Patch(recArmor);
    }

    private void Patch(IArmor rec)
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

        rec.ArmorRating = DataUtils.PatchNumber(rec.ArmorRating, ArmorRating);

        DataUtils.PatchFormLink(rec.AlternateBlockMaterial, AlternativeBlock);
        DataUtils.PatchFormLink(rec.BashImpactDataSet, BashImpact);
        
        if (ArmorType is not null)
        {
            rec.BodyTemplate ??= new BodyTemplate();
            rec.BodyTemplate.ArmorType = Enum.Parse<ArmorType>(ArmorType);
        }

        DataUtils.PatchFormLink(rec.Race, Race);
        DataUtils.PatchFormLink(rec.TemplateArmor, TemplateArmor);
        
        DataUtils.PatchFormLinkList(rec.Armature, Armatures);
        
        if (MaleModelPath is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            DataUtils.PatchString(rec.WorldModel.Male.Model.File.GivenPath, MaleModelPath);
        }
        
        if (MaleModelTextures is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Model ??= new Model();
            DataAltTexSet.PatchModelTextures(rec.WorldModel.Male.Model.AlternateTextures, MaleModelTextures);
        }

        if (MaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Icons ??= new Icons();
            DataUtils.PatchString(rec.WorldModel.Male.Icons.LargeIconFilename, MaleIconLarge);
        }

        if (MaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Male ??= new ArmorModel();
            rec.WorldModel.Male.Icons ??= new Icons();
            DataUtils.PatchString(rec.WorldModel.Male.Icons.SmallIconFilename, MaleIconSmall);
        }
        
        if (FemaleModelPath is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            DataUtils.PatchString(rec.WorldModel.Female.Model.File.GivenPath, FemaleModelPath);
        }
        
        if (FemaleModelTextures is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Model ??= new Model();
            DataAltTexSet.PatchModelTextures(rec.WorldModel.Female.Model.AlternateTextures, FemaleModelTextures);
        }

        if (FemaleIconLarge is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Icons ??= new Icons();
            DataUtils.PatchString(rec.WorldModel.Female.Icons.LargeIconFilename, FemaleIconLarge);
        }

        if (FemaleIconSmall is not null)
        {
            rec.WorldModel ??= new GenderedItem<ArmorModel?>(new ArmorModel(), new ArmorModel());
            rec.WorldModel.Female ??= new ArmorModel();
            rec.WorldModel.Female.Icons ??= new Icons();
            DataUtils.PatchString(rec.WorldModel.Female.Icons.SmallIconFilename, FemaleIconSmall);
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