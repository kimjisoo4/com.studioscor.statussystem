#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities;
using System;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.StatusSystem.Behavior
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [NodeDescription(name: "Set Or Create Status", story: "[Self] Set Or Create [StatusTag]", category: "Action/StudioScor/StatusSystem", id: "playersystem_setorcreatestatus")]
    public class SetOrCreateStatusAction : StatusSystemAction
    {
        [SerializeReference] public BlackboardVariable<StatusTag> StatusTag;
        [SerializeReference] public BlackboardVariable<float> MaxValue = new(100f);
        [SerializeReference] public BlackboardVariable<float> CurrentValue = new(1f);
        [SerializeReference] public BlackboardVariable<bool> UseRatio = new(true);

        protected override Status OnStart()
        {
            if (base.OnStart() == Status.Failure)
                return Status.Failure;

            if (!StatusTag.Value)
            {
                Log($"{nameof(StatusTag).ToBold()} is {"Null".ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}.");
                return Status.Failure;
            }

            _statusSystem.SetOrCreateStatus(StatusTag.Value, MaxValue.Value, CurrentValue.Value, UseRatio.Value);

            return Status.Success;
        }
    }
}

#endif