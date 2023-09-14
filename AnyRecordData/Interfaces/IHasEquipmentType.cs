using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasEquipmentType
{
    public string? EquipmentType { get; set; }
    
    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        EquipmentType = newRef switch
        {
            IArmorGetter =>      DataUtils.GetString(newRef.AsArmor().EquipmentType, oldRef.AsArmor().EquipmentType),
            IIngestibleGetter => DataUtils.GetString(newRef.AsIngestible().EquipmentType, oldRef.AsIngestible().EquipmentType),
            ISpellGetter =>      DataUtils.GetString(newRef.AsSpell().EquipmentType, oldRef.AsSpell().EquipmentType),
            IWeaponGetter =>     DataUtils.GetString(newRef.AsWeapon().EquipmentType, oldRef.AsWeapon().EquipmentType),
            _ => default
        };
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IArmor      item1: DataUtils.PatchFormLink(item1.EquipmentType, EquipmentType); break;
            case IIngestible item2: DataUtils.PatchFormLink(item2.EquipmentType, EquipmentType); break;
            case ISpell      item3: DataUtils.PatchFormLink(item3.EquipmentType, EquipmentType); break;
            case IWeapon     item4: DataUtils.PatchFormLink(item4.EquipmentType, EquipmentType); break;
        }
    }

    public bool IsModifiedInterface()
    {
        return EquipmentType is not null;
    }
}