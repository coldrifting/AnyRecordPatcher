using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData;

// Helper function for casting records to subrecords
public static partial class DataUtils
{
    // Getters
    public static IAmmunitionGetter AsAmmo(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IAmmunitionGetter ?? throw new InvalidOperationException();
    }

    public static IArmorGetter AsArmor(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IArmorGetter ?? throw new InvalidOperationException();
    }

    public static IBookGetter AsBook(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IBookGetter ?? throw new InvalidOperationException();
    }

    public static ICellGetter AsCell(this ISkyrimMajorRecordGetter rec)
    {
        return rec as ICellGetter ?? throw new InvalidOperationException();
    }

    public static IIngestibleGetter AsIngestible(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IIngestibleGetter ?? throw new InvalidOperationException();
    }

    public static IIngredientGetter AsIngredient(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IIngredientGetter ?? throw new InvalidOperationException();
    }

    public static ILightGetter AsLight(this ISkyrimMajorRecordGetter rec)
    {
        return rec as ILightGetter ?? throw new InvalidOperationException();
    }

    public static IMiscItemGetter AsMisc(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IMiscItemGetter ?? throw new InvalidOperationException();
    }

    public static IPerkGetter AsPerk(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IPerkGetter ?? throw new InvalidOperationException();
    }

    public static IScrollGetter AsScroll(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IScrollGetter ?? throw new InvalidOperationException();
    }

    public static IShoutGetter AsShout(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IShoutGetter ?? throw new InvalidOperationException();
    }

    public static ISoulGemGetter AsSoulGem(this ISkyrimMajorRecordGetter rec)
    {
        return rec as ISoulGemGetter ?? throw new InvalidOperationException();
    }

    public static ISpellGetter AsSpell(this ISkyrimMajorRecordGetter rec)
    {
        return rec as ISpellGetter ?? throw new InvalidOperationException();
    }

    public static IWeaponGetter AsWeapon(this ISkyrimMajorRecordGetter rec)
    {
        return rec as IWeaponGetter ?? throw new InvalidOperationException();
    }
    
    // Setters
    public static IAmmunition AsAmmo(this ISkyrimMajorRecord rec)
    {
        return rec as IAmmunition ?? throw new InvalidOperationException();
    }

    public static IArmor AsArmor(this ISkyrimMajorRecord rec)
    {
        return rec as IArmor ?? throw new InvalidOperationException();
    }

    public static IBook AsBook(this ISkyrimMajorRecord rec)
    {
        return rec as IBook ?? throw new InvalidOperationException();
    }

    public static ICell AsCell(this ISkyrimMajorRecord rec)
    {
        return rec as ICell ?? throw new InvalidOperationException();
    }

    public static IIngestible AsIngestible(this ISkyrimMajorRecord rec)
    {
        return rec as IIngestible ?? throw new InvalidOperationException();
    }

    public static IIngredient AsIngredient(this ISkyrimMajorRecord rec)
    {
        return rec as IIngredient ?? throw new InvalidOperationException();
    }

    public static ILight AsLight(this ISkyrimMajorRecord rec)
    {
        return rec as ILight ?? throw new InvalidOperationException();
    }

    public static IMiscItem AsMisc(this ISkyrimMajorRecord rec)
    {
        return rec as IMiscItem ?? throw new InvalidOperationException();
    }

    public static IPerk AsPerk(this ISkyrimMajorRecord rec)
    {
        return rec as IPerk ?? throw new InvalidOperationException();
    }

    public static IScroll AsScroll(this ISkyrimMajorRecord rec)
    {
        return rec as IScroll ?? throw new InvalidOperationException();
    }

    public static IShout AsShout(this ISkyrimMajorRecord rec)
    {
        return rec as IShout ?? throw new InvalidOperationException();
    }

    public static ISoulGem AsSoulGem(this ISkyrimMajorRecord rec)
    {
        return rec as ISoulGem ?? throw new InvalidOperationException();
    }

    public static ISpell AsSpell(this ISkyrimMajorRecord rec)
    {
        return rec as ISpell ?? throw new InvalidOperationException();
    }

    public static IWeapon AsWeapon(this ISkyrimMajorRecord rec)
    {
        return rec as IWeapon ?? throw new InvalidOperationException();
    }
}