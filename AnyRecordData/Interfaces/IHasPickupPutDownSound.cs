using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasPickUpPutDownSound
{
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }

    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }

    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        var newSounds = GetPickUpPutDownSounds(newRef);
        var oldSounds = GetPickUpPutDownSounds(oldRef);

        PickUpSoundDeleted = Utility.GetDeleted(newSounds.Item1, oldSounds.Item1);
        PickUpSound = Utility.GetChangesFormKey(newSounds.Item1, oldSounds.Item1);
        
        PutDownSoundDeleted = Utility.GetDeleted(newSounds.Item2, oldSounds.Item2);
        PutDownSound = Utility.GetChangesFormKey(newSounds.Item2, oldSounds.Item2);
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        if (PickUpSoundDeleted == true)
        {
            switch (rec)
            {
                case IAmmunition    item: item.PickUpSound.SetToNull(); break;
                case IArmor         item: item.PickUpSound.SetToNull(); break;
                case IBook          item: item.PickUpSound.SetToNull(); break;
                case IIngestible    item: item.PickUpSound.SetToNull(); break;
                case IIngredient    item: item.PickUpSound.SetToNull(); break;
                case IMiscItem      item: item.PickUpSound.SetToNull(); break;
                case ISoulGem       item: item.PickUpSound.SetToNull(); break;
                case IWeapon        item: item.PickUpSound.SetToNull(); break;
            }
        }
        else if (PickUpSound is not null)
        {
            switch (rec)
            {
                case IAmmunition    item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IArmor         item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IBook          item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IIngestible    item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IIngredient    item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IMiscItem      item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case ISoulGem       item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
                case IWeapon        item: item.PickUpSound.SetTo(PickUpSound.ToFormKey()); break;
            }
        }
        
        if (PutDownSoundDeleted == true)
        {
            switch (rec)
            {
                case IAmmunition    item: item.PutDownSound.SetToNull(); break;
                case IArmor         item: item.PutDownSound.SetToNull(); break;
                case IBook          item: item.PutDownSound.SetToNull(); break;
                case IIngestible    item: item.PutDownSound.SetToNull(); break;
                case IIngredient    item: item.PutDownSound.SetToNull(); break;
                case IMiscItem      item: item.PutDownSound.SetToNull(); break;
                case ISoulGem       item: item.PutDownSound.SetToNull(); break;
                case IWeapon        item: item.PutDownSound.SetToNull(); break;
            }
        }
        else if (PutDownSound is not null)
        {
            switch (rec)
            {
                case IAmmunition    item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IArmor         item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IBook          item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IIngestible    item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IIngredient    item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IMiscItem      item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case ISoulGem       item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
                case IWeapon        item: item.PutDownSound.SetTo(PutDownSound.ToFormKey()); break;
            }
        }
    }

    private static (IFormLinkNullableGetter<ISoundDescriptorGetter>, IFormLinkNullableGetter<ISoundDescriptorGetter>) GetPickUpPutDownSounds(ISkyrimMajorRecordGetter rec)
    {
        return rec switch
        {
            IAmmunitionGetter   item => (item.PickUpSound, item.PutDownSound),
            IArmorGetter        item => (item.PickUpSound, item.PutDownSound),
            IBookGetter         item => (item.PickUpSound, item.PutDownSound),
            IIngestibleGetter   item => (item.PickUpSound, item.PutDownSound),
            IIngredientGetter   item => (item.PickUpSound, item.PutDownSound),
            IMiscItemGetter     item => (item.PickUpSound, item.PutDownSound),
            ISoulGemGetter      item => (item.PickUpSound, item.PutDownSound),
            IWeaponGetter       item => (item.PickUpSound, item.PutDownSound),
            _ => default
        };
    }

    public bool IsModifiedInterface()
    {
        return PickUpSound is not null ||
               PutDownSound is not null ||
               PickUpSoundDeleted is not null ||
               PutDownSoundDeleted is not null;
    }
}