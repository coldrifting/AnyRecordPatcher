using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataSpell : BaseItem, IHasName, IHasKeywords, IHasObjectBounds, IHasMagicEffects
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    // Spell Specific
    public string? Description { get; set; }
    public string? EquipmentType { get; set; }
    public string? MenuObject { get; set; }

    public uint? BaseCost { get; set; }
    public uint? Flags { get; set; }
    public string? Type { get; set; }
    public float? ChargeTime { get; set; }
    public string? CastType { get; set; }
    public string? TargetType { get; set; }
    public float? CastDuration { get; set; }
    public float? Range { get; set; }
    public string? HalfCostPerk { get; set; }
    
    public List<DataMagicEffect>? Effects { get; set; }

    public DataSpell()
    {
        PatchFileName = "Spells";
    }
    
    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ISpellGetter x && oldRef is ISpellGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ISpellGetter newRef, ISpellGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasMagicEffects)this).SaveChangesInterface(newRef, oldRef);

        string newDesc = newRef.Description.String ?? "";
        string oldDesc = oldRef.Description.String ?? "";
        if (newDesc != oldDesc)
        {
            Description = newDesc;
        }

        if (!newRef.EquipmentType.FormKey.ToString().Equals(oldRef.EquipmentType.FormKey.ToString()))
            EquipmentType = newRef.EquipmentType.FormKey.ToString();

        if (!newRef.MenuDisplayObject.FormKey.ToString().Equals(oldRef.MenuDisplayObject.FormKey.ToString()))
            MenuObject = newRef.MenuDisplayObject.FormKey.ToString();

        if (newRef.BaseCost != oldRef.BaseCost)
            BaseCost = newRef.BaseCost;

        if (!newRef.Flags.Equals(oldRef.Flags))
            Flags = (uint)newRef.Flags;

        if (!newRef.Type.Equals(oldRef.Type))
            Type = newRef.Type.ToString();

        if (!Utility.AreEqual(newRef.ChargeTime, oldRef.ChargeTime))
            ChargeTime = newRef.ChargeTime;

        if (!newRef.CastType.Equals(oldRef.CastType))
            CastType = newRef.CastType.ToString();

        if (!newRef.TargetType.Equals(oldRef.TargetType))
            TargetType = newRef.TargetType.ToString();

        if (!Utility.AreEqual(newRef.CastDuration, oldRef.CastDuration))
            CastDuration = newRef.CastDuration;

        if (!Utility.AreEqual(newRef.Range, oldRef.Range))
            Range = newRef.Range;

        string newHalfCostPerk = newRef.HalfCostPerk.FormKey.IsNull
            ? ""
            : newRef.HalfCostPerk.FormKey.ToString();
        
        string oldHalfCostPerk = oldRef.HalfCostPerk.FormKey.IsNull
            ? ""
            : oldRef.HalfCostPerk.FormKey.ToString();

        if (newHalfCostPerk != oldHalfCostPerk)
            HalfCostPerk = newHalfCostPerk;
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ISpell recSpell)
            Patch(recSpell);
    }

    public void Patch(ISpell rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasMagicEffects)this).PatchInterface(rec);

        if (Description is not null)
            rec.Description = Description;

        if (EquipmentType is not null)
            rec.EquipmentType = new FormLinkNullable<IEquipTypeGetter>(EquipmentType.ToFormKey());

        if (MenuObject is not null)
            rec.MenuDisplayObject = new FormLinkNullable<IStaticGetter>(MenuObject.ToFormKey());

        if (BaseCost is not null)
            rec.BaseCost = BaseCost ?? 1;

        if (Flags is not null)
            rec.Flags = (SpellDataFlag)Flags;

        if (Type is not null)
            rec.Type = Enum.Parse<SpellType>(Type);

        if (ChargeTime is not null)
            rec.ChargeTime = ChargeTime ?? 1.0f;

        if (CastType is not null)
            rec.CastType = Enum.Parse<CastType>(CastType);

        if (TargetType is not null)
            rec.TargetType = Enum.Parse<TargetType>(TargetType);

        if (CastDuration is not null)
            rec.CastDuration = CastDuration ?? 1.0f;

        if (Range is not null)
            rec.Range = Range ?? 1.0f;

        if (HalfCostPerk is not null)
            rec.HalfCostPerk = new FormLinkNullable<IPerkGetter>(HalfCostPerk.ToFormKey());
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasMagicEffects)this).IsModifiedInterface() ||
               Description is not null ||
               EquipmentType is not null ||
               MenuObject is not null ||
               BaseCost is not null ||
               Flags is not null ||
               Type is not null ||
               ChargeTime is not null ||
               CastType is not null ||
               TargetType is not null ||
               CastDuration is not null ||
               Range is not null ||
               HalfCostPerk is not null ||
               Effects is not null;
    }
}