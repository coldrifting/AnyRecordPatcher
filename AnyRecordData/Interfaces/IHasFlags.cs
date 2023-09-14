using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasFlags
{
    public List<string>? Flags { get; set; }
    
    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        Flags = newRef switch
        {
            IArmorGetter =>      GetFlags(newRef.AsArmor().BodyTemplate?.FirstPersonFlags, oldRef.AsArmor().BodyTemplate?.FirstPersonFlags),
            ICellGetter =>       GetFlags(newRef.AsCell().Flags, oldRef.AsCell().Flags),
            IIngestibleGetter => GetFlags(newRef.AsIngestible().Flags, oldRef.AsIngestible().Flags),
            IIngredientGetter => GetFlags(newRef.AsIngredient().Flags, oldRef.AsIngredient().Flags),
            ILightGetter =>      GetFlags(newRef.AsLight().Flags, oldRef.AsLight().Flags),
            IScrollGetter =>     GetFlags(newRef.AsScroll().Flags, oldRef.AsScroll().Flags),
            ISpellGetter =>      GetFlags(newRef.AsSpell().Flags, oldRef.AsSpell().Flags),
            _ => default
        };
    }
    
    private static List<string>? GetFlags(BipedObjectFlag? newFlags, BipedObjectFlag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
            : null;
    }
    
    private static List<string>? GetFlags(Cell.Flag? newFlags, Cell.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
            : null;
    }
    
    private static List<string>? GetFlags(Ingestible.Flag? newFlags, Ingestible.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
            : null;
    }
    
    private static List<string>? GetFlags(Ingredient.Flag? newFlags, Ingredient.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
            : null;
    }
    
    private static List<string>? GetFlags(Light.Flag? newFlags, Light.Flag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
            : null;
    }
    
    private static List<string>? GetFlags(SpellDataFlag? newFlags, SpellDataFlag? oldFlags)
    {
        return !Equals(newFlags, oldFlags) 
            ? newFlags?.EnumerateContainedFlags().Select(flag => flag.ToString()).ToList()
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
            case ICell item:
                item.Flags = SetCellFlags() ?? item.Flags;
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

    private Cell.Flag? SetCellFlags()
    {
        if (Flags is null) 
            return null;
        
        Cell.Flag flags = new();
        foreach (string flag in Flags)
        {
            flags = flags.SetFlag(Enum.Parse<Cell.Flag>(flag), true);
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