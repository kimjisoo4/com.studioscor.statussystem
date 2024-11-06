using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.StatusSystem
{
    public class StatusUIToSimpleAmount : BaseMonoBehaviour
    {
        [Header(" [ Status UI To Simple Amount ] ")]
        [SerializeField] private StatusSystemComponent _statusSystem;
        [SerializeField] private StatusTag _statusTag;
        [SerializeField] private SimpleAmountComponent _simpleAmount;

        private void Reset()
        {
            gameObject.TryGetComponentInParentOrChildren(out _simpleAmount);
        }

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            if(!_simpleAmount)
            {
                if(!gameObject.TryGetComponentInParentOrChildren(out _simpleAmount))
                {
                    LogError(" Simple Amount Is NULL!!");
                }
            }
        }

        private void Start()
        {
            SetupStatusSystem();
        }

        public void SetTarget(GameObject gameObject)
        {
            if (gameObject && gameObject.TryGetComponent(out StatusSystemComponent statusSystem))
            {
                SetTarget(statusSystem);
            }
            else
            {
                SetTarget();
            }
        }
        public void SetTarget(Transform transform)
        {
            if(transform && transform.TryGetComponent(out StatusSystemComponent statusSystem))
            {
                SetTarget(statusSystem);
            }
            else
            {
                SetTarget();
            }
        }

        public void SetTarget(Component component)
        {
            if (component && component.TryGetComponent(out StatusSystemComponent statusSystem))
                SetTarget(statusSystem);
            else
                SetTarget();
        }

        public void SetTarget(StatusSystemComponent statusSystemComponent = null)
        {
            ResetStatusSystem();

            _statusSystem = statusSystemComponent;

            SetupStatusSystem();
        }

        private void ResetStatusSystem()
        {
            if (_statusSystem)
            {
                var status = _statusSystem.GetOrCreateStatus(_statusTag);

                status.OnChangedMaxValue -= Status_OnChangedMaxValue;
                status.OnChangedValue -= Status_OnChangedValue;
            }
        }
        private void SetupStatusSystem()
        {
            if (_statusSystem)
            {
                var status = _statusSystem.GetOrCreateStatus(_statusTag);

                Log($"{nameof(SetupStatusSystem)} :: {status.CurrentValue} / {status.MaxValue}");

                _simpleAmount.SetValue(status.CurrentValue, status.MaxValue);

                status.OnChangedMaxValue += Status_OnChangedMaxValue;
                status.OnChangedValue += Status_OnChangedValue;
            }
            else
            {
                _simpleAmount.SetValue(0f, 0f);
            }
        }

        private void Status_OnChangedValue(Status status, float currentValue, float prevValue)
        {
            _simpleAmount.SetCurrentValue(currentValue);
        }

        private void Status_OnChangedMaxValue(Status status, float currentValue, float prevValue)
        {
            _simpleAmount.SetMaxValue(currentValue);
        }
    }
}