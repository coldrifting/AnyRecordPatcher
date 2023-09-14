using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataIngredient : DataBaseItem, 
                              IHasName, 
                              IHasKeywords, 
                              IHasModel, 
                              IHasObjectBounds, 
                              IHasWeightValue, 
                              IHasPickUpPutDownSound, 
                              IHasMagicEffects, 
                              IHasEquipmentType,
                              IHasFlags
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
    public string? EquipmentType { get; set; }
    public List<DataMagicEffect>? Effects { get; set; }
    public List<string>? Flags { get; set; }

    public DataIngredient()
    {
        PatchFileName = "Ingredients";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IIngredientGetter x && oldRef is IIngredientGetter y)
            GetData(x, y);
    }

    private void GetData(IIngredientGetter newRef, IIngredientGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);
        ((IHasMagicEffects)this).GetDataInterface(newRef, oldRef);
        ((IHasEquipmentType)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IIngredient recIngredient)
            Patch(recIngredient);
    }

    private void Patch(IIngredient rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);
        ((IHasMagicEffects)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);
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
               ((IHasFlags)this).IsModifiedInterface();
    }
}