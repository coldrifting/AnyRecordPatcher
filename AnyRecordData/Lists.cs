using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData;

// Helper Functions for dealing with lists and sets
public static partial class DataUtils
{
    public static List<string>? GetDataFormLinkList(
        IReadOnlyList<IFormLinkGetter<ISkyrimMajorRecordGetter>>? newList, 
        IReadOnlyList<IFormLinkGetter<ISkyrimMajorRecordGetter>>? oldList,
        bool isOrderImportant = false)
    {
        if (newList is null)
        {
            return oldList is not null 
                ? new List<string>() 
                : null;
        }

        if (oldList is null)
        {
            return ToStringList(newList);
        }
        
        // Both non-null
        var x = ToStringList(newList);
        var y = ToStringList(oldList);
        if ((isOrderImportant && x.SequenceEqual(y)) ||
            (!isOrderImportant && x.ToHashSet().SetEquals(y)))
        {
            return null;
        }

        return x;
    }

    public static void PatchFormLinkList<T>(ExtendedList<IFormLinkGetter<T>> recList, List<string>? dataList)
    where T: class, IMajorRecordGetter
    {
        if (dataList is null)
            return;

        recList.Clear();
        if (dataList.Count <= 0)
            return;

        foreach (var x in dataList)
        {
            recList.Add(new FormLink<T>(x.ToFormKey()));
        }
    }

    private static List<string> ToStringList(IReadOnlyList<IFormLinkGetter<ISkyrimMajorRecordGetter>>? list)
    {
        List<string> output = new();
        if (list is null)
            return output;

        foreach (var f in list)
        {
            output.Add(f.FormKey.ToString());
        }

        return output;
    }
}