using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataSoulGem : DataBaseItem, 
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
        
    // SoulGem Specific
    [UsedImplicitly] public string? LinkedTo { get; set; }

    public DataSoulGem()
    {
        PatchFileName = "SoulGems";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ISoulGemGetter x && oldRef is ISoulGemGetter y)
            GetData(x, y);
    }

    private void GetData(ISoulGemGetter newRef, ISoulGemGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);

        LinkedTo = DataUtils.GetString(newRef.LinkedTo, oldRef.LinkedTo);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ISoulGem recSoulGem)
            Patch(recSoulGem);
    }

    private void Patch(ISoulGem rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);

        DataUtils.PatchFormLink(rec.LinkedTo, LinkedTo);
    }
    
    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               LinkedTo is not null;
    }
}
