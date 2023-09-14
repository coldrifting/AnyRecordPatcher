using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataSpell : DataBaseItem, 
                         IHasName, 
                         IHasKeywords, 
                         IHasObjectBounds, 
                         IHasMagicEffects, 
                         IHasEquipmentType, 
                         IHasMenuDisplayObject, 
                         IHasDescription, 
                         IHasSpellData,
                         IHasFlags
{
    public string? Name { get; set; }
    public List<string>? Keywords { get; set; }
    public short[]? Bounds { get; set; }
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
    public List<string>? Flags { get; set; }
    public List<DataMagicEffect>? Effects { get; set; }

    public DataSpell()
    {
        PatchFileName = "Spells";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ISpellGetter x && oldRef is ISpellGetter y)
            GetData(x, y);
    }

    private void GetData(ISpellGetter newRef, ISpellGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasMagicEffects)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasMenuDisplayObject)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);
        ((IHasSpellData)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ISpell recSpell)
            Patch(recSpell);
    }

    private void Patch(ISpell rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
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
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasMagicEffects)this).IsModifiedInterface() ||
               ((IHasEquipmentType)this).IsModifiedInterface() ||
               ((IHasMenuDisplayObject)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               ((IHasSpellData)this).IsModifiedInterface() ||
               ((IHasFlags)this).IsModifiedInterface();
    }
}