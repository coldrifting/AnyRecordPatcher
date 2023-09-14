using Mutagen.Bethesda.Plugins.Aspects;

namespace AnyRecordData.Interfaces;

public interface IHasWeightValue
{
    public float? Weight { get; set; }
    public uint? Value { get; set; }

    public void GetDataInterface(IWeightValueGetter newRef, IWeightValueGetter oldRef)
    {
        Weight = DataUtils.GetNumber(newRef.Weight, oldRef.Weight);
        Value = DataUtils.GetNumber(newRef.Value, oldRef.Value);
    }
    
    public void PatchInterface<T>(T rec)
        where T : IWeightValue
    {
        Weight = DataUtils.PatchNumber(rec.Weight, Weight);
        Value = DataUtils.PatchNumber(rec.Value, Value);
    }

    public bool IsModifiedInterface()
    {
        return Weight is not null ||
               Value is not null;
    }
}