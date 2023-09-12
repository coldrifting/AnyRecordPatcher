using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;

namespace AnyRecordData.DataTypes.Conditions;

public static partial class DataConditionParser
{
    private static int GetIntValue(string? value)
    {
        return int.Parse(value ?? "-1");
    }

    private static float GetFloatValue(string? value)
    {
        return float.Parse(value ?? "-1.0f");
    }

    private static FormLinkOrIndex<IClassGetter> GetClass<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IClassGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IFactionGetter> GetFaction<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IFactionGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IKeywordGetter> GetKeyword<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IKeywordGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<INpcGetter> GetNpc<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<INpcGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IPlacedNpcGetter> GetPlacedNpc<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IPlacedNpcGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IPlacedSimpleGetter> GetPlacedSimple<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IPlacedSimpleGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IItemOrListGetter> GetItemOrList<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IItemOrListGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IGlobalGetter> GetGlobal<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IGlobalGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<ICellGetter> GetCell<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<ICellGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<ILocationGetter> GetLocation<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<ILocationGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<ILocationReferenceTypeGetter> GetLocationReferenceType<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<ILocationReferenceTypeGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IReferenceableObjectGetter> GetReferenceableObject<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IReferenceableObjectGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IEquipTypeGetter> GetEquipType<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IEquipTypeGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IFormListGetter> GetFormList<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IFormListGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IWorldspaceOrListGetter> GetWorldspaceOrList<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IWorldspaceOrListGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IEncounterZoneGetter> GetEncounterZone<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IEncounterZoneGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IPackageGetter> GetPackage<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IPackageGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IWeatherGetter> GetWeather<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IWeatherGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IVoiceTypeOrListGetter> GetVoiceTypeOrList<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IVoiceTypeOrListGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IRaceGetter> GetRace<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IRaceGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IQuestGetter> GetQuest<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IQuestGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IMagicItemGetter> GetMagicItem<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IMagicItemGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IAssociationTypeGetter> GetAssociationType<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IAssociationTypeGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IPerkGetter> GetPerk<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IPerkGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IShoutGetter> GetShout<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IShoutGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<ISpellGetter> GetSpell<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<ISpellGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IFurnitureGetter> GetFurniture<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IFurnitureGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<ISceneGetter> GetScene<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<ISceneGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IOwnerGetter> GetOwner<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IOwnerGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IKnowableGetter> GetKnowable<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IKnowableGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IRegionGetter> GetRegion<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IRegionGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IIdleAnimationGetter> GetIdleAnimation<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IIdleAnimationGetter>(new T(), value.ToFormKey());
    }

    private static FormLinkOrIndex<IMagicEffectGetter> GetMagicEffect<T>(string? value)
        where T : ConditionData, new()
    {
        return new FormLinkOrIndex<IMagicEffectGetter>(new T(), value.ToFormKey());
    }

    private static ActorValue GetActorValue(string? value)
    {
        return Enum.Parse<ActorValue>(value ?? "Alchemy");
    }

    private static Axis GetAxis(string? value)
    {
        return Enum.Parse<Axis>(value ?? "X");
    }

    private static MaleFemaleGender GetGender(string? value)
    {
        return Enum.Parse<MaleFemaleGender>(value ?? "Male");
    }

    private static CastSource GetCastSource(string? param1)
    {
        return Enum.Parse<CastSource>(param1 ?? "Left");
    }

    private static FurnitureAnimType GetFurnitureAnimType(string? value)
    {
        return Enum.Parse<FurnitureAnimType>(value ?? "Sit");
    }

    private static FurnitureEntryType GetFurnitureEntryType(string? value)
    {
        return Enum.Parse<FurnitureEntryType>(value ?? "Front");
    }

    private static PlayerAction GetPlayerAction(string? param1)
    {
        return Enum.Parse<PlayerAction>(param1 ?? "Jumping");
    }
}