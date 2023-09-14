using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataMisc : DataBaseItem,
                        IHasName,
                        IHasKeywords,
                        IHasModel,
                        IHasObjectBounds,
                        IHasWeightValue,
                        IHasPickUpPutDownSound
{
    public string? Name { get; set; }
    public List<string>? Keywords { get; set; }
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    public short[]? Bounds { get; set; }
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }

    public DataMisc()
    {
        PatchFileName = "Misc";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IMiscItemGetter x && oldRef is IMiscItemGetter y)
            GetData(x, y);
    }

    private void GetData(IMiscItemGetter newRef, IMiscItemGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IMiscItem recMisc)
            Patch(recMisc);
    }

    private void Patch(IMiscItem rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface();
    }
}