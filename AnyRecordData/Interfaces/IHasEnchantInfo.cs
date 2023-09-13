using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasEnchantInfo
{
    public string? ObjectEffect { get; set; }
    public bool? ObjectEffectDeleted { get; set; }
    public ushort? EnchantmentAmount { get; set; }

    public void SaveChangesInterface(IEnchantableGetter newRef, IEnchantableGetter oldRef)
    {
        ObjectEffectDeleted = Utility.GetDeleted(newRef.ObjectEffect, oldRef.ObjectEffect);
        ObjectEffect = Utility.GetChangesFormKey(newRef.ObjectEffect, oldRef.ObjectEffect);
        EnchantmentAmount = Utility.GetChangesNumber(newRef.EnchantmentAmount, oldRef.EnchantmentAmount);
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