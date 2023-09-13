using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataIngestible : BaseItem, IHasName, IHasKeywords, IHasModel, IHasObjectBounds, IHasWeightValue, IHasPickUpPutDownSound, IHasMagicEffects, IHasEquipmentType, IHasDescription
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }

    public string? ModelPath { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }

    public List<DataMagicEffect>? Effects { get; set; }
    
    public string? EquipmentType { get; set; }
    public bool? EquipmentTypeDeleted { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }
    
    // Ingestible Specific
    public uint? Flags { get; set; }
    public string? Addiction { get; set; }
    public float? AddictionChance { get; set; }
    public string? ConsumeSound { get; set; }

    public DataIngestible()
    {
        PatchFileName = "Ingestibles";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IIngestibleGetter x && oldRef is IIngestibleGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IIngestibleGetter newRef, IIngestibleGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);
        ((IHasMagicEffects)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEquipmentType)this).SaveChangesInterface(newRef, oldRef);
        ((IHasDescription)this).SaveChangesInterface(newRef, oldRef);

        if (newRef.Flags != oldRef.Flags)
            Flags = (uint)newRef.Flags;

        if (!newRef.Addiction.FormKey.ToString().Equals(oldRef.Addiction.FormKey.ToString()))
            Addiction = newRef.Addiction.FormKey.ToString();

        if (!Utility.AreEqual(newRef.AddictionChance, oldRef.AddictionChance))
            AddictionChance = newRef.AddictionChance;

        if (!newRef.ConsumeSound.FormKey.ToString().Equals(oldRef.ConsumeSound.FormKey.ToString()))
            ConsumeSound = newRef.ConsumeSound.FormKey.ToString();
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IIngestible recIngestible)
            Patch(recIngestible);
    }
    
    public void Patch(IIngestible rec)
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

        if (Flags is not null)
            rec.Flags = (Ingestible.Flag)Flags;

        if (Addiction is not null)
            rec.Addiction = new FormLink<ISkyrimMajorRecordGetter>(Addiction.ToFormKey());

        if (AddictionChance is not null)
            rec.AddictionChance = AddictionChance ?? 0.0f;

        if (ConsumeSound is not null)
            rec.ConsumeSound = new FormLink<ISoundDescriptorGetter>(ConsumeSound.ToFormKey());
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
               Flags is not null ||
               Addiction is not null ||
               AddictionChance is not null ||
               ConsumeSound is not null;
    }
}