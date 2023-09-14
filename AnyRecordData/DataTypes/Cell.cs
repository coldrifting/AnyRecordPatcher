using AnyRecordData.Interfaces;
using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.DataTypes;

public class DataCell : DataBaseItem, IHasName, IHasFlags
{
    public string? Name { get; set; }
    public List<string>? Flags { get; set; }
    
    // Cell Specific
    [UsedImplicitly] public string? LightingTemplate { get; set; }
    [UsedImplicitly] public string? ImageSpace { get; set; }
    [UsedImplicitly] public string? Water { get; set; }
    [UsedImplicitly] public string? Owner { get; set; }
    [UsedImplicitly] public string? EncounterZone { get; set; }
    [UsedImplicitly] public string? Location { get; set; }
    [UsedImplicitly] public List<string>? Regions { get; set; }

    public DataCell()
    {
        PatchFileName = "Cells";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ICellGetter x && oldRef is ICellGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ICellGetter newRef, ICellGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasFlags)this).GetDataInterface(newRef, oldRef);
        
        LightingTemplate = DataUtils.GetString(newRef.LightingTemplate, oldRef.LightingTemplate);
        ImageSpace = DataUtils.GetString(newRef.ImageSpace, oldRef.ImageSpace);
        Water = DataUtils.GetString(newRef.Water, oldRef.Water);
        Owner = DataUtils.GetString(newRef.Owner, oldRef.Owner);
        EncounterZone = DataUtils.GetString(newRef.EncounterZone, oldRef.EncounterZone);
        Location = DataUtils.GetString(newRef.Location, oldRef.Location);
        Regions = DataUtils.GetDataFormLinkList(newRef.Regions, oldRef.Regions);
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is ICell recCell)
            Patch(recCell);
    }

    private void Patch(ICell rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasFlags)this).PatchInterface(rec);

        DataUtils.PatchFormLink(rec.LightingTemplate, LightingTemplate);
        DataUtils.PatchFormLink(rec.ImageSpace, ImageSpace);
        DataUtils.PatchFormLink(rec.Water, Water);
        DataUtils.PatchFormLink(rec.Owner, Owner);
        DataUtils.PatchFormLink(rec.EncounterZone, EncounterZone);
        DataUtils.PatchFormLink(rec.Location, Location);
        DataUtils.PatchFormLinkList(rec.Regions ??= new ExtendedList<IFormLinkGetter<IRegionGetter>>(), Regions);
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasFlags)this).IsModifiedInterface() ||
               LightingTemplate is not null ||
               ImageSpace is not null ||
               Water is not null ||
               Owner is not null ||
               EncounterZone is not null ||
               Location is not null ||
               Regions is not null;
    }
}