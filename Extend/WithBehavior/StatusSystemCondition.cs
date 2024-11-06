#if SCOR_ENABLE_BEHAVIOR
using StudioScor.Utilities;
using StudioScor.Utilities.UnityBehavior;
using Unity.Behavior;
using UnityEngine;

namespace StudioScor.StatusSystem.Behavior
{
    public abstract class StatusSystemCondition : BaseCondition
    {
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected IStatusSystem _statusSystem;

        public override void OnStart()
        {
            base.OnStart();

            var self = Self.Value;

            if (!self)
            {
                _statusSystem = null;
                LogError($"{nameof(Self).ToBold()} is Null.");
                return;
            }

            if (!self.TryGetStatusSystem(out _statusSystem))
            {
                LogError($"{nameof(Self).ToBold()} is Not Has {nameof(IStatusSystem).ToBold()}.");
                return;
            }
        }

        public override bool IsTrue()
        {
            var result = _statusSystem is not null;

            Log($"{nameof(Self).ToBold()} {(result ? $"Has {nameof(IStatusSystem)}".ToColor(SUtility.STRING_COLOR_SUCCESS) : $"Not Has {nameof(IStatusSystem).ToColor(SUtility.STRING_COLOR_FAIL)}").ToBold()}");

            return result;
        }
    }

}

#endif