using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData;

public static partial class DataUtils
{
    public const string Delete = "[DELETE]";
    
    // Empty string means no change
    public static string? GetDataString<T>(T? newObj, T? oldObj)
    {
        if (newObj != null && newObj.Equals(oldObj))
        {
            return string.Empty;
        }
        
        if (newObj is null && oldObj is null)
            return null;

        if (newObj is null)
            return string.Empty;
        
        if (newObj is IFormKeyGetter x)
            return x.FormKey.ToString();
            
        return newObj.ToString();

    }

    public static string? PatchString(string? record, string? value)
    {
        if (value is null)
            return record;
        
        if (value != Delete)
            return value;
        
        return null;
    }
    
    public static void PatchFormLink<T>(T record, string? formKey)
        where T : IFormLinkNullable<ISkyrimMajorRecordGetter>
    {
        if (formKey is null)
            return;
        
        if (formKey == "")
            record.SetToNull();

        record.SetTo(formKey.ToFormKey());
    }
}