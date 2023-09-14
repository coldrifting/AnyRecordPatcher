using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataPerk : DataBaseItem, 
                        IHasName, 
                        IHasDescription
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Perk Specific
    [UsedImplicitly] public string? NextPerk { get; set; }
    [UsedImplicitly] public bool? Trait { get; set; }
    [UsedImplicitly] public byte? Level { get; set; }
    [UsedImplicitly] public byte? NumRanks { get; set; }
    [UsedImplicitly] public bool? Playable { get; set; }
    [UsedImplicitly] public bool? Hidden { get; set; }
    [UsedImplicitly] public List<DataCondition>? Conditions { get; set; }
    [UsedImplicitly] public List<DataPerkEffect>? Effects { get; set; }

    public DataPerk()
    {
        PatchFileName = "Perks";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IPerkGetter x && oldRef is IPerkGetter y)
            GetData(x, y);
    }

    private void GetData(IPerkGetter newRef, IPerkGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);

        NextPerk = DataUtils.GetString(newRef.NextPerk, oldRef.NextPerk);
        Trait = DataUtils.GetBool(newRef.Trait, oldRef.Trait);
        Level = DataUtils.GetNumber(newRef.Level, oldRef.Level);
        NumRanks = DataUtils.GetNumber(newRef.NumRanks, oldRef.NumRanks);
        Playable = DataUtils.GetBool(newRef.Playable, oldRef.Playable);
        Hidden = DataUtils.GetBool(newRef.Hidden, oldRef.Hidden);

        if (!newRef.Conditions.SequenceEqual(oldRef.Conditions))
        {
            Conditions = DataCondition.CopyConditions(newRef.Conditions);
        }

        if (newRef.Effects.ToHashSet().SetEquals(oldRef.Effects))
            return;
        
        Effects = new List<DataPerkEffect>();
        foreach (IAPerkEffectGetter effect in newRef.Effects)
        {
            Effects.Add(DataPerkEffect.ConvertToData(effect));
        }
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IPerk recPerk)
            Patch(recPerk);
    }

    private void Patch(IPerk rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);

        DataUtils.PatchFormLink(rec.NextPerk, NextPerk);
        rec.Trait = DataUtils.PatchBool(rec.Trait, Trait);
        rec.Level = DataUtils.PatchNumber(rec.Level, Level);
        rec.NumRanks = DataUtils.PatchNumber(rec.NumRanks, NumRanks);
        rec.Playable = DataUtils.PatchBool(rec.Playable, Playable);
        rec.Hidden = DataUtils.PatchBool(rec.Hidden, Hidden);

        if (Conditions is not null)
        {
            rec.Conditions.Clear();
            foreach (DataCondition c in Conditions)
            {
                rec.Conditions.Add(c.ConvertToPatch());
            }
        }

        if (Effects is not null)
        {
            rec.Effects.Clear();
            foreach (DataPerkEffect e in Effects)
            {
                rec.Effects.Add(e.ConvertToPatch());
            }
        }
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               NextPerk is not null ||
               Trait is not null ||
               Level is not null ||
               NumRanks is not null ||
               Playable is not null ||
               Hidden is not null ||
               Conditions is not null ||
               Effects is not null;
    }
}