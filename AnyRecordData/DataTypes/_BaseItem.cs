using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;

public abstract class DataBaseItem
{
    // Which record to replace (FormKey)
    public string? Id { get; set; }
    
    [YamlIgnore]
    public string PatchFileName { get; set; } = "";

    public abstract void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef);

    public abstract void Patch(ISkyrimMajorRecord rec);
    
    public abstract bool IsModified();
}