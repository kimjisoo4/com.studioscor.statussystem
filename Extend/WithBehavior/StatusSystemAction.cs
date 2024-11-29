#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities;
using StudioScor.Utilities.UnityBehavior;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.StatusSystem.Behavior
{
    public class StatusSystemAction : BaseAction
    {
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected IStatusSystem _statusSystem;

        protected override Status OnStart()
        {
            if (base.OnStart() == Status.Failure)
                return Status.Failure;

            var self = Self.Value;

            if (!self)
            {
                LogError($"{nameof(Self).ToBold()} is {"Null".ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}.");
                return Status.Failure;
            }

            if (!self.TryGetStatusSystem(out _statusSystem))
            {
                LogError($"{nameof(Self).ToBold()} is Not Has {nameof(_statusSystem).ToBold().ToColor(SUtility.STRING_COLOR_FAIL)}.");
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}

#endif