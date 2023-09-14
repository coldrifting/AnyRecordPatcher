using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasMenuDisplayObject
{
    public string? MenuObject { get; set; }

    public void GetDataInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        MenuObject = newRef switch
        {
            IScrollGetter => DataUtils.GetString(newRef.AsScroll().MenuDisplayObject, oldRef.AsScroll().MenuDisplayObject),
            IShoutGetter => DataUtils.GetString(newRef.AsShout().MenuDisplayObject, oldRef.AsShout().MenuDisplayObject),
            ISpellGetter => DataUtils.GetString(newRef.AsSpell().MenuDisplayObject, oldRef.AsSpell().MenuDisplayObject),
            _ => default
        };
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        switch (rec)
        {
            case IScroll item: DataUtils.PatchFormLink(item.MenuDisplayObject, MenuObject); break;
            case IShout  item: DataUtils.PatchFormLink(item.MenuDisplayObject, MenuObject); break;
            case ISpell  item: DataUtils.PatchFormLink(item.MenuDisplayObject, MenuObject); break;
        }
    }

    public bool IsModifiedInterface()
    {
        return MenuObject is not null;
    }
}