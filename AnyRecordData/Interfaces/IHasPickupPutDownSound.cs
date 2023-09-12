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
        (string?, string?) newSounds = GetPickUpPutDownSounds(newRef);
        (string?, string?) oldSounds = GetPickUpPutDownSounds(oldRef);

        if (newSounds.Item1 is null && oldSounds.Item1 is not null)
        {
            PickUpSoundDeleted = true;
        }
        else if (newSounds.Item1?.Equals(oldSounds.Item1) == false)
        {
            PickUpSound = newSounds.Item1;
        }
        
        if (newSounds.Item2 is null && oldSounds.Item2 is not null)
        {
            PutDownSoundDeleted = true;
        }
        else if (newSounds.Item2?.Equals(oldSounds.Item2) == false)
        {
            PutDownSound = newSounds.Item2;
        }
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
                case IAmmunition    item: item.PickUpSound = PickUpSoundLink(); break;
                case IArmor         item: item.PickUpSound = PickUpSoundLink(); break;
                case IBook          item: item.PickUpSound = PickUpSoundLink(); break;
                case IIngestible    item: item.PickUpSound = PickUpSoundLink(); break;
                case IIngredient    item: item.PickUpSound = PickUpSoundLink(); break;
                case IMiscItem      item: item.PickUpSound = PickUpSoundLink(); break;
                case ISoulGem       item: item.PickUpSound = PickUpSoundLink(); break;
                case IWeapon        item: item.PickUpSound = PickUpSoundLink(); break;
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
                case IAmmunition    item: item.PutDownSound = PutDownSoundLink(); break;
                case IArmor         item: item.PutDownSound = PutDownSoundLink(); break;
                case IBook          item: item.PutDownSound = PutDownSoundLink(); break;
                case IIngestible    item: item.PutDownSound = PutDownSoundLink(); break;
                case IIngredient    item: item.PutDownSound = PutDownSoundLink(); break;
                case IMiscItem      item: item.PutDownSound = PutDownSoundLink(); break;
                case ISoulGem       item: item.PutDownSound = PutDownSoundLink(); break;
                case IWeapon        item: item.PutDownSound = PutDownSoundLink(); break;
            }
        }
    }

    private static (string?, string?) GetPickUpPutDownSounds(ISkyrimMajorRecordGetter rec)
    {
        var sounds = rec switch
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

        return (sounds.PickUpSound?.FormKey.ToString(), sounds.PutDownSound?.FormKey.ToString());
    }

    private FormLinkNullable<ISoundDescriptorGetter> PutDownSoundLink()
    {
        return new FormLinkNullable<ISoundDescriptorGetter>(FormKey.Factory(PutDownSound));
    }

    private FormLinkNullable<ISoundDescriptorGetter> PickUpSoundLink()
    {
        return new FormLinkNullable<ISoundDescriptorGetter>(FormKey.Factory(PickUpSound));
    }

    public bool IsModifiedInterface()
    {
        return PickUpSound is not null ||
               PutDownSound is not null ||
               PickUpSoundDeleted is not null ||
               PutDownSoundDeleted is not null;
    }
}