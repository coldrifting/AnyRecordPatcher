using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasEnchantInfo
{
    public string? ObjectEffect { get; set; }
    public bool? ObjectEffectDeleted { get; set; }
    public ushort? EnchantmentAmount { get; set; }

    public void SaveChangesInterface(IEnchantableGetter newRef, IEnchantableGetter oldRef)
    {
        string newEnchant = newRef.ObjectEffect.FormKey.ToString();
        string oldEnchant = oldRef.ObjectEffect.FormKey.ToString();
        if (newEnchant is "Null" && oldEnchant is not "Null")
        {
            ObjectEffectDeleted = true;
        }

        if (newEnchant is not "Null" && newEnchant != oldEnchant)
        {
            ObjectEffect = newEnchant;
        }
    }

    public void PatchInterface(IEnchantable rec)
    {
        if (ObjectEffectDeleted is not null)
            rec.ObjectEffect.SetToNull();
        
        if (ObjectEffect is not null)
            rec.ObjectEffect.SetTo(ObjectEffect.ToFormKey());
    }

    public bool IsModifiedInterface()
    {
        return ObjectEffect is not null ||
               ObjectEffectDeleted is not null ||
               EnchantmentAmount is not null;
    }
}