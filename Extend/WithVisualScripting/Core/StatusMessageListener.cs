#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public class StatusMessageListener : MessageListener
    {
        private StatusSystemComponent _StatusSystem;
        private List<StatusTag> _StatusTags;

        private void Awake()
        {
            _StatusSystem = GetComponent<StatusSystemComponent>();
            _StatusTags = new();
        }
        private void OnDestroy()
        {
            if (!_StatusSystem)
                return;

            foreach (var tag in _StatusTags)
            {
                if (_StatusSystem.TryGetStatus(tag, out Status status))
                {
                    status.OnChangedMaxValue -= Status_OnChangedMaxValue;
                    status.OnChangedState -= Status_OnChangedState;
                    status.OnChangedValue -= Status_OnChangedValue;
                }
            }

            _StatusSystem = null;
        }

        private void Status_OnChangedMaxValue(Status status, float currentValue, float prevValue)
        {
            var statusChangedValue = new ChangedStatusValue(status, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_MAX_VALUE, status), statusChangedValue);
        }

        private void Status_OnChangedState(Status status, EStatusState currentState, EStatusState prevState)
        {
            var statusChangedState = new ChangedStatusState(status, currentState, prevState);
            
            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_STATE, status), statusChangedState);
        }

        private void Status_OnChangedValue(Status status, float currentValue, float prevValue)
        {
            var statusChangedValue = new ChangedStatusValue(status, currentValue, prevValue);

            EventBus.Trigger(new EventHook(StatusSystemWithVisualScripting.STATUS_ON_CHANGED_VALUE, status), statusChangedValue);
        }

        public void TryAddEventBus(StatusTag statusTag)
        {
            if (_StatusTags.Contains(statusTag))
                return;

            _StatusTags.Add(statusTag);

            var status = _StatusSystem.GetStatus(statusTag);

            status.OnChangedMaxValue += Status_OnChangedMaxValue;
            status.OnChangedState += Status_OnChangedState;
            status.OnChangedValue += Status_OnChangedValue;
        }
    }
}

#endif