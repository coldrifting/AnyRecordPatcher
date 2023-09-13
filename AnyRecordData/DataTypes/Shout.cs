﻿using AnyRecordData.Interfaces;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataShout : BaseItem, IHasName
{
    public string? Name { get; set; }
    public bool? NameDeleted { get; set; }
    
    // Shout specific
    public string? Description { get; set; }
    public string? MenuObject { get; set; }

    public List<DataWord>? Words { get; set; }

    public DataShout()
    {
        PatchFileName = "Shouts";
    }

    public override void SaveChanges(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        if (newRef is IShoutGetter x && oldRef is IShoutGetter y)
            SaveChanges(x, y);
    }

    private void SaveChanges(IShoutGetter newRef, IShoutGetter oldRef)
    {
        ((IHasName)this).SaveChangesInterface(newRef, oldRef);
        
        string newDesc = newRef.Description?.String ?? "";
        string oldDesc = oldRef.Description?.String ?? "";
        if (newDesc != oldDesc)
        {
            Description = newDesc;
        }
        
        if (!newRef.MenuDisplayObject.FormKey.ToString().Equals(oldRef.MenuDisplayObject.FormKey.ToString()))
            MenuObject = newRef.MenuDisplayObject.FormKey.ToString();

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

        if (Description is not null)
            rec.Description = Description;

        if (MenuObject is not null)
            rec.MenuDisplayObject = new FormLinkNullable<IStaticGetter>(FormKey.Factory(MenuObject));
        
        if (Words is not null)
        {
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
    }

    public override bool IsModified()
    {
        return ((IHasName)this).IsModifiedInterface() ||
               Description is not null ||
               MenuObject is not null ||
               Words is not null;
    }
}

public class DataWord
{
    public string? Word;
    public string? Spell;
    public float? RecoveryTime;
}