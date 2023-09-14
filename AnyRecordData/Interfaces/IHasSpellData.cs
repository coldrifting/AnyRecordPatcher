using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasSpellData
{
    public uint? BaseCost { get; set; }
    public string? Type { get; set; }
    public float? ChargeTime { get; set; }
    public string? CastType { get; set; }
    public string? TargetType { get; set; }
    public float? CastDuration { get; set; }
    public float? Range { get; set; }
    public string? HalfCostPerk { get; set; }

    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef)
        {
            case IScrollGetter: SaveChangesScroll(newRef.AsScroll(), oldRef.AsScroll()); break;
            case ISpellGetter:  SaveChangesSpell(newRef.AsSpell(), oldRef.AsSpell()); break;
        }
    }

    private void SaveChangesScroll(IScrollGetter newRef, IScrollGetter oldRef)
    {
        BaseCost     = DataUtils.GetNumber(newRef.BaseCost, oldRef.BaseCost);
        ChargeTime   = DataUtils.GetNumber(newRef.ChargeTime, oldRef.ChargeTime);
        CastDuration = DataUtils.GetNumber(newRef.CastDuration, oldRef.CastDuration);
        Range        = DataUtils.GetNumber(newRef.Range, oldRef.Range);
        
        Type         = DataUtils.GetString(newRef.Type, oldRef.Type);
        CastType     = DataUtils.GetString(newRef.CastType, oldRef.CastType);
        TargetType   = DataUtils.GetString(newRef.TargetType, oldRef.TargetType);
        
        HalfCostPerk = DataUtils.GetString(newRef.HalfCostPerk, oldRef.HalfCostPerk);
    }

    private void SaveChangesSpell(ISpellGetter newRef, ISpellGetter oldRef)
    {
        BaseCost     = DataUtils.GetNumber(newRef.BaseCost, oldRef.BaseCost);
        ChargeTime   = DataUtils.GetNumber(newRef.ChargeTime, oldRef.ChargeTime);
        CastDuration = DataUtils.GetNumber(newRef.CastDuration, oldRef.CastDuration);
        Range        = DataUtils.GetNumber(newRef.Range, oldRef.Range);
        
        Type         = DataUtils.GetString(newRef.Type, oldRef.Type);
        CastType     = DataUtils.GetString(newRef.CastType, oldRef.CastType);
        TargetType   = DataUtils.GetString(newRef.TargetType, oldRef.TargetType);
        
        HalfCostPerk = DataUtils.GetString(newRef.HalfCostPerk, oldRef.HalfCostPerk);
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IScroll scroll: PatchScroll(scroll); break;
            case ISpell spell:   PatchSpell(spell);break;
        }
    }

    private void PatchScroll(IScroll rec)
    {
        rec.BaseCost = DataUtils.PatchNumber(rec.BaseCost, BaseCost);
        rec.ChargeTime = DataUtils.PatchNumber(rec.ChargeTime, ChargeTime);
        rec.CastDuration = DataUtils.PatchNumber(rec.CastDuration, CastDuration);
        rec.Range = DataUtils.PatchNumber(rec.Range, Range);
        
        rec.Type = Enum.Parse<SpellType>(Type ?? rec.Type.ToString());
        rec.CastType = Enum.Parse<CastType>(CastType ?? rec.CastType.ToString());
        rec.TargetType = Enum.Parse<TargetType>(TargetType ?? rec.TargetType.ToString());

        DataUtils.PatchFormLink(rec.HalfCostPerk, HalfCostPerk);
    }
    
    private void PatchSpell(ISpell rec)
    {
        rec.BaseCost = DataUtils.PatchNumber(rec.BaseCost, BaseCost);
        rec.ChargeTime = DataUtils.PatchNumber(rec.ChargeTime, ChargeTime);
        rec.CastDuration = DataUtils.PatchNumber(rec.CastDuration, CastDuration);
        rec.Range = DataUtils.PatchNumber(rec.Range, Range);
        
        rec.Type = Enum.Parse<SpellType>(Type ?? rec.Type.ToString());
        rec.CastType = Enum.Parse<CastType>(CastType ?? rec.CastType.ToString());
        rec.TargetType = Enum.Parse<TargetType>(TargetType ?? rec.TargetType.ToString());

        DataUtils.PatchFormLink(rec.HalfCostPerk, HalfCostPerk);
    }

    public bool IsModifiedInterface()
    {
        return BaseCost is not null ||
               Type is not null ||
               ChargeTime is not null ||
               CastType is not null ||
               TargetType is not null ||
               CastDuration is not null ||
               Range is not null ||
               HalfCostPerk is not null;
    }
}