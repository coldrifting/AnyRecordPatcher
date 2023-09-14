using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.DataTypes;

public class DataAltTexSet
{
    [UsedImplicitly] public string Name { get; set; }
    [UsedImplicitly] public string Texture { get; set; }
    [UsedImplicitly] public int Index { get; set; }

    public DataAltTexSet()
    {
        Name = "";
        Texture = "";
        Index = -1;
    }
    
    private DataAltTexSet(string name, string texture, int index)
    {
        Name = name;
        Texture = texture;
        Index = index;
    }

    public static List<DataAltTexSet>? GetTextureList(IReadOnlyList<IAlternateTextureGetter>? newList, IReadOnlyList<IAlternateTextureGetter>? oldList)
    {
        if (newList is null)
        {
            return oldList is null 
                ? null 
                : new List<DataAltTexSet>();
        }

        var newSet = newList.ToHashSet();
        if (oldList is null || !newSet.SetEquals(oldList))
        {
            return ReadAltTextures(newList);
        }

        return null;
    }

    public static void PatchModelTextures(ExtendedList<AlternateTexture>? recList, IEnumerable<DataAltTexSet> dataTextures)
    {
        recList?.Clear();
        recList?.AddRange(dataTextures.Select(dataTex => dataTex.ToPatch()));
    }

    private AlternateTexture ToPatch()
    {
        return new AlternateTexture()
        {
            Name = Name,
            NewTexture = new FormLink<ITextureSetGetter>(Texture.ToFormKey()),
            Index = Index
        };
    }

    private static DataAltTexSet FromPatch(IAlternateTextureGetter at)
    {
        return new DataAltTexSet(at.Name, at.NewTexture.FormKey.ToString(), at.Index);
    }

    private static List<DataAltTexSet> ReadAltTextures(IEnumerable<IAlternateTextureGetter> list)
    {
        return list.Select(FromPatch).ToList();
    }
}