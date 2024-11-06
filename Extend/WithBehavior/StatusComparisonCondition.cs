#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace StudioScor.StatusSystem.Behavior
{
    [Serializable, GeneratePropertyBag]
    [Condition(name: "Status Comparison",
       story: "[Self] [StatusTag] Status [TargetValue] is [Operator] to [Value] ( UseDebug [UseDebugKey] )",
       category: "Conditions/StudioScor/StatusSystem",
       id: "playersystem_statuscomparison")]
    public partial class StatusComparisonCondition : StatusSystemCondition
    {
        public enum ETarget
        {
            MaxValue,
            CurrentValue,
            NormalizedValue,
        }

        [SerializeReference] public BlackboardVariable<StatusTag> StatusTag = new();
        [SerializeReference] public BlackboardVariable<ETarget> TargetValue = new(ETarget.NormalizedValue);
        [Comparison(comparisonType: ComparisonType.All)]
        [SerializeReference] public BlackboardVariable<ConditionOperator> Operator = new(ConditionOperator.LowerOrEqual);
        [SerializeReference] public BlackboardVariable<float> Value = new(0f);

        public override bool IsTrue()
        {
            if (!base.IsTrue())
                return false;

            if (!StatusTag.Value)
            {
                Log($"{nameof(StatusTag).ToBold()} Is {"Null".ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}");
                return false;
            }

            if (!_statusSystem.TryGetStatus(StatusTag.Value, out Status status))
            {
                Log($"{nameof(Self).ToBold()} Is {$"Not Has {StatusTag.Value}".ToColor(SUtility.STRING_COLOR_FAIL).ToBold()}");
                return false;
            }

            bool result = false;
            float targetValue = 0;

            switch (TargetValue.Value)
            {
                case ETarget.MaxValue:
                    targetValue = status.MaxValue;
                    break;
                case ETarget.CurrentValue:
                    targetValue = status.CurrentValue;
                    break;
                case ETarget.NormalizedValue:
                    targetValue = status.NormalizedValue;
                    break;
            }

            switch (Operator.Value)
            {
                case ConditionOperator.Equal:
                    result = targetValue.SafeEquals(Value.Value);
                    break;
                case ConditionOperator.NotEqual:
                    result = !targetValue.SafeEquals(Value.Value);
                    break;
                case ConditionOperator.Greater:
                    result = targetValue > Value.Value;
                    break;
                case ConditionOperator.Lower:
                    result = targetValue < Value.Value;
                    break;
                case ConditionOperator.GreaterOrEqual:
                    result = targetValue >= Value.Value;
                    break;
                case ConditionOperator.LowerOrEqual:
                    result = targetValue <= Value.Value;
                    break;
                default:
                    break;
            }

            Log($"{status.Tag} {TargetValue.Value} is {$"{(result ? "" : "Not ")} {Operator.Value}".ToBold().ToColor(result ? SUtility.STRING_COLOR_SUCCESS : SUtility.STRING_COLOR_FAIL)} to {Value.Value.ToString().ToBold()}");

            return result;
        }
    }

}

#endif