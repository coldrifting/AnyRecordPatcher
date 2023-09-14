using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataAmmo : DataBaseItem,
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
    
    // Ammunition Specific
    [UsedImplicitly] public float? Damage { get; set; }

    public DataAmmo()
    {
        PatchFileName = "Ammo";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IAmmunitionGetter x && oldRef is IAmmunitionGetter y)
            GetData(x, y);
    }

    private void GetData(IAmmunitionGetter newRef, IAmmunitionGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);

        Damage = DataUtils.GetNumber(newRef.Damage, oldRef.Damage);
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

        rec.Damage = DataUtils.PatchNumber(rec.Damage, Damage);
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