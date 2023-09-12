using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasKeywords
{
    public string[]? Keywords { get; set; }
    public bool? KeywordsDeleted { get; set; }

    public void SaveChangesInterface(IKeywordedGetter<IKeywordGetter> newRef, IKeywordedGetter<IKeywordGetter> oldRef)
    {
        if (newRef.Keywords is null && oldRef.Keywords is not null)
        {
            KeywordsDeleted = true;
            return;
        }
        
        var newKeywords = Utility.ToStringSet(newRef.Keywords);
        var oldKeywords = Utility.ToStringSet(oldRef.Keywords);

        if (!newKeywords.SetEquals(oldKeywords)) 
            Keywords = newKeywords.ToArray();
    }
    
    public void PatchInterface<T>(T rec)
        where T : IKeyworded<IKeywordGetter>
    {
        if (KeywordsDeleted == true)
        {
            rec.Keywords = null;
            return;
        }
        
        if (Keywords is null) return;
        
        rec.Keywords ??= new ExtendedList<IFormLinkGetter<IKeywordGetter>>();
        rec.Keywords.Clear();
        foreach (string kwd in Keywords)
        {
            rec.Keywords.Add(new FormLink<IKeywordGetter>(FormKey.Factory(kwd)));
        }
    }

    public bool IsModifiedInterface()
    {
        return Keywords is not null ||
               KeywordsDeleted is not null;
    }
}