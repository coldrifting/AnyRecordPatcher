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

    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef, oldRef)
        {
            case (IScrollGetter, IScrollGetter) x:
                SaveChangesScroll((IScrollGetter)x.newRef, (IScrollGetter)x.oldRef);
                break;
            
            case (ISpellGetter, ISpellGetter) x:
                SaveChangesSpell((ISpellGetter)x.newRef, (ISpellGetter)x.oldRef);
                break;
        }
    }

    private void SaveChangesScroll(IScrollGetter newRef, IScrollGetter oldRef)
    {
        BaseCost = Utility.GetChangesNumber(newRef.BaseCost, oldRef.BaseCost);
        Type = Utility.GetChangesString(newRef.Type, oldRef.Type);
        ChargeTime = Utility.GetChangesNumber(newRef.ChargeTime, oldRef.ChargeTime);
        CastType = Utility.GetChangesString(newRef.CastType, oldRef.CastType);
        TargetType = Utility.GetChangesString(newRef.TargetType, oldRef.TargetType);
        CastDuration = Utility.GetChangesNumber(newRef.CastDuration, oldRef.CastDuration);
        Range = Utility.GetChangesNumber(newRef.Range, oldRef.Range);
        HalfCostPerk = Utility.GetChangesFormKey(newRef.HalfCostPerk, oldRef.HalfCostPerk);
    }

    private void SaveChangesSpell(ISpellGetter newRef, ISpellGetter oldRef)
    {
        BaseCost = Utility.GetChangesNumber(newRef.BaseCost, oldRef.BaseCost);
        Type = Utility.GetChangesString(newRef.Type, oldRef.Type);
        ChargeTime = Utility.GetChangesNumber(newRef.ChargeTime, oldRef.ChargeTime);
        CastType = Utility.GetChangesString(newRef.CastType, oldRef.CastType);
        TargetType = Utility.GetChangesString(newRef.TargetType, oldRef.TargetType);
        CastDuration = Utility.GetChangesNumber(newRef.CastDuration, oldRef.CastDuration);
        Range = Utility.GetChangesNumber(newRef.Range, oldRef.Range);
        HalfCostPerk = Utility.GetChangesFormKey(newRef.HalfCostPerk, oldRef.HalfCostPerk);
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IScroll scroll:
                PatchScroll(scroll);
                break;
            
            case ISpell spell:
                PatchSpell(spell);
                break;
        }
    }

    private void PatchScroll(IScroll rec)
    {
        rec.BaseCost = BaseCost ?? rec.BaseCost;
        rec.Type = Enum.Parse<SpellType>(Type ?? rec.Type.ToString());
        rec.ChargeTime = ChargeTime ?? rec.ChargeTime;
        rec.CastType = Enum.Parse<CastType>(CastType ?? rec.CastType.ToString());
        rec.TargetType = Enum.Parse<TargetType>(TargetType ?? rec.TargetType.ToString());
        rec.CastDuration = CastDuration ?? rec.CastDuration;
        rec.Range = Range ?? rec.Range;
        rec.ChargeTime = ChargeTime ?? rec.ChargeTime;

        if (HalfCostPerk is not null)
            rec.HalfCostPerk.SetTo(HalfCostPerk.ToFormKey());
    }
    
    private void PatchSpell(ISpell rec)
    {
        rec.BaseCost = BaseCost ?? rec.BaseCost;
        rec.Type = Enum.Parse<SpellType>(Type ?? rec.Type.ToString());
        rec.ChargeTime = ChargeTime ?? rec.ChargeTime;
        rec.CastType = Enum.Parse<CastType>(CastType ?? rec.CastType.ToString());
        rec.TargetType = Enum.Parse<TargetType>(TargetType ?? rec.TargetType.ToString());
        rec.CastDuration = CastDuration ?? rec.CastDuration;
        rec.Range = Range ?? rec.Range;
        rec.ChargeTime = ChargeTime ?? rec.ChargeTime;

        if (HalfCostPerk is not null)
            rec.HalfCostPerk.SetTo(HalfCostPerk.ToFormKey());
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