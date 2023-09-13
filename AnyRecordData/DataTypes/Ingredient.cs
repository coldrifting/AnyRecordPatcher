﻿using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataIngredient : BaseItem, IHasName, IHasKeywords, IHasModel, IHasObjectBounds, IHasWeightValue, IHasPickUpPutDownSound, IHasMagicEffects, IHasEquipmentType
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }

    public string? ModelPath { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }

    public string? EquipmentType { get; set; }
    public bool? EquipmentTypeDeleted { get; set; }

    public List<DataMagicEffect>? Effects { get; set; }

    public uint? Flags { get; set; }

    public DataIngredient()
    {
        PatchFileName = "Ingredients";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IIngredientGetter x && oldRef is IIngredientGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IIngredientGetter newRef, IIngredientGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);
        ((IHasMagicEffects)this).SaveChangesInterface(newRef, oldRef);
        ((IHasEquipmentType)this).SaveChangesInterface(newRef, oldRef);

        if (newRef.Flags != oldRef.Flags)
            Flags = (uint)newRef.Flags;
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IIngredient recIngredient)
            Patch(recIngredient);
    }

    public void Patch(IIngredient rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasMagicEffects)this).PatchInterface(rec);

        if (Flags is not null)
            rec.Flags = (Ingredient.Flag)Flags;
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               ((IHasMagicEffects)this).IsModifiedInterface() ||
               Flags is not null;
    }
}