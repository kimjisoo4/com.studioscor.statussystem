using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] private StatusSystemComponent _StatusSystem;
        [SerializeField] private StatusTag _Tag;
        [SerializeField] private List<StatusUIModifier> _Modifiers = new List<StatusUIModifier>();

        private Status _Status;

        private void OnEnable()
        {
            Setup();
        }
        private void OnDisable()
        {
            if (!Application.isPlaying)
                return;

            SetStatusSystem(null);
        }

        public void SetStatusSystem(StatusSystemComponent statusSystem = null)
        {
            if (_StatusSystem != null)
            {
                _Status.OnChangedValue -= Status_OnChangedPoint;
            }

            _StatusSystem = statusSystem;

            Setup();
        }
        private void Setup()
        {
            if (!_StatusSystem)
                _StatusSystem = GetComponentInParent<StatusSystemComponent>();

            if (_StatusSystem != null)
            {
                _Status = _StatusSystem.GetOrCreateStatus(_Tag);

                Status_OnChangedPoint(_Status, _Status.CurrentValue);

                _Status.OnChangedValue += Status_OnChangedPoint;
            }
        }

        public void AddModifier(StatusUIModifier modifier)
        {
            _Modifiers.Add(modifier);
        }
        public void RemoveModifire(StatusUIModifier modifier)
        {
            _Modifiers.Remove(modifier);
        }

        protected void Status_OnChangedPoint(Status status, float currentPoint, float prevPoint = 0)
        {
            foreach (StatusUIModifier modifier in _Modifiers)
            {
                modifier.StatusUpdate(status, currentPoint, prevPoint);
            }
        }
    }
}