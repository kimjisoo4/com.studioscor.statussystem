#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace StudioScor.StatusSystem.Behavior
{
    [Serializable, GeneratePropertyBag]
    [Condition(name: "Has Status",
       story: "[Self] Has [StatusTag] Status ( UseDebug [UseDebugKey] )",
       category: "Conditions/StudioScor/StatusSystem",
       id: "playersystem_hasstatus")]
    public partial class HasStatusCondition : StatusSystemCondition
    {
        [SerializeReference] public BlackboardVariable<StatusTag> StatusTag = new();
        public override bool IsTrue()
        {
            if (!base.IsTrue())
                return false;

            if (!StatusTag.Value)
            {
                Log($"{nameof(StatusTag).ToBold()} Is {"Null".ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}");
                return false;
            }

            var result = _statusSystem.HasStatus(StatusTag.Value);

            Log($"{nameof(Self).ToBold()} Is {(result ? $"Has {StatusTag.Value}".ToColor(SUtility.STRING_COLOR_SUCCESS) : $"Not Has {StatusTag.Value}".ToColor(SUtility.STRING_COLOR_FAIL)).ToBold()}");

            return result;
        }
    }

}

#endif