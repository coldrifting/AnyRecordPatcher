using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataPerk : BaseItem, IHasName, IHasDescription
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }

    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }

    // Perk Specific
    public string? NextPerk { get; set; }
    public bool? NextPerkDeleted { get; set; }

    public bool? Trait { get; set; }
    public byte? Level { get; set; }
    public byte? NumRanks { get; set; }
    public bool? Playable { get; set; }
    public bool? Hidden { get; set; }
    
    public List<DataCondition>? Conditions { get; set; }

    public List<DataPerkEffect>? Effects { get; set; }

    public DataPerk()
    {
        PatchFileName = "Perks";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IPerkGetter x && oldRef is IPerkGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IPerkGetter newRef, IPerkGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasDescription)this).SaveChangesInterface(newRef, oldRef);
        
        if (newRef.NextPerk.IsNull && !oldRef.NextPerk.FormKey.IsNull)
        {
            NextPerkDeleted = true;
        }
        else if (!newRef.NextPerk.FormKey.ToString().Equals(oldRef.NextPerk.FormKey.ToString()))
        {
            NextPerk = newRef.NextPerk.FormKey.ToString();
        }

        if (newRef.Trait != oldRef.Trait)
            Trait = newRef.Trait;

        if (newRef.Level != oldRef.Level)
            Level = newRef.Level;

        if (newRef.NumRanks != oldRef.NumRanks)
            NumRanks = newRef.NumRanks;

        if (newRef.Playable != oldRef.Playable)
            Playable = newRef.Playable;

        if (newRef.Hidden != oldRef.Hidden)
            Hidden = newRef.Hidden;

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

        if (NextPerk is not null)
            rec.NextPerk = new FormLinkNullable<IPerkGetter>(NextPerk.ToFormKey());

        if (Trait is not null)
            rec.Trait = Trait.Value;

        if (Level is not null)
            rec.Level = Level.Value;

        if (NumRanks is not null)
            rec.NumRanks = NumRanks.Value;

        if (Playable is not null)
            rec.Playable = Playable.Value;

        if (Hidden is not null)
            rec.Hidden = Hidden.Value;

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