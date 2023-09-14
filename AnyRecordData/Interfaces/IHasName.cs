using Mutagen.Bethesda.Plugins.Aspects;

namespace AnyRecordData.Interfaces;

public interface IHasName
{
    public string? Name { get; set; }

    public void GetDataInterface(INamedGetter newRef, INamedGetter oldRef)
    {
        Name = DataUtils.GetString(newRef.Name, oldRef.Name);
    }
    
    public void PatchInterface<T>(T rec)
        where T: INamed
    {
        rec.Name = DataUtils.PatchString(rec.Name, Name);
    }

    public bool IsModifiedInterface()
    {
        return Name is not null;
    }
}