using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public partial class DataBook : DataBaseItem,
                        IHasName,
                        IHasKeywords,
                        IHasWeightValue,
                        IHasObjectBounds,
                        IHasModel,
                        IHasPickUpPutDownSound
{
    public string? Name { get; set; }
    public List<string>? Keywords { get; set; }
    public string? ModelFile { get; set; }
    public List<DataAltTexSet>? ModelTextures { get; set; }
    public short[]? Bounds { get; set; }
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    
    // Book Specific
    [YamlMember(ScalarStyle = ScalarStyle.Literal)] 
    public string? BookText { get; set; }
    
    [UsedImplicitly] public string? InventoryArt { get; set; }
    [UsedImplicitly] public string? Type { get; set; }

    public DataBook()
    {
        PatchFileName = "Books";
    }
    
    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IBookGetter x && oldRef is IBookGetter y)
            GetChanges(x, y);
    }

    private void GetChanges(IBookGetter newRef, IBookGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasKeywords)this).GetDataInterface(newRef, oldRef);
        ((IHasModel)this).GetDataInterface(newRef, oldRef);
        ((IHasObjectBounds)this).GetDataInterface(newRef, oldRef);
        ((IHasWeightValue)this).GetDataInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).GetDataInterface(newRef, oldRef);

        string newTextNoWhiteSpace = RemoveWhiteSpace().Replace(newRef.BookText.String ?? "", "");
        string oldTextNoWhiteSpace = RemoveWhiteSpace().Replace(oldRef.BookText.String ?? "", "");

        if (!newTextNoWhiteSpace.Equals(oldTextNoWhiteSpace))
            BookText = newRef.BookText.String;
        
        InventoryArt = DataUtils.GetString(newRef.InventoryArt, oldRef.InventoryArt);
        Type = DataUtils.GetString(newRef.Type, oldRef.Type);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IBook recBook)
            Patch(recBook);
    }

    private void Patch(IBook rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);

        // Prevent extra conflicts in xEdit due to line ending differences
        if (BookText != null && !BookText.Contains("\r\n"))
            BookText = BookText?.Replace("\n", "\r\n");
        
        rec.BookText = DataUtils.PatchString(rec.BookText, BookText);

        DataUtils.PatchFormLink(rec.InventoryArt, InventoryArt);

        if (Type is not null)
            rec.Type = Enum.Parse<Book.BookType>(Type);
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               BookText is not null ||
               InventoryArt is not null ||
               Type is not null;
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex RemoveWhiteSpace();
}