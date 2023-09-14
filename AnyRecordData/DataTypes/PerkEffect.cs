using JetBrains.Annotations;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes;

public class DataPerkEffect
{
    // Header
    [UsedImplicitly] public string? Type { get; set; }
    [UsedImplicitly] public byte? Rank { get; set; }
    [UsedImplicitly] public byte? Priority { get; set; }

    // Perk Conditions
    [UsedImplicitly] public List<DataPerkEffectCondition>? PerkEffectConditions { get; set; }
    
    // Data
    [UsedImplicitly] public string? EntryPointType { get; set; }
    [UsedImplicitly] public byte? TabCount { get; set; }

    [UsedImplicitly] public string? ModType { get; set; }
    [UsedImplicitly] public float? ModValue { get; set; }

    [UsedImplicitly] public string? Spell { get; set; }

    [UsedImplicitly] public string? ButtonLabel { get; set; }
    [UsedImplicitly] public ushort? FragIndex { get; set; }
    [UsedImplicitly] public uint? ScriptFlags { get; set; }

    [UsedImplicitly] public string? ActorValue { get; set; }

    // PerkEntryPointSelectText
    [UsedImplicitly] public string? Text { get; set; }

    [UsedImplicitly] public string? Quest { get; set; }
    [UsedImplicitly] public byte? Stage { get; set; }
    
    [UsedImplicitly] public string? Ability { get; set; }

    public APerkEffect ConvertToPatch()
    {
        APerkEffect output = Type switch
        {
            "PerkEntryPointModifyValue" => new PerkEntryPointModifyValue()
            {
                EntryPoint = Enum.Parse<APerkEntryPointEffect.EntryType>(EntryPointType ?? ""),
                PerkConditionTabCount = TabCount ?? 0,
                
                Modification = Enum.Parse<PerkEntryPointModifyValue.ModificationType>(ModType ?? ""),
                Value = ModValue,
            },
            "PerkEntryPointSelectSpell" => new PerkEntryPointSelectSpell()
            {
                EntryPoint = Enum.Parse<APerkEntryPointEffect.EntryType>(EntryPointType ?? ""),
                PerkConditionTabCount = TabCount ?? 0,

                Spell = new FormLink<ISpellGetter>(Spell.ToFormKey())
            },
            "PerkEntryPointAddActivateChoice" => new PerkEntryPointAddActivateChoice()
            {
                EntryPoint = Enum.Parse<APerkEntryPointEffect.EntryType>(EntryPointType ?? ""),
                PerkConditionTabCount = TabCount ?? 0,

                ButtonLabel = ButtonLabel,
                Spell = new FormLinkNullable<ISpellGetter>(Spell.ToFormKey()),
                Flags = new PerkScriptFlag
                {
                    FragmentIndex = FragIndex ?? 0,
                    Flags = (PerkScriptFlag.Flag)(ScriptFlags ?? 0)
                }
            },
            "PerkEntryPointModifyActorValue" => new PerkEntryPointModifyActorValue()
            {
                EntryPoint = Enum.Parse<APerkEntryPointEffect.EntryType>(EntryPointType ?? ""),
                PerkConditionTabCount = TabCount ?? 0,

                Modification = Enum.Parse<PerkEntryPointModifyActorValue.ModificationType>(ModType ?? ""),
                Value = ModValue ?? 0.0f,
                ActorValue = Enum.Parse<ActorValue>(ActorValue ?? "Alchemy")
            },
            "PerkEntryPointSelectText" => new PerkEntryPointSelectText()
            {
                EntryPoint = Enum.Parse<APerkEntryPointEffect.EntryType>(EntryPointType ?? ""),
                PerkConditionTabCount = TabCount ?? 0,
                
                Text = Text ?? ""
            },
            "PerkQuestEffect" => new PerkQuestEffect()
            {
                Quest = new FormLink<IQuestGetter>(Quest.ToFormKey()),
                Stage = Stage ?? 0
            },
            "PerkAbilityEffect" => new PerkAbilityEffect()
            {
                Ability = new FormLink<ISpellGetter>(Ability.ToFormKey())
            },
            _ => throw new Exception()
        };

        output.Priority = Priority ?? 0;
        
        if (PerkEffectConditions is null) 
            return output;

        output.Conditions.Clear();
        foreach (DataPerkEffectCondition perkCond in PerkEffectConditions)
        {
            PerkCondition pk = new()
            {
                RunOnTabIndex = perkCond.RunOnTabIndex ?? 0
            };

            if (perkCond.Conditions is null) 
                continue;
                
            pk.Conditions.Clear();
            foreach (DataCondition dataCond in perkCond.Conditions)
            {
                pk.Conditions.Add(dataCond.ConvertToPatch());
            }

            output.Conditions.Add(pk);
        }

        return output;
    }
    
    public static DataPerkEffect ConvertToData(IAPerkEffectGetter perkEffect)
    {
        DataPerkEffect dataPerkEffect = new()
        {
            Priority = perkEffect.Priority,
            Rank = perkEffect.Rank,
            PerkEffectConditions = new List<DataPerkEffectCondition>()
        };

        foreach (IPerkConditionGetter perkCondition in perkEffect.Conditions)
        {
            DataPerkEffectCondition cond = new()
            {
                RunOnTabIndex = perkCondition.RunOnTabIndex,
                Conditions = DataCondition.CopyConditions(perkCondition.Conditions)
            };
            dataPerkEffect.PerkEffectConditions.Add(cond);
        }

        switch (perkEffect)
        {
            case IPerkEntryPointModifyValue entry:
                dataPerkEffect.Type = "PerkEntryPointModifyValue";
                dataPerkEffect.EntryPointType = entry.EntryPoint.ToString();
                dataPerkEffect.TabCount = entry.PerkConditionTabCount;
                dataPerkEffect.ModType = entry.Modification.ToString();
                dataPerkEffect.ModValue = entry.Value;
                break;
            
            case IPerkEntryPointSelectSpell entry:
                dataPerkEffect.Type = "PerkEntryPointSelectSpell";
                dataPerkEffect.EntryPointType = entry.EntryPoint.ToString();
                dataPerkEffect.TabCount = entry.PerkConditionTabCount;
                dataPerkEffect.Spell = entry.Spell.FormKey.ToString();
                break;
            
            case IPerkEntryPointAddActivateChoice entry:
                dataPerkEffect.Type = "PerkEntryPointAddActivateChoice";
                dataPerkEffect.EntryPointType = entry.EntryPoint.ToString();
                dataPerkEffect.TabCount = entry.PerkConditionTabCount;
                dataPerkEffect.ButtonLabel = entry.ButtonLabel;
                dataPerkEffect.Spell = entry.Spell.FormKey.ToString();
                dataPerkEffect.ScriptFlags = (uint)entry.Flags.Flags;
                dataPerkEffect.FragIndex = entry.Flags.FragmentIndex;
                break;
            
            case IPerkEntryPointModifyActorValue entry:
                dataPerkEffect.Type = "PerkEntryPointModifyActorValue";
                dataPerkEffect.EntryPointType = entry.EntryPoint.ToString();
                dataPerkEffect.TabCount = entry.PerkConditionTabCount;
                dataPerkEffect.ModType = entry.Modification.ToString();
                dataPerkEffect.ModValue = entry.Value;
                dataPerkEffect.ActorValue = entry.ActorValue.ToString();
                break;
            
            case IPerkEntryPointSelectText entry:
                dataPerkEffect.Type = "PerkEntryPointSelectText";
                dataPerkEffect.EntryPointType = entry.EntryPoint.ToString();
                dataPerkEffect.TabCount = entry.PerkConditionTabCount;
                dataPerkEffect.Text = entry.Text;
                break;
            
            case IPerkQuestEffect quest:
                dataPerkEffect.Type = "PerkQuestEffect";
                dataPerkEffect.Quest = quest.Quest.FormKey.ToString();
                dataPerkEffect.Stage = quest.Stage;
                break;
            
            case IPerkAbilityEffect ability:
                dataPerkEffect.Type = "PerkAbilityEffect";
                dataPerkEffect.Ability = ability.Ability.FormKey.ToString();
                break;
            
            default:
                throw new Exception("Perk Effect Case not yet covered");
        }

        return dataPerkEffect;
    }
}

public class DataPerkEffectCondition
{
    public byte? RunOnTabIndex { get; set; }
    public List<DataCondition>? Conditions { get; set; }
}