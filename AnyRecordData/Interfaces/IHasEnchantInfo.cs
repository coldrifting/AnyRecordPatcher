using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasEnchantInfo
{
    public string? ObjectEffect { get; set; }
    public ushort? EnchantmentAmount { get; set; }

    public void GetDataInterface(IEnchantableGetter newRef, IEnchantableGetter oldRef)
    {
        ObjectEffect = DataUtils.GetString(newRef.ObjectEffect, oldRef.ObjectEffect);
        EnchantmentAmount = DataUtils.GetNumber(newRef.EnchantmentAmount, oldRef.EnchantmentAmount);
    }

    public void PatchInterface(IEnchantable rec)
    {
        DataUtils.PatchFormLink(rec.ObjectEffect, ObjectEffect);

        if (EnchantmentAmount is not null)
            rec.EnchantmentAmount = DataUtils.PatchNumber(rec.EnchantmentAmount ?? ushort.MaxValue, EnchantmentAmount);
    }

    public bool IsModifiedInterface()
    {
        return ObjectEffect is not null ||
               EnchantmentAmount is not null;
    }
}