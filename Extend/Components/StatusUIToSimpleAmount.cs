using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.StatusSystem
{
    public class StatusUIToSimpleAmount : BaseMonoBehaviour
    {
        [Header(" [ Status UI To Simple Amount ] ")]
        [SerializeField] private StatusSystemComponent _StatusSystem;
        [SerializeField] private StatusTag _StatusTag;
        [SerializeField] private SimpleAmountComponent _SImpleAmount;

        private void Reset()
        {
            gameObject.TryGetComponentInParentOrChildren(out _SImpleAmount);
        }

        private void Awake()
        {
            Setup();
        }

        private void Setup()
        {
            if(!_SImpleAmount)
            {
                if(!gameObject.TryGetComponentInParentOrChildren(out _SImpleAmount))
                {
                    Log(" Simple Amount Is NULL!!", true);
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

            _StatusSystem = statusSystemComponent;

            SetupStatusSystem();
        }

        private void ResetStatusSystem()
        {
            if (_StatusSystem)
            {
                var status = _StatusSystem.GetOrCreateStatus(_StatusTag);

                status.OnChangedMaxValue -= Status_OnChangedMaxValue;
                status.OnChangedValue -= Status_OnChangedValue;
            }
        }
        private void SetupStatusSystem()
        {
            if (_StatusSystem)
            {
                var status = _StatusSystem.GetOrCreateStatus(_StatusTag);

                _SImpleAmount.SetValue(status.CurrentValue, status.MaxValue);

                status.OnChangedMaxValue += Status_OnChangedMaxValue;
                status.OnChangedValue += Status_OnChangedValue;
            }
            else
            {
                _SImpleAmount.SetValue(0f, 0f);
            }
        }

        private void Status_OnChangedValue(Status status, float currentValue, float prevValue)
        {
            _SImpleAmount.SetCurrentValue(currentValue);
        }

        private void Status_OnChangedMaxValue(Status status, float currentValue, float prevValue)
        {
            _SImpleAmount.SetMaxValue(currentValue);
        }
    }
}