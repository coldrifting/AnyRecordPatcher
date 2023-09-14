using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataIngestible : DataBaseItem,
                              IHasName,
                              IHasKeywords,
                              IHasModel,
                              IHasObjectBounds,
                              IHasWeightValue,
                              IHasPickUpPutDownSound,
                              IHasMagicEffects,
                              IHasEquipmentType,
                              IHasDescription,
                              IHasFlags
{
    public string? Name { get; set; }
    public List<string>? Keywords { get; set; }
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    public short[]? Bounds { get; set; }
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public List<DataMagicEffect>? Effects { get; set; }
    public string? EquipmentType { get; set; }
    public string? Description { get; set; }
    public List<string>? Flags { get; set; }
    
    // Ingestible Specific
    [UsedImplicitly] public string? Addiction { get; set; }
    [UsedImplicitly] public float? AddictionChance { get; set; }
    [UsedImplicitly] public string? ConsumeSound { get; set; }

    public DataIngestible()
    {
        PatchFileName = "Ingestibles";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IIngestibleGetter x && oldRef is IIngestibleGetter y)
            GetChanges(x, y);
    }

    private void GetChanges(IIngestibleGetter newRef, IIngestibleGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
        ((IHasMagicEffects)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);

        Addiction = DataUtils.GetString(newRef.Addiction, oldRef.Addiction);
        AddictionChance = DataUtils.GetNumber(newRef.AddictionChance, oldRef.AddictionChance);
        ConsumeSound = DataUtils.GetString(newRef.ConsumeSound, oldRef.ConsumeSound);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IIngestible recIngestible)
            Patch(recIngestible);
    }
    
    private void Patch(IIngestible rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasMagicEffects)this).PatchInterface(rec);
        ((IHasEquipmentType)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);

        DataUtils.PatchFormLink(rec.Addiction, Addiction);
        DataUtils.PatchFormLink(rec.ConsumeSound, ConsumeSound);
        rec.AddictionChance = DataUtils.PatchNumber(rec.AddictionChance, AddictionChance);
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               ((IHasMagicEffects)this).IsModifiedInterface() ||
               ((IHasEquipmentType)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               ((IHasFlags)this).IsModifiedInterface() ||
               Addiction is not null ||
               AddictionChance is not null ||
               ConsumeSound is not null;
    }
}