using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.Interfaces;
using DataTypes;

public interface IHasMagicEffects
{
    public List<DataMagicEffect>? Effects { get; set; }
    
    public void SaveChangesInterface(ISkyrimMajorRecordGetter newRef, ISkyrimMajorRecordGetter oldRef)
    {
        switch (newRef, oldRef)
        {
            case (ISpellGetter, ISpellGetter) x: 
                CopyEffects(((ISpellGetter)x.newRef).Effects, ((ISpellGetter)x.oldRef).Effects);
                break;
                
            case (IIngestibleGetter, IIngestibleGetter) x: 
                CopyEffects(((IIngestibleGetter)x.newRef).Effects, ((IIngestibleGetter)x.oldRef).Effects);
                break;
                
            case (IIngredientGetter, IIngredientGetter) x: 
                CopyEffects(((IIngredientGetter)x.newRef).Effects, ((IIngredientGetter)x.oldRef).Effects);
                break;
        }
    }

    private void CopyEffects(IEnumerable<IEffectGetter> newEffects, IEnumerable<IEffectGetter> oldEffects)
    {
        var effectGetters = newEffects as IEffectGetter[] ?? newEffects.ToArray();
        if (effectGetters.ToHashSet().SetEquals(oldEffects))
            return;
        
        Effects = new List<DataMagicEffect>();
        foreach (IEffectGetter effect in effectGetters)
        {
            DataMagicEffect dataMagicEffect = new()
            {
                BaseEffect = effect.BaseEffect.FormKey.ToString(),
                Magnitude = effect.Data?.Magnitude ?? 0,
                Area = effect.Data?.Area ?? 0,
                Duration = effect.Data?.Duration ?? 0,
                Conditions = new List<DataCondition>()
            };

            foreach (IConditionGetter condition in effect.Conditions)
            {
                dataMagicEffect.Conditions.Add(DataCondition.ConvertToData(condition));
            }

            Effects.Add(dataMagicEffect);
        }
    }

    public void PatchInterface(ISkyrimMajorRecord rec)
    {
        if (Effects is null) 
            return;
        
        switch (rec)
        {
            case ISpell spell: 
                spell.Effects.Clear();
                spell.Effects.AddRange(GetEffects());
                break;
            
            case IIngestible ingestible: 
                ingestible.Effects.Clear();
                ingestible.Effects.AddRange(GetEffects());
                break;
            
            case IIngredient ingredient: 
                ingredient.Effects.Clear();
                ingredient.Effects.AddRange(GetEffects());
                break;
        }
    }

    private IEnumerable<Effect> GetEffects()
    {
        List<Effect> output = new();
        if (Effects is null)
            return output;
        
        foreach (DataMagicEffect dataEffect in Effects)
        {
            Effect fx = new()
            {
                BaseEffect = new FormLinkNullable<IMagicEffectGetter>(dataEffect.BaseEffect.ToFormKey())
            };

            fx.Data ??= new EffectData();
            fx.Data.Magnitude = dataEffect.Magnitude;
            fx.Data.Area = dataEffect.Area;
            fx.Data.Duration = dataEffect.Duration;

            if (dataEffect.Conditions is not null)
            {
                foreach (DataCondition c in dataEffect.Conditions)
                {
                    fx.Conditions.Add(c.ConvertToPatch());
                } 
            }

            output.Add(fx);
        }

        return output;
    }

    public bool IsModifiedInterface()
    {
        return Effects is not null;
    }
}

public class DataMagicEffect
{
    public string BaseEffect = "";
    public float Magnitude;
    public int Area;
    public int Duration;
    
    public List<DataCondition>? Conditions;
}