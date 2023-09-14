using AnyRecordData.Interfaces;
using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataShout : DataBaseItem, 
                         IHasName, 
                         IHasDescription,
                         IHasMenuDisplayObject
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? MenuObject { get; set; }
    
    // Shout specific
    [UsedImplicitly] public List<DataWord>? Words { get; set; }

    public DataShout()
    {
        PatchFileName = "Shouts";
    }

    public override void GetData(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IShoutGetter x && oldRef is IShoutGetter y)
            GetData(x, y);
    }

    private void GetData(IShoutGetter newRef, IShoutGetter oldRef)
    {
        ((IHasName)this).GetDataInterface(newRef, oldRef);
        ((IHasDescription)this).GetDataInterface(newRef, oldRef);
        ((IHasMenuDisplayObject)this).GetDataInterface(newRef, oldRef);

        if (!newRef.WordsOfPower.SequenceEqual(oldRef.WordsOfPower))
        {
            CopyWordsOfPower(newRef);
        }
    }

    private void CopyWordsOfPower(IShoutGetter newRef)
    {
        Words ??= new List<DataWord>();
        foreach (IShoutWordGetter shout in newRef.WordsOfPower)
        {
            DataWord dataWord = new()
            {
                Word = shout.Word.FormKey.ToString(),
                Spell = shout.Spell.FormKey.ToString(),
                RecoveryTime = shout.RecoveryTime
            };
            Words.Add(dataWord);
        }
    }

    public override void Patch(ISkyrimMajorRecord rec)
    {
        if (rec is IShout recShout)
            Patch(recShout);
    }

    private void Patch(IShout rec)
    {
        ((IHasName)this).PatchInterface(rec);
        ((IHasDescription)this).PatchInterface(rec);
        ((IHasMenuDisplayObject)this).PatchInterface(rec);

        if (Words is null) 
            return;
        
        rec.WordsOfPower.Clear();
        foreach (DataWord dataWord in Words)
        {
            ShoutWord newWord = new()
            {
                Spell = new FormLink<ISpellGetter>(dataWord.Spell.ToFormKey()),
                Word = new FormLink<IWordOfPowerGetter>(dataWord.Word.ToFormKey()),
                RecoveryTime = dataWord.RecoveryTime ?? 0.0f
            };
            rec.WordsOfPower.Add(newWord);
        }
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               ((IHasDescription)this).IsModifiedInterface() ||
               ((IHasMenuDisplayObject)this).IsModifiedInterface() ||
               Words is not null;
    }
}

public class DataWord
{
    public string? Word;
    public string? Spell;
    public float? RecoveryTime;
}