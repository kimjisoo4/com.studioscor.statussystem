using UnityEngine;

namespace KimScor.StatusSystem
{
    public enum EConsumeType
    {
        [Tooltip("절대값으로 스테이터스를 차감함")] Absolute,
        [Tooltip("최대 값을 비율로 차감함")] RatioInMax,
        [Tooltip("현재 값을 비율로 차감함")] RatioInCurret,
    }

    [System.Serializable]
    public class StatusCost
    {
        [SerializeField] private EConsumeType _ConsumeType;
        [SerializeField] private float _ConsumePoint;
        [SerializeField] private float _ConsumeRatioPoint;
        [SerializeField] private Status _Status;
        public StatusCost(StatusSystem statusSystem, StatusTag statusTag, EConsumeType consumeType, float consumePoint, float consumeRatioPoint)
        {
            _Status = statusSystem.GetOrCreateValue(statusTag);

            _ConsumeType = consumeType;
            _ConsumePoint = consumePoint;
            _ConsumeRatioPoint = consumeRatioPoint;
        }
        public EConsumeType ConsumeType => _ConsumeType;
        public float ConsumePoint => _ConsumePoint;
        public float ConsumeRatioPoint => _ConsumeRatioPoint;

        public void ConsumeCost()
        {
            if (_Status == null)
                return;

            _Status.DamagePoint(CurrentConsumeCost());
        }

        public bool CanConsumeCost()
        {
            if (_Status == null)
                return false;

            float consumePoint = CurrentConsumeCost();

            if (consumePoint < 0)
                return false;

            return _Status.CurrentPoint >= consumePoint;
        }

        public float CurrentConsumeCost()
        {
            if (_Status == null)
                return -1f;

            switch (ConsumeType)
            {
                case EConsumeType.Absolute:
                    return ConsumePoint;
                case EConsumeType.RatioInMax:
                    return _Status.MaxPoint * ConsumeRatioPoint * 0.01f;
                case EConsumeType.RatioInCurret:
                    return _Status.CurrentPoint * ConsumeRatioPoint * 0.01f;
                default:
                    return -1f;
            }
        }
    }

    [System.Serializable]
    public class StatusCostSpec
    {
        private StatusCostSO _StatusCost;
        public StatusTag StatusTag => _StatusCost.StatusTag;
        public EConsumeType ConsumeType => _StatusCost.ConsumeType;
        public float ConsumePoint => _StatusCost.ConsumePoint;
        public float ConsumeRatioPoint => _StatusCost.ConsumeRatioPoint;

        private StatusSystem _StatusSystem;
        public StatusSystem StatusSystem => _StatusSystem;

        private Status _Status;

        public Status Status
        {
            get
            {
                if (_Status == null)
                {
                    if (!StatusSystem)
                        return null;

                    _Status = StatusSystem.GetOrCreateValue(_StatusCost.StatusTag);
                }

                return _Status;
            }
        }
        
        public StatusCostSpec(StatusCostSO statusCostSO, StatusSystem statusSystem)
        {
            _StatusCost = statusCostSO;
            _StatusSystem = statusSystem;
        }

        public void ConsumeCost()
        {
            if (!StatusSystem || Status == null)
                return;

            Status.DamagePoint(CurrentConsumeCost());
        }

        public bool CanConsumeCost()
        {
            if (!StatusSystem || Status == null)
                return false;

            float consumePoint = CurrentConsumeCost();

            if (consumePoint < 0)
                return false;

            return Status.CurrentPoint >= consumePoint;
        }

        public float CurrentConsumeCost()
        {
            if (!StatusSystem || Status == null)
                return -1f;

            switch (ConsumeType)
            {
                case EConsumeType.Absolute:
                    return ConsumePoint;
                case EConsumeType.RatioInMax:
                    return Status.MaxPoint * ConsumeRatioPoint * 0.01f;
                case EConsumeType.RatioInCurret:
                    return Status.CurrentPoint * ConsumeRatioPoint * 0.01f;
                default:
                    return -1f;
            }
        }
    }
}

