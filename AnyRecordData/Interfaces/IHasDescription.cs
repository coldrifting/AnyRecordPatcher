using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasDescription
{
    public string? Description { get; set; }

    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        Description = newRef switch
        {
            IArmorGetter =>      DataUtils.GetString(newRef.AsArmor().Description, oldRef.AsArmor().Description),
            IIngestibleGetter => DataUtils.GetString(newRef.AsIngestible().Description, oldRef.AsIngestible().Description),
            IPerkGetter =>       DataUtils.GetString(newRef.AsPerk().Description, oldRef.AsPerk().Description),
            IScrollGetter =>     DataUtils.GetString(newRef.AsScroll().Description, oldRef.AsScroll().Description),
            IShoutGetter =>      DataUtils.GetString(newRef.AsShout().Description, oldRef.AsShout().Description),
            ISpellGetter =>      DataUtils.GetString(newRef.AsSpell().Description, oldRef.AsSpell().Description),
            IWeaponGetter =>     DataUtils.GetString(newRef.AsWeapon().Description, oldRef.AsWeapon().Description),
            _ => default
        };
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IArmor      item1: item1.Description = DataUtils.PatchString(item1.Description, Description); break;
            case IIngestible item2: item2.Description = DataUtils.PatchString(item2.Description, Description); break;
            case IPerk       item3: item3.Description = DataUtils.PatchString(item3.Description, Description); break;
            case IScroll     item4: item4.Description = DataUtils.PatchString(item4.Description, Description); break;
            case IShout      item5: item5.Description = DataUtils.PatchString(item5.Description, Description); break;
            case ISpell      item6: item6.Description = DataUtils.PatchString(item6.Description, Description); break;
            case IWeapon     item7: item7.Description = DataUtils.PatchString(item7.Description, Description); break;
        }
    }

    public bool IsModifiedInterface()
    {
        return Description is not null;
    }
}