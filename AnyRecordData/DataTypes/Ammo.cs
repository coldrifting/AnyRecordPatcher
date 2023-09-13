using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataAmmo : BaseItem,
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
    
    // Ammunition Specific
    [YamlMember] public float? Damage { get; set; }

    public DataAmmo()
    {
        PatchFileName = "Ammo";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IAmmunitionGetter x && oldRef is IAmmunitionGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IAmmunitionGetter newRef, IAmmunitionGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);

        Damage = Utility.GetChangesNumber(newRef.Damage, oldRef.Damage);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IAmmunition recAmmo)
            Patch(recAmmo);
    }
    
    private void Patch(IAmmunition rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);

        if (Damage is not null)
            rec.Damage = Damage ?? 0.0f;
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               Damage is not null;
    }
}