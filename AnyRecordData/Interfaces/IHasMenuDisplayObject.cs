using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;

public interface IHasMenuDisplayObject
{
    public string? MenuObject { get; set; }
    public bool? MenuObjectDeleted { get; set; }

    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef, oldRef)
        {
            case (IScrollGetter, IScrollGetter) x:
                SaveChangesMenuObject(
                    ((IScrollGetter)x.newRef).MenuDisplayObject, ((IScrollGetter)x.oldRef).MenuDisplayObject);
                break;
            
            case (IShoutGetter, IShoutGetter) x:
                SaveChangesMenuObject(
                    ((IShoutGetter)x.newRef).MenuDisplayObject, ((IShoutGetter)x.oldRef).MenuDisplayObject);
                break;
            
            case (ISpellGetter, ISpellGetter) x:
                SaveChangesMenuObject(
                    ((ISpellGetter)x.newRef).MenuDisplayObject, ((ISpellGetter)x.oldRef).MenuDisplayObject);
                break;
        }
    }
    
    private void SaveChangesMenuObject(
        IFormKeyGetter first,
        IFormKeyGetter second)
    {
        string newMenuObject = first.FormKey.ToString();
        string oldMenuObject = second.FormKey.ToString();
        if (newMenuObject is "Null" && oldMenuObject is not "Null")
        {
            MenuObjectDeleted = true;
        }

        if (newMenuObject is not "Null" && newMenuObject != oldMenuObject)
        {
            MenuObject = newMenuObject;
        }
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        if (MenuObject is null && MenuObjectDeleted is null)
            return;
        
        FormKey menuObject = FormKey.Null;
        if (MenuObject is not null)
            menuObject = MenuObject.ToFormKey();
        
        switch (rec)
        {
            case IScroll x:
                x.MenuDisplayObject.SetTo(menuObject);
                break;
            
            case IShout x:
                x.MenuDisplayObject.SetTo(menuObject);
                break;
            
            case ISpell x:
                x.MenuDisplayObject.SetTo(menuObject);
                break;
        }
    }

    public bool IsModifiedInterface()
    {
        return MenuObject is not null ||
               MenuObjectDeleted is not null;
    }
}