using UnityEngine;

namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public class Status
    {
        #region Event
        public delegate void StatusChangedValueHandler(Status status, float currentValue, float prevValue);
        public delegate void StatusChangedStateHandler(Status status, EStatusState currentState, EStatusState prevState);
        #endregion

        [Header(" [ Status ] ")]
        [SerializeField] private string _Name;
        [Space(5f)]
        [SerializeField] private StatusTag _Tag;
        [Space(5f)]
        [SerializeField] private float _MaxValue;
        [SerializeField] private float _CurrentValue;

        private float _NormalizedValue;
        private EStatusState _CurrentState;

        public string Name => _Name;
        public float MaxValue => _MaxValue;
        public float CurrentValue => _CurrentValue;
        public float NormalizedValue => _NormalizedValue;
        public StatusTag Tag => _Tag;
        public EStatusState CurrentState => _CurrentState;

        public event StatusChangedValueHandler OnChangedMaxValue;
        public event StatusChangedValueHandler OnChangedValue;
        public event StatusChangedStateHandler OnChangedState;


        public Status(StatusTag statusTag, float maxValue, float currentValue, bool useRate = false)
        {
            _Name = statusTag.Name;
            _Tag = statusTag;

            float value = useRate ? maxValue * currentValue : currentValue;

            SetValue(maxValue, value);
        }        
        public void SetValue(float maxValue, float currentValue, bool useRate = false)
        {
            float value = useRate ? maxValue * currentValue : currentValue;

            SetMaxValue(maxValue);
            SetCurrentValue(value);
        }

        public void SetMaxValue(float maxValue, bool useRateValue = false, bool useRateChangeCurrentValue = false)
        {
            float prevMaxValue = _MaxValue;
            float newValue = useRateValue ? _MaxValue * maxValue : maxValue;

            _MaxValue = Mathf.Max(newValue, 0);

            if (prevMaxValue != _CurrentValue)
            {
                Callback_OnChangedMaxValue( prevMaxValue);

                if (useRateChangeCurrentValue)
                {
                    if (_MaxValue > prevMaxValue)
                    {
                        SetCurrentValue(_CurrentValue * (_MaxValue / prevMaxValue));
                    }
                    else
                    {
                        SetCurrentValue(_CurrentValue * (prevMaxValue / _MaxValue));
                    }
                }
            }
        }
        public void SetCurrentValue(float currentValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            float prevValue = _CurrentValue;
            float newValue = CalculateValue(currentValue, calculateType);

            _CurrentValue = Mathf.Clamp(newValue, 0, _MaxValue);

            if (prevValue != _CurrentValue)
            {
                _NormalizedValue = _CurrentValue / _MaxValue;

                Callback_OnChangedValue(prevValue);


                if (CurrentValue >= MaxValue)
                {
                    TransitionState(EStatusState.Fulled);
                }
                else if (CurrentValue <= 0f)
                {
                    TransitionState(EStatusState.Emptied);
                }
                else if (prevValue >= MaxValue || prevValue <= 0f)
                {
                    TransitionState(EStatusState.Consumed);
                }
            }
        }

        private void TransitionState(EStatusState newStatusState)
        {
            if (newStatusState == CurrentState)
                return;

            var prevState = CurrentState;
            _CurrentState = newStatusState;

            Callback_OnChangedState(prevState);
        }

        private float CalculateValue(float value, ECalculateType calculateType)
        {
            float calcValue;

            switch (calculateType)
            {
                case ECalculateType.Absolute:
                    calcValue = value;
                    break;
                case ECalculateType.RateMax:
                    calcValue = value * MaxValue;
                    break;
                case ECalculateType.RateCurret:
                    calcValue = value * CurrentValue;
                    break;
                default:
                    calcValue = 0f;
                    break;
            }

            return calcValue;
        }

        public void AddCurretValue(float addValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            if (addValue <= 0f)
                return;

            float value = _CurrentValue + CalculateValue(addValue, calculateType);

            SetCurrentValue(value);
        }
        public void SubtractCurrentValue(float subtractValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            if (subtractValue <= 0f)
                return;

            float value = _CurrentValue - CalculateValue(subtractValue, calculateType);

            SetCurrentValue(value);
        }

        public bool CanSubtractValue(float subtractValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            if (subtractValue <= 0)
                return true;

            float value = _CurrentValue - CalculateValue(subtractValue, calculateType);

            return value >= 0f;
        }

        #region Callback
        protected void Callback_OnChangedValue(float prevValue)
        {
            OnChangedValue?.Invoke(this, CurrentValue, prevValue);
        }
        protected void Callback_OnChangedMaxValue(float prevValue)
        {
            OnChangedMaxValue?.Invoke(this, MaxValue, prevValue);
        }
        protected void Callback_OnChangedState(EStatusState prevState)
        {
            OnChangedState?.Invoke(this, CurrentState, prevState);
        }
        #endregion
    }
}