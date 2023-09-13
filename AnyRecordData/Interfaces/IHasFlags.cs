using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasFlags
{
    public string[]? Flags { get; set; }
    
    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        Flags = (newRef, oldRef) switch
        {
            (IArmorGetter, IArmorGetter) x => 
                GetFlags(
                    ((IArmorGetter)x.newRef).BodyTemplate?.FirstPersonFlags,
                     ((IArmorGetter)x.oldRef).BodyTemplate?.FirstPersonFlags),
            (IIngestibleGetter, IIngestibleGetter) x => 
                GetFlags(
                    ((IIngestibleGetter)x.newRef).Flags,
                     ((IIngestibleGetter)x.oldRef).Flags),
            (IIngredientGetter, IIngredientGetter) x => 
                GetFlags(
                    ((IIngredientGetter)x.newRef).Flags,
                    ((IIngredientGetter)x.oldRef).Flags),
            (ILightGetter, ILightGetter) x => 
                GetFlags(
                    ((ILightGetter)x.newRef).Flags,
                    ((ILightGetter)x.oldRef).Flags),
            (IScrollGetter, IScrollGetter) x => 
                GetFlags(
                    ((IScrollGetter)x.newRef).Flags,
                    ((IScrollGetter)x.oldRef).Flags),
            (ISpellGetter, ISpellGetter) x => 
                GetFlags(
                    ((ISpellGetter)x.newRef).Flags,
                    ((ISpellGetter)x.oldRef).Flags),
            _ => Flags
        };
    }
    
    private static string[]? GetFlags(BipedObjectFlag? newFlags, BipedObjectFlag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToArray() 
            : null;
    }
    
    private static string[]? GetFlags(Ingestible.Flag? newFlags, Ingestible.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToArray() 
            : null;
    }
    
    private static string[]? GetFlags(Ingredient.Flag? newFlags, Ingredient.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToArray() 
            : null;
    }
    
    private static string[]? GetFlags(Light.Flag? newFlags, Light.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToArray() 
            : null;
    }
    
    private static string[]? GetFlags(SpellDataFlag? newFlags, SpellDataFlag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToArray() 
            : null;
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IArmor item:
                item.BodyTemplate ??= new BodyTemplate();
                item.BodyTemplate.FirstPersonFlags = SetArmorFlags() ?? item.BodyTemplate.FirstPersonFlags;
                break;
            case IIngestible item:
                item.Flags = SetIngestibleFlags() ?? item.Flags;
                break;
            case IIngredient item:
                item.Flags = SetIngredientFlags() ?? item.Flags;
                break;
            case ILight item:
                item.Flags = SetLightFlags() ?? item.Flags;
                break;
            case IScroll item:
                item.Flags = SetSpellDataFlags() ?? item.Flags;
                break;
            case ISpell item:
                item.Flags = SetSpellDataFlags() ?? item.Flags;
                break;
        }
    }

    private BipedObjectFlag? SetArmorFlags()
    {
        if (Flags is null) 
            return null;
        
        BipedObjectFlag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<BipedObjectFlag>(flag), true);
        }
        
        return flags;
    }

    private Ingestible.Flag? SetIngestibleFlags()
    {
        if (Flags is null) 
            return null;
        
        Ingestible.Flag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<Ingestible.Flag>(flag), true);
        }
        
        return flags;
    }

    private Ingredient.Flag? SetIngredientFlags()
    {
        if (Flags is null) 
            return null;
        
        Ingredient.Flag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<Ingredient.Flag>(flag), true);
        }
        
        return flags;
    }

    private Light.Flag? SetLightFlags()
    {
        if (Flags is null) 
            return null;
        
        Light.Flag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<Light.Flag>(flag), true);
        }

        return flags;
    }

    private SpellDataFlag? SetSpellDataFlags()
    {
        if (Flags is null) 
            return null;
        
        SpellDataFlag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<SpellDataFlag>(flag), true);
        }

        return flags;
    }

    public bool IsModifiedInterface()
    {
        return Flags is not null;
    }
}