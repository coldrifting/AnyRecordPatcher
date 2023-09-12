using Mutagen.Bethesda.Plugins.Aspects;

namespace AnyRecordData.Interfaces;

public interface IHasName
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }

    public void SaveChangesInterface(INamedGetter newRef, INamedGetter oldRef)
    {
        if (newRef.Name is null && oldRef.Name is not null)
        {
            NameDeleted = true;
            return;
        }
        
        if (newRef.Name is not null && !newRef.Name.Equals(oldRef.Name))
        {
            Name = newRef.Name;
        }
    }
    
    public void PatchInterface<T>(T rec)
        where T: INamed
    {
        if (NameDeleted == true)
            rec.Name = null;
        
        if (Name is not null) 
            rec.Name = Name;

    }

    public bool IsModifiedInterface()
    {
        return Name is not null ||
               NameDeleted is not null;
    }
}