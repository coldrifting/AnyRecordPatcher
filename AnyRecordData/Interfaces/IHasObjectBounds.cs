using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasObjectBounds
{
    public short[]? Bounds { get; set; }

    public void GetDataInterface(IObjectBoundedOptionalGetter newRef, IObjectBoundedOptionalGetter oldRef)
    {
        if (newRef.ObjectBounds is null && oldRef.ObjectBounds is not null)
            Bounds = Array.Empty<short>();
        
        if (newRef.ObjectBounds is null || newRef.ObjectBounds.Equals(oldRef.ObjectBounds)) 
            return;
        
        Bounds = new[]
        {
            newRef.ObjectBounds.First.X, 
            newRef.ObjectBounds.First.Y, 
            newRef.ObjectBounds.First.Z, 
            newRef.ObjectBounds.Second.X, 
            newRef.ObjectBounds.Second.Y, 
            newRef.ObjectBounds.Second.Z
        };
    }
    
    public void PatchInterface<T>(T rec)
        where T : IObjectBoundedOptional
    {
        if (Bounds is null) 
            return;
        
        if (Bounds.Length <= 6)
            rec.ObjectBounds = null;

        rec.ObjectBounds ??= new ObjectBounds();
        rec.ObjectBounds.First = new P3Int16(Bounds[0], Bounds[1], Bounds[2]);
        rec.ObjectBounds.Second = new P3Int16(Bounds[3], Bounds[4], Bounds[5]);
    }

    public bool IsModifiedInterface()
    {
        return Bounds is not null;
    }
}