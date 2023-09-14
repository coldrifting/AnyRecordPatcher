using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataCell : DataBaseItem, IHasName
{
    public string? Name { get; set; }
    
    // Cell Specific
    public string? EncounterZone { get; set; }
    public string? Location { get; set; }
    public List<string>? Regions { get; set; }

    public DataCell()
    {
        PatchFileName = "Cells";
        
        Regions = new List<string>();
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is ICellGetter x && oldRef is ICellGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(ICellGetter newRef, ICellGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);

        EncounterZone = DataUtils.GetDataString(newRef.EncounterZone, oldRef.EncounterZone);
        Location = DataUtils.GetDataString(newRef.Location, oldRef.Location);
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

        DataUtils.PatchFormLink(rec.EncounterZone, EncounterZone);
        DataUtils.PatchFormLink(rec.Location, Location);
        DataUtils.PatchFormLinkList(rec.Regions, Regions);
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               EncounterZone is not null ||
               Location is not null ||
               Regions is not null;
    }
}