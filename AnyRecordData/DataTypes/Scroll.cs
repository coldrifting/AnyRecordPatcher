using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataScroll : DataBaseItem, 
                          IHasName, 
                          IHasKeywords, 
                          IHasModel, 
                          IHasObjectBounds, 
                          IHasWeightValue, 
                          IHasPickUpPutDownSound, 
                          IHasMagicEffects, 
                          IHasEquipmentType, 
                          IHasMenuDisplayObject, 
                          IHasDescription,
                          IHasSpellData,
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
    public string? EquipmentType { get; set; }
    public string? MenuObject { get; set; }
    public string? Description { get; set; }
    public uint? BaseCost { get; set; }
    public string? Type { get; set; }
    public float? ChargeTime { get; set; }
    public string? CastType { get; set; }
    public string? TargetType { get; set; }
    public float? CastDuration { get; set; }
    public float? Range { get; set; }
    public string? HalfCostPerk { get; set; }
    public List<DataMagicEffect>? Effects { get; set; }
    public List<string>? Flags { get; set; }

    public DataScroll()
    {
        PatchFileName = "Scrolls";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IScrollGetter x && oldRef is IScrollGetter y)
            GetData(x, y);
    }

    private void GetData(IScrollGetter newRef, IScrollGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
        ((IHasMagicEffects)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasMenuDisplayObject)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);
        ((IHasSpellData)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);
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
        ((IHasSpellData)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);
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
               ((IHasDescription)this).IsModifiedInterface() ||
               ((IHasSpellData)this).IsModifiedInterface() ||
               ((IHasFlags)this).IsModifiedInterface();
    }
}