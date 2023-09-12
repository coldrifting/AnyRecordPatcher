using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataLight : BaseItem, IHasName, IHasModel, IHasObjectBounds, IHasWeightValue
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string? ModelPath { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }

    // TODO - Light Specific 

    public DataLight()
    {
        PatchFileName = "Lights";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ILightGetter x && oldRef is ILightGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ILightGetter newRef, ILightGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        
        // TODO
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ILight recLight)
            Patch(recLight);
    }

    public void Patch(ILight rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        
        // TODO
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface();
        // TODO
    }
}