using Mutagen.Bethesda.Plugins.Aspects;

namespace AnyRecordData.Interfaces;

public interface IHasWeightValue
{
    public float? Weight { get; set; }
    public uint? Value { get; set; }

    public void SaveChangesInterface(IWeightValueGetter newRef, IWeightValueGetter oldRef)
    {
        Weight = Utility.GetChangesNumber(newRef.Weight, oldRef.Weight);
        Value = Utility.GetChangesNumber(newRef.Value, oldRef.Value);
    }
    
    public void PatchInterface<T>(T rec)
        where T : IWeightValue
    {
        if (Weight is not null)
            rec.Weight = Weight ?? 1.0f;

        if (Value is not null)
            rec.Value = Value ?? 1;
    }

    public bool IsModifiedInterface()
    {
        return Weight is not null ||
               Value is not null;
    }
}