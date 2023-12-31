﻿using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Strings;

namespace AnyRecordData;

// Helper functions for dealing with strings and form keys
public static partial class DataUtils
{
    private const string Delete = "Null";
    
    public static string? GetString<T>(T? newObj, T? oldObj)
    {
        // Both null or both equal
        if (AreEqual(newObj, oldObj))
        {
            return null;
        }
        
        // New null, old is not
        if (newObj is null && oldObj is not null)
            return Delete;

        if (newObj is IFormLinkGetter<ISkyrimMajorRecordGetter> newLink)
        {
            return newLink.FormKey.ToString();
        }

        return newObj?.ToString();
    }

    public static string? PatchString(string? oldValue, string? newValue)
    {
        if (newValue is null)
            return oldValue;
        
        return newValue != Delete 
            ? newValue 
            : null;
    }

    public static string PatchStringNonNull(string oldValue, string? newValue)
    {
        if (newValue is null)
            return oldValue;
        
        return newValue != Delete 
            ? newValue 
            : "";
    }
    
    public static void PatchFormLink<T>(T record, string? formKey)
        where T : IFormLink<ISkyrimMajorRecordGetter>
    {
        switch (formKey)
        {
            case null:
                return;
            
            case "":
                record.SetTo(null);
                return;
            
            default:
                record.SetTo(formKey.ToFormKey());
                break;
        }
    }

    public static FormKey ToFormKey(this string? input)
    {
        if (input is null)
            return FormKey.Null;
        
        return FormKey.TryFactory(input, out FormKey result) 
             ? result 
             : FormKey.Null;
    }

    private static bool AreEqual<T>(T? o1, T? o2)
    {
        if (IsNullOrEmpty(o1))
        {
            return IsNullOrEmpty(o2);
        }

        return o1 != null && o1.Equals(o2);
    }

    private static bool IsNullOrEmpty(object? obj)
    {
        if (obj is null)
            return true;

        if (obj.ToString()!.ToLower().Equals("null") ||
             obj.ToString()!.ToLower().StartsWith("null:") ||
             obj.ToString()!.ToLower().StartsWith("null<"))
        {
            return true;
        }
        
        return obj switch
        {
            string str => string.IsNullOrEmpty(str),
            FormLink<ISkyrimMajorRecordGetter> formLink => formLink.IsNull,
            TranslatedString str => string.IsNullOrEmpty(str.String),
            _ => false
        };
    }
}