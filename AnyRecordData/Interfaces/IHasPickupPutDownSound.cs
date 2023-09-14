using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasPickUpPutDownSound
{
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }

    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef)
        {
            case IAmmunitionGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsAmmo().PickUpSound, oldRef.AsAmmo().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsAmmo().PutDownSound, oldRef.AsAmmo().PutDownSound); 
                break;
            case IArmorGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsArmor().PickUpSound, oldRef.AsArmor().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsArmor().PutDownSound, oldRef.AsArmor().PutDownSound); 
                break;
            case IBookGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsBook().PickUpSound, oldRef.AsBook().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsBook().PutDownSound, oldRef.AsBook().PutDownSound); 
                break;
            case IIngestibleGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsIngestible().PickUpSound, oldRef.AsIngestible().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsIngestible().PutDownSound, oldRef.AsIngestible().PutDownSound); 
                break;
            case IIngredientGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsIngredient().PickUpSound, oldRef.AsIngredient().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsIngredient().PutDownSound, oldRef.AsIngredient().PutDownSound); 
                break;
            case IMiscItemGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsMisc().PickUpSound, oldRef.AsMisc().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsMisc().PutDownSound, oldRef.AsMisc().PutDownSound); 
                break;
            case ISoulGemGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsSoulGem().PickUpSound, oldRef.AsSoulGem().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsSoulGem().PutDownSound, oldRef.AsSoulGem().PutDownSound); 
                break;
            case IWeaponGetter: 
                PickUpSound  = DataUtils.GetString(newRef.AsWeapon().PickUpSound, oldRef.AsWeapon().PickUpSound); 
                PutDownSound = DataUtils.GetString(newRef.AsWeapon().PutDownSound, oldRef.AsWeapon().PutDownSound); 
                break;
        }
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IAmmunitionGetter: 
                DataUtils.PatchFormLink(rec.AsAmmo().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsAmmo().PutDownSound, PutDownSound); 
                break;
            case IArmorGetter: 
                DataUtils.PatchFormLink(rec.AsArmor().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsArmor().PutDownSound, PutDownSound); 
                break;
            case IBookGetter: 
                DataUtils.PatchFormLink(rec.AsBook().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsBook().PutDownSound, PutDownSound); 
                break;
            case IIngestibleGetter: 
                DataUtils.PatchFormLink(rec.AsIngestible().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsIngestible().PutDownSound, PutDownSound); 
                break;
            case IIngredientGetter: 
                DataUtils.PatchFormLink(rec.AsIngredient().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsIngredient().PutDownSound, PutDownSound); 
                break;
            case IMiscItemGetter: 
                DataUtils.PatchFormLink(rec.AsMisc().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsMisc().PutDownSound, PutDownSound); 
                break;
            case ISoulGemGetter: 
                DataUtils.PatchFormLink(rec.AsSoulGem().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsSoulGem().PutDownSound, PutDownSound); 
                break;
            case IWeaponGetter: 
                DataUtils.PatchFormLink(rec.AsWeapon().PickUpSound, PickUpSound); 
                DataUtils.PatchFormLink(rec.AsWeapon().PutDownSound, PutDownSound); 
                break;
        }
    }

    public bool IsModifiedInterface()
    {
        return PickUpSound is not null ||
               PutDownSound is not null;
    }
}