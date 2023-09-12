using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasObjectBounds
{
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }

    public void SaveChangesInterface(IObjectBoundedOptionalGetter newRef, IObjectBoundedOptionalGetter oldRef)
    {
        if (newRef.ObjectBounds is null && oldRef.ObjectBounds is not null)
        {
            BoundsDeleted = true;
            return;
        }
        
        if (newRef.ObjectBounds is null || newRef.ObjectBounds.Equals(oldRef.ObjectBounds)) 
            return;
        
        Bounds = new short[6];
        Bounds[0]= newRef.ObjectBounds.First.X;
        Bounds[1]= newRef.ObjectBounds.First.Y;
        Bounds[2]= newRef.ObjectBounds.First.Z;
        Bounds[3]= newRef.ObjectBounds.Second.X;
        Bounds[4]= newRef.ObjectBounds.Second.Y;
        Bounds[5]= newRef.ObjectBounds.Second.Z;
    }
    
    public void PatchInterface<T>(T rec)
        where T : IObjectBoundedOptional
    {
        if (BoundsDeleted == true)
        {
            if (!typeof(T).IsAssignableTo(typeof(IObjectBounded)))
            {
                // Only set object bounds to null if it can be removed from the record normally
                rec.ObjectBounds = null;
            }
        }
        
        if (Bounds is null) return;

        rec.ObjectBounds ??= new ObjectBounds();
        rec.ObjectBounds.First = new P3Int16(Bounds[0], Bounds[1], Bounds[2]);
        rec.ObjectBounds.Second = new P3Int16(Bounds[3], Bounds[4], Bounds[5]);
    }

    public bool IsModifiedInterface()
    {
        return Bounds is not null ||
               BoundsDeleted is not null;
    }
}