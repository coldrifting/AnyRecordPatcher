using System.Globalization;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Noggog;

namespace AnyRecordData.DataTypes;
using Conditions;

public class DataCondition
{
    public string? Type;
    public string? Function;
    public string? Parameter1;
    public string? Parameter2;
    public string? CompareType;
    public string? CompareResult;
    public string? CompareLink;
    public string? Reference;
    
    public Condition ConvertToPatch()
    {
        Condition cnd;
        var f = FormKey.TryFactory(CompareResult);
        if (f is not null)
        {
            cnd = new ConditionGlobal()
            {
                ComparisonValue = new FormLink<IGlobalGetter>((FormKey)f),
                CompareOperator = Enum.Parse<CompareOperator>(CompareType ?? "EqualTo")
            };
        }
        else
        {
            cnd = new ConditionFloat()
            {
                ComparisonValue = Convert.ToSingle(CompareResult),
                CompareOperator = Enum.Parse<CompareOperator>(CompareType ?? "EqualTo")
            };
        }

        ConditionData cd = DataConditionParser.ImportConditionData(Function ?? "", Parameter1, Parameter2);
        cd.RunOnType = Enum.Parse<Condition.RunOnType>(Type ?? "Subject");

        if (Reference is not null)
        {
            cd.Reference = new FormLink<ISkyrimMajorRecordGetter>(Reference.ToFormKey());
        }
                        
        cnd.Data = cd;
        cnd.Flags = cnd.Flags.SetFlag(Condition.Flag.OR, CompareLink == "OR");

        return cnd;
    }
    
    public static DataCondition ConvertToData(IConditionGetter cond)
    {
        DataCondition dataCondition = new()
        {
            Type = cond.Data.RunOnType.ToString(),
            Function = cond.Data.Function.ToString(),
            CompareType = cond.CompareOperator.ToString(),
            CompareLink = (cond.Flags & Condition.Flag.OR) != 0 ? "OR" : "AND",
            Reference = cond.Data.Reference.FormKey.ToString()
        };

        (dataCondition.Parameter1, dataCondition.Parameter2) = DataConditionParser.GetConditionParameters(cond.Data);

        dataCondition.CompareResult = cond switch
        {
            IConditionFloatGetter cf => cf.ComparisonValue.ToString(CultureInfo.InvariantCulture),
            IConditionGlobalGetter cg => cg.ComparisonValue.FormKey.ToString(),
            _ => dataCondition.CompareResult
        };

        return dataCondition;
    }

    public static List<DataCondition> CopyConditions(IEnumerable<IConditionGetter> newRefConditions)
    {
        return newRefConditions.Select(ConvertToData).ToList();
    }
}