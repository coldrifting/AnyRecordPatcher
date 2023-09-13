using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;

public abstract class BaseItem
{
    // Which record to replace (FormKey)
    public string? Id { get; set; }
    
    [YamlIgnore]
    public string PatchFileName { get; set; } = "";

    public abstract void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef);

    public abstract void Patch(ISkyrimMajorRecord rec);
    
    public abstract bool IsModified();
}

public class AltTexSet
{
    public string Name { get; init; }
    public string Texture { get; init; }
    public int Index { get; init; }

    public AltTexSet()
    {
        Name = "";
        Texture = "";
        Index = -1;
    }
    
    public AltTexSet(string name, string texture, int index)
    {
        Name = name;
        Texture = texture;
        Index = index;
    }

    public static AltTexSet FromPatch(IAlternateTextureGetter at)
    {
        return new AltTexSet(at.Name, at.NewTexture.FormKey.ToString() ?? string.Empty, at.Index);
    }

    public AlternateTexture ToPatch()
    {
        return new AlternateTexture()
        {
            Name = Name,
            NewTexture = new FormLink<ITextureSetGetter>(Texture.ToFormKey()),
            Index = Index
        };
    }

    public static AltTexSet[]? CompareTextureLists(IReadOnlyList<IAlternateTextureGetter>? newList, IReadOnlyList<IAlternateTextureGetter>? oldList)
    {
        if (newList is null)
        {
            return oldList is null 
                ? null 
                : Array.Empty<AltTexSet>();
        }

        var newListTexArr = ReadAltTextures(newList);
        if (oldList is null)
        {
            return newListTexArr;
        }
            
        var oldListTexArr = ReadAltTextures(oldList).ToHashSet();
        return oldListTexArr.SetEquals(newListTexArr) 
            ? null 
            : newListTexArr;
    }

    public static AltTexSet[] ReadAltTextures(IReadOnlyList<IAlternateTextureGetter> list)
    {
        var arr = new AltTexSet[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            arr[i] = FromPatch(list[i]);
        }

        return arr;
    }
}