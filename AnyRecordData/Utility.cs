using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData;

public static class Utility
{
    
    internal static bool? GetDeleted(object? newObj, object? oldObj)
    {
        if (newObj is null && oldObj is not null)
            return true;

        return null;
    }
    
    internal static byte? GetChangesNumber(byte? newNum, byte? oldNum)
    {
        if (newNum == oldNum)
            return null;

        return newNum;
    }
    
    internal static short? GetChangesNumber(short? newNum, short? oldNum)
    {
        if (newNum == oldNum)
            return null;

        return newNum;
    }
    
    internal static ushort? GetChangesNumber(ushort? newNum, ushort? oldNum)
    {
        if (newNum == oldNum)
            return null;

        return newNum;
    }
    
    internal static uint? GetChangesNumber(uint? newNum, uint? oldNum)
    {
        if (newNum == oldNum)
            return null;

        return newNum;
    }

    internal static int? GetChangesNumber(int? newNum, int? oldNum)
    {
        if (newNum == oldNum)
            return null;

        return newNum;
    }

    internal static float? GetChangesNumber(float? newFloat, float? oldFloat)
    {
        if (newFloat is not null && 
            oldFloat is not null && 
            Math.Abs((float)(newFloat - (float)oldFloat)) < 0.01)
            return null;

        return newFloat;
    }

    internal static string? GetChangesFormKey<T>(T? newObj, T? oldObj)
        where T : IFormKeyGetter
    {
        if (newObj is not null && 
            !newObj.FormKey.IsNull &&
            oldObj is not null &&
            !oldObj.FormKey.IsNull &&
            GetChanges(newObj, oldObj) is not null)
        {
            return newObj.FormKey.ToString();
        }

        return null;
    }
    
    internal static string? GetChangesString(object? newObj, object? oldObj)
    {
        return GetChanges(newObj, oldObj)?.ToString();
    }
    
    // If new is different from old, returns new. Otherwise returns null
    private static T? GetChanges<T>(T? newObj, T? oldObj)
    {
        if (newObj is null)
        {
            if (oldObj is null)
            {
                return default;
            }

            return default;
        }

        if (oldObj is null)
        {
            return newObj;
        }

        // both non-null
        return newObj.Equals(oldObj) 
            ? default 
            : newObj;
    }

    internal static string[]? GetChangesSet(ISet<string> newItems, IEnumerable<string> oldItems)
    {
        return newItems.SetEquals(oldItems) 
            ? null
            : newItems.ToArray() ;
    }

    internal static ISet<string> ToStringSet(IEnumerable<IFormLinkGetter<ISkyrimMajorRecordGetter>>? formLinks)
    {
        HashSet<string> output = new();
        if (formLinks != null)
        {
            foreach (var x in formLinks)
            {
                output.Add(x.FormKey.ToString());
            }
        }

        return output;
    }

    public static FormKey ToFormKey(this string? input)
    {
        return input is null 
            ? FormKey.Null 
            : FormKey.Factory(input);
    }
    
    public static bool IsNumericType(this object o)
    {   
        switch (Type.GetTypeCode(o.GetType()))
        {
            case TypeCode.Byte:
            case TypeCode.SByte:
            case TypeCode.UInt16:
            case TypeCode.UInt32:
            case TypeCode.UInt64:
            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
            case TypeCode.Decimal:
            case TypeCode.Double:
            case TypeCode.Single:
                return true;
            default:
                return false;
        }
    }
}