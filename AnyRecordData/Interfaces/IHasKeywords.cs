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
        KeywordsDeleted = Utility.GetDeleted(newRef.Keywords, oldRef.Keywords);
        
        if (KeywordsDeleted is null)
            Keywords = Utility.GetChangesSet(Utility.ToStringSet(newRef.Keywords), Utility.ToStringSet(oldRef.Keywords));
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
            rec.Keywords.Add(new FormLink<IKeywordGetter>(kwd.ToFormKey()));
        }
    }

    public bool IsModifiedInterface()
    {
        return Keywords is not null ||
               KeywordsDeleted is not null;
    }
}