using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    [AddComponentMenu("StudioScor/StatusSystem/StatusSystem Component", order: 0)]
    public class StatusSystemComponent : MonoBehaviour
    {
        #region Events
        public delegate void StatusEventHandler(StatusSystemComponent statusSystem, Status status);
        #endregion

        [SerializeField] private InitializationStatuses _InitializationStatuses;
        private Dictionary<StatusTag, Status> _Statuses = new Dictionary<StatusTag, Status>();

        public IReadOnlyDictionary<StatusTag, Status> Statuses => _Statuses;

        public event StatusEventHandler OnGrantedStatus;

        private void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            _Statuses = new();

            SetupInitializationStatuses();

            OnSetup();
        }
        public void ResetStatusSystem()
        {
            SetupInitializationStatuses();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }

        
        private void SetupInitializationStatuses()
        {
            if (!_InitializationStatuses)
                return;

            foreach (var initStatus in _InitializationStatuses.Statuses)
            {
                SetOrCreateStatus(initStatus.Tag, initStatus.MaxValue, initStatus.MaxValue, initStatus.UseRate);
            }
        }

        public Status SetOrCreateStatus(StatusTag tag, float maxValue, float currentValue, bool useRate = false)
        {
            if (!Statuses.TryGetValue(tag, out Status status))
            {
                status = new Status(tag, maxValue, currentValue, useRate);

                _Statuses.Add(tag, status);

                Callback_OnGrantedStatus(status);
            }
            else
            {
                status.SetValue(maxValue, currentValue);
            }

            return status;
        }

        #region Get Status
        public bool TryGetStatus(StatusTag tag, out Status status)
        {
            if (!tag)
            {
                status = null;

                return false;
            }

            if (Statuses.TryGetValue(tag, out status))
                return true;

            return false;
        }
        public Status GetStatus(StatusTag Tag)
        {
            if (!Tag)
            {
                return null;
            }

            if (Statuses.TryGetValue(Tag, out Status value))
                return value;

            return null;
        }

        public Status GetOrCreateStatus(StatusTag Tag, float minValue = 0f, float maxValue = 0f)
        {
            if (!Tag)
            {
                return null;
            }

            if (Statuses.TryGetValue(Tag, out Status value))
            {
                return value;
            }
            else
            {
                value = new Status(Tag, minValue, maxValue);

                _Statuses.Add(Tag, value);

                Callback_OnGrantedStatus(value);

                return value;
            }
        }
        public Status GetOrCreateStatus(FInitializationStatus Status)
        {
            return GetOrCreateStatus(Status.Tag, Status.CurrentValue, Status.MaxValue);
        }

        #endregion

        #region Callback
        protected void Callback_OnGrantedStatus(Status status)
        {
            OnGrantedStatus?.Invoke(this, status);
        }
        #endregion
    }
}