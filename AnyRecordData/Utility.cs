using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData;

public static class Utility
{
    internal static bool AreEqual(float f1, float f2)
    {
        return Math.Abs(f1 - f2) < 0.01;
    }

    internal static ISet<string> ToStringSet(IReadOnlyList<IFormLinkGetter<IArmorAddonGetter>>? armatures)
    {
        HashSet<string> set = new();
        if (armatures is null)
            return set;

        foreach (var x in armatures)
        {
            set.Add(x.FormKey.ToString());
        }
        
        return set;
    }

    internal static ISet<string> ToStringSet(IReadOnlyList<IFormLinkGetter<IKeywordGetter>>? keywords)
    {
        HashSet<string> set = new();
        if (keywords is null)
            return set;

        foreach (var x in keywords)
        {
            set.Add(x.FormKey.ToString());
        }
        
        return set;
    }

    public static FormKey ToFormKey(this string? input)
    {
        return input is null 
            ? FormKey.Null 
            : FormKey.Factory(input);
    }
}