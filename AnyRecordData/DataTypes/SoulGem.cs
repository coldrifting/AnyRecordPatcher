using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataSoulGem : BaseItem, 
                           IHasName, 
                           IHasKeywords, 
                           IHasModel, 
                           IHasObjectBounds, 
                           IHasWeightValue, 
                           IHasPickUpPutDownSound
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }

    public string? ModelPath { get; set; }
    public bool? ModelPathDeleted { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }
        
    // SoulGem Specific
    [YamlMember] public string? LinkedTo { get; set; }

    public DataSoulGem()
    {
        PatchFileName = "SoulGems";
    }
    
    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ISoulGemGetter x && oldRef is ISoulGemGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ISoulGemGetter newRef, ISoulGemGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);

        LinkedTo = Utility.GetChangesString(newRef.LinkedTo, oldRef.LinkedTo);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ISoulGem recSoulGem)
            Patch(recSoulGem);
    }

    public void Patch(ISoulGem rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);

        if (LinkedTo is not null)
            rec.LinkedTo = new FormLinkNullable<ISoulGemGetter>(LinkedTo.ToFormKey());
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
