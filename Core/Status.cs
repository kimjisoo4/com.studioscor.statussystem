using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.StatusSystem
{
    public class Status
    {
        #region Event
        public delegate void StatusChangedValueHandler(Status status, float currentValue, float prevValue);
        public delegate void StatusChangedStateHandler(Status status, EStatusState currentState, EStatusState prevState);
        #endregion

        private readonly StatusTag _tag;
        private float _maxValue = -1f;
        private float _currentValue = -1f;
        private float _normalizedValue = -1f;
        private EStatusState _currentState = EStatusState.None;

        public StatusTag Tag => _tag;
        public float MaxValue => _maxValue;
        public float CurrentValue => _currentValue;
        public float NormalizedValue => _normalizedValue;
        public EStatusState CurrentState => _currentState;


        public event StatusChangedValueHandler OnChangedMaxValue;
        public event StatusChangedValueHandler OnChangedValue;
        public event StatusChangedStateHandler OnChangedState;


        public Status(StatusTag tag, float maxValue, float currentValue, bool useRate = false)
        {
            _tag = tag;

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
            float prevMaxValue = _maxValue;
            float newValue = useRateValue ? _maxValue * maxValue : maxValue;

            _maxValue = Mathf.Max(newValue, 0);

            if (!_maxValue.SafeEquals(prevMaxValue))
            {
                Invoke_OnChangedMaxValue(prevMaxValue);

                if (useRateChangeCurrentValue)
                    SetCurrentValue(_currentValue * (_maxValue / prevMaxValue));
            }
        }
        public void SetCurrentValue(float currentValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            float prevValue = _currentValue;
            float newValue = CalculateValue(currentValue, calculateType);

            _currentValue = Mathf.Clamp(newValue, 0, _maxValue);

            if (!_currentValue.SafeEquals(prevValue))
            {
                _normalizedValue = _currentValue.SafeDivide(_maxValue);

                Invoke_OnChangedValue(prevValue);

                if (MaxValue > 0f && CurrentValue.SafeEquals(MaxValue))
                {
                    TransitionState(EStatusState.Fulled);
                }
                else if (_currentValue.SafeEquals(0f))
                {
                    TransitionState(EStatusState.Emptied);
                }
                else
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
            _currentState = newStatusState;

            Invoke_OnChangedState(prevState);
        }

        private float CalculateValue(float value, ECalculateType calculateType)
        {
            float calcValue;

            switch (calculateType)
            {
                case ECalculateType.Absolute:
                    calcValue = value;
                    break;
                case ECalculateType.RatioMax:
                    calcValue = value * MaxValue;
                    break;
                case ECalculateType.RatioCurrent:
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

            float value = _currentValue + CalculateValue(addValue, calculateType);

            SetCurrentValue(value);
        }
        public void SubtractCurrentValue(float subtractValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            if (subtractValue <= 0f)
                return;

            float value = _currentValue - CalculateValue(subtractValue, calculateType);

            SetCurrentValue(value);
        }

        public bool CanSubtractValue(float subtractValue, ECalculateType calculateType = ECalculateType.Absolute)
        {
            if (subtractValue <= 0)
                return true;

            float value = _currentValue - CalculateValue(subtractValue, calculateType);

            return value >= 0f;
        }

        #region Invoke
        protected void Invoke_OnChangedValue(float prevValue)
        {
            OnChangedValue?.Invoke(this, CurrentValue, prevValue);
        }
        protected void Invoke_OnChangedMaxValue(float prevValue)
        {
            OnChangedMaxValue?.Invoke(this, MaxValue, prevValue);
        }
        protected void Invoke_OnChangedState(EStatusState prevState)
        {
            OnChangedState?.Invoke(this, CurrentState, prevState);
        }
        #endregion
    }
}