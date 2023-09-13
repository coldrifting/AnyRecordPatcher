using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataScroll : BaseItem, IHasName, IHasKeywords, IHasModel, IHasObjectBounds, IHasWeightValue, IHasPickUpPutDownSound, IHasMagicEffects, IHasEquipmentType, IHasMenuDisplayObject, IHasDescription
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

    public string? MenuObject { get; set; }
    public bool? MenuObjectDeleted { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }

    public DataScroll()
    {
        PatchFileName = "Scrolls";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IScrollGetter x && oldRef is IScrollGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IScrollGetter newRef, IScrollGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);
        ((IHasMagicEffects)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEquipmentType)this).SaveChangesInterface(newRef, oldRef);
        ((IHasMenuDisplayObject)this).SaveChangesInterface(newRef, oldRef);
        ((IHasDescription)this).SaveChangesInterface(newRef, oldRef);
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IScroll recScroll)
            Patch(recScroll);
    }

    private void Patch(IScroll rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasMagicEffects)this).PatchInterface(rec);
        ((IHasEquipmentType)this).PatchInterface(rec);
        ((IHasMenuDisplayObject)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);
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
               ((IHasMenuDisplayObject)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface();
    }
}