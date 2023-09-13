using Mutagen.Bethesda.Skyrim;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace AnyRecordData.DataTypes;
using Interfaces;

public class DataBook : BaseItem,
                        IHasName,
                        IHasKeywords,
                        IHasWeightValue,
                        IHasObjectBounds,
                        IHasModel,
                        IHasPickUpPutDownSound
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }
    
    public string? ModelPath { get; set; }
    public bool? ModelPathDeleted { get; set; }
    public AltTexSet[]? ModelTextures { get; set; }
    
    public short[]? Bounds { get; set; }
    public bool? BoundsDeleted { get; set; }
    
    public float? Weight { get; set; }
    public uint? Value { get; set; }
    
    public string? PickUpSound { get; set; }
    public string? PutDownSound { get; set; }
    public bool? PickUpSoundDeleted { get; set; }
    public bool? PutDownSoundDeleted { get; set; }
    
    // Book Specific
    [YamlMember(ScalarStyle = ScalarStyle.Plain)] public string? Text { get; set; }
    [YamlMember] public string? InventoryArt { get; set; }
    [YamlMember] public string? BookType { get; set; }

    public DataBook()
    {
        PatchFileName = "Books";
    }
    
    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IBookGetter x && oldRef is IBookGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IBookGetter newRef, IBookGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        ((IHasKeywords)this).SaveChangesInterface(newRef, oldRef);
        ((IHasModel)this).SaveChangesInterface(newRef, oldRef);
        ((IHasObjectBounds)this).SaveChangesInterface(newRef, oldRef);
        ((IHasWeightValue)this).SaveChangesInterface(newRef, oldRef);
        ((IHasPickUpPutDownSound)this).SaveChangesInterface(newRef, oldRef);

        Text = Utility.GetChangesString(newRef.BookText, oldRef.BookText);
        InventoryArt = Utility.GetChangesString(newRef.InventoryArt, oldRef.InventoryArt);
        BookType = Utility.GetChangesString(newRef.Type, oldRef.Type);
    }
    
    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IBook recBook)
            Patch(recBook);
    }

    public void Patch(IBook rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasKeywords)this).PatchInterface(rec);
        ((IHasModel)this).PatchInterface(rec);
        ((IHasObjectBounds)this).PatchInterface(rec);
        ((IHasWeightValue)this).PatchInterface(rec);
        ((IHasPickUpPutDownSound)this).PatchInterface(rec);

        if (Text is not null)
            rec.BookText = Text;

        if (BookType is not null)
            rec.Type = Enum.Parse<Book.BookType>(BookType);

        if (InventoryArt is not null)
            rec.InventoryArt.SetTo(InventoryArt.ToFormKey());
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasKeywords)this).IsModifiedInterface() ||
               ((IHasModel)this).IsModifiedInterface() ||
               ((IHasObjectBounds)this).IsModifiedInterface() ||
               ((IHasWeightValue)this).IsModifiedInterface() ||
               ((IHasPickUpPutDownSound)this).IsModifiedInterface() ||
               Text is not null ||
               InventoryArt is not null ||
               BookType is not null;
    }
}