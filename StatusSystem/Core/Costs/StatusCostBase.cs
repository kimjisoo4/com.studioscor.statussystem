using UnityEngine;

namespace StudioScor.StatusSystem
{

    [System.Serializable]
    public abstract class BaseStatusCost
    {
        private readonly Status _Status;

        public Status Status => _Status;
        public StatusTag Tag => _Status.Tag;
        public abstract FStatusCost Cost { get; }

        public BaseStatusCost(StatusSystemComponent statusSystem, StatusTag statusTag)
        {
            _Status = statusSystem.GetOrCreateStatus(statusTag);
        }

        public void ConsumeCost()
        {
            if (_Status == null)
                return;

            float value = Cost.useRateValue ? Cost.RateValue : Cost.Value;

            _Status.SubtractCurrentValue(value, Cost.Type);
        }

        public bool CanConsumeCost()
        {
            if (_Status == null)
                return false;

            float value = Cost.useRateValue ? Cost.RateValue : Cost.Value;

            return _Status.CanSubtractValue(value, Cost.Type);
        }
    }

}

