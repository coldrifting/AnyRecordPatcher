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
}