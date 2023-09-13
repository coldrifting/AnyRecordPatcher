using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasEquipmentType
{
    public string? EquipmentType { get; set; }
    public bool? EquipmentTypeDeleted { get; set; }
    
    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef, oldRef)
        {
            case (IArmorGetter, IArmorGetter) x:
                SaveChangesEquipType(
                    ((IArmorGetter)x.newRef).EquipmentType,
                    ((IArmorGetter)x.oldRef).EquipmentType);
                break;

            case (IIngestibleGetter, IIngestibleGetter) x:
                SaveChangesEquipType(
                    ((IIngestibleGetter)x.newRef).EquipmentType,
                    ((IIngestibleGetter)x.oldRef).EquipmentType);
                break;
            
            case (ISpellGetter, ISpellGetter) x:
                SaveChangesEquipType(
                    ((ISpellGetter)x.newRef).EquipmentType,
                    ((ISpellGetter)x.oldRef).EquipmentType);
                break;
            
            case (IWeaponGetter, IWeaponGetter) x:
                SaveChangesEquipType(
                    ((IWeaponGetter)x.newRef).EquipmentType,
                    ((IWeaponGetter)x.oldRef).EquipmentType);
                break;
        }
    }

    public void SaveChangesEquipType(IFormLinkNullableGetter<IEquipTypeGetter> first,
                      IFormLinkNullableGetter<IEquipTypeGetter> second)
    {
        EquipmentTypeDeleted = Utility.GetDeleted(first, second);
        EquipmentType = Utility.GetChangesFormKey(first, second);
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IArmor x:
                x.EquipmentType = PatchEquipType(x.EquipmentType);
                break;

            case IIngestible x:
                x.EquipmentType = PatchEquipType(x.EquipmentType);
                break;
            
            case ISpell x:
                x.EquipmentType = PatchEquipType(x.EquipmentType);
                break;
            
            case IWeapon x:
                x.EquipmentType = PatchEquipType(x.EquipmentType);
                break;
        }
    }

    public IFormLinkNullable<IEquipTypeGetter> PatchEquipType(IFormLinkNullable<IEquipTypeGetter> original)
    {
        if (EquipmentType is not null)
            return new FormLinkNullable<IEquipTypeGetter>(EquipmentType.ToFormKey());
        
        return EquipmentTypeDeleted is not null 
            ? new FormLinkNullable<IEquipTypeGetter>(FormKey.Null) 
            : original;
    }

    public bool IsModifiedInterface()
    {
        return EquipmentType is not null ||
               EquipmentTypeDeleted is not null;
    }
}