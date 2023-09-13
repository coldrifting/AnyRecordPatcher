using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasDescription
{
    public string? Description { get; set; }
    public bool? DescriptionDeleted { get; set; }

    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef, oldRef)
        {
            case (IArmorGetter, IArmorGetter) x:
                SaveChangesDescription(
                    ((IArmorGetter)x.newRef).Description?.ToString(), 
                    (((IArmorGetter)x.oldRef).Description?.ToString()));
                break;
            
            case (IIngestibleGetter, IIngestibleGetter) x:
                SaveChangesDescription(
                    ((IIngestibleGetter)x.newRef).Description?.ToString(), 
                    (((IIngestibleGetter)x.oldRef).Description?.ToString()));
                break;
            
            case (IPerkGetter, IPerkGetter) x:
                SaveChangesDescription(
                    ((IPerkGetter)x.newRef).Description.ToString(), 
                    (((IPerkGetter)x.oldRef).Description.ToString()));
                break;
            
            case (IScrollGetter, IScrollGetter) x:
                SaveChangesDescription(
                    ((IScrollGetter)x.newRef).Description?.ToString(), 
                    (((IScrollGetter)x.oldRef).Description?.ToString()));
                break;
            
            case (IShoutGetter, IShoutGetter) x:
                SaveChangesDescription(
                    ((IShoutGetter)x.newRef).Description?.ToString(), 
                    (((IShoutGetter)x.oldRef).Description?.ToString()));
                break;
            
            case (ISpellGetter, ISpellGetter) x:
                SaveChangesDescription(
                    ((ISpellGetter)x.newRef).Description.ToString(), 
                    (((ISpellGetter)x.oldRef).Description.ToString()));
                break;
            
            case (IWeaponGetter, IWeaponGetter) x:
                SaveChangesDescription(
                    ((IWeaponGetter)x.newRef).Description?.ToString(), 
                    (((IWeaponGetter)x.oldRef).Description?.ToString()));
                break;
        }
    }

    private void SaveChangesDescription(string? newDesc, string? oldDesc)
    {
        DescriptionDeleted = Utility.GetDeleted(newDesc, oldDesc);
        Description = Utility.GetChangesString(newDesc, oldDesc);
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        if (Description is null && DescriptionDeleted is null)
            return;

        string? desc = Description;
        if (DescriptionDeleted is not null)
            desc = null;
        
        switch (rec)
        {
            case IArmor item:
                item.Description = desc;
                break;
            
            case IIngestible item:
                item.Description = desc;
                break;
            
            case IPerk item:
                item.Description = desc;
                break;
            
            case IScroll item:
                item.Description = desc;
                break;
            
            case IShout item:
                item.Description = desc;
                break;
            
            case ISpell item:
                item.Description = desc;
                break;
            
            case IWeapon item:
                item.Description = desc;
                break;
        }
    }

    public bool IsModifiedInterface()
    {
        return Description is not null ||
               DescriptionDeleted is not null;
    }
}