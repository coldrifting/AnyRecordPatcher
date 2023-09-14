using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.Interfaces;

public interface IHasKeywords
{
    public List<string>? Keywords { get; set; }

    public void GetDataInterface(IKeywordedGetter<IKeywordGetter> newRef, IKeywordedGetter<IKeywordGetter> oldRef)
    {
        Keywords = DataUtils.GetDataFormLinkList(newRef.Keywords, oldRef.Keywords);
    }
    
    public void PatchInterface<T>(T rec)
        where T : IKeyworded<IKeywordGetter>
    {
        DataUtils.PatchFormLinkList(rec.Keywords ??= new ExtendedList<IFormLinkGetter<IKeywordGetter>>(), Keywords);
        if (rec.Keywords.Count == 0)
            rec.Keywords = null;
    }

    public bool IsModifiedInterface()
    {
        return Keywords is not null;
    }
}