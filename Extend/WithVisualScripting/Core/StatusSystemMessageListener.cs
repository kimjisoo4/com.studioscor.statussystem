#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public class StatusSystemMessageListener : MessageListener
    {
        private void Awake()
        {
            var statusSystem = transform.GetStatusSystem();

            statusSystem.OnGrantedStatus += StatusSystem_OnGrantedStatus;
            statusSystem.OnChangedStatusValue += StatusSystem_OnChangedStatusValue;
            statusSystem.OnChangedStatusMaxValue += StatusSystem_OnChangedStatusMaxValue;
            statusSystem.OnChangedStatusState += StatusSystem_OnChangedStatusState;
        }
        private void OnDestroy()
        {
            if(transform.TryGetStatusSystem(out IStatusSystem statusSystem))
            {
                statusSystem.OnGrantedStatus -= StatusSystem_OnGrantedStatus;
                statusSystem.OnChangedStatusValue -= StatusSystem_OnChangedStatusValue;
                statusSystem.OnChangedStatusMaxValue -= StatusSystem_OnChangedStatusMaxValue;
                statusSystem.OnChangedStatusState -= StatusSystem_OnChangedStatusState;
            }
        }

        private void StatusSystem_OnGrantedStatus(IStatusSystem statusSystem, Status status)
        {
            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_GRANTED_STATUS, statusSystem), status);
        }
        private void StatusSystem_OnChangedStatusValue(IStatusSystem statusSystem, Status status, float currentValue, float prevValue)
        {
            var statusChangedValue = new ChangedStatusValue(status, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_VALUE, statusSystem), statusChangedValue);
        }
        private void StatusSystem_OnChangedStatusMaxValue(IStatusSystem statusSystem, Status status, float currentValue, float prevValue)
        {
            var statusChangedValue = new ChangedStatusValue(status, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_MAX_VALUE, statusSystem), statusChangedValue);
        }
        private void StatusSystem_OnChangedStatusState(IStatusSystem statusSystem, Status status, EStatusState currentState, EStatusState prevState)
        {
            var statusChangedState = new ChangedStatusState(status, currentState, prevState);

            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_STATE, statusSystem), statusChangedState);
        }
    }
}

#endif