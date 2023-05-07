using UnityEngine;
using System.Collections.Generic;

using StudioScor.Utilities;

namespace StudioScor.StatusSystem
{
    public delegate void StatusEventHandler(IStatusSystem statusSystem, Status status);
    public delegate void ChangedStatusValueEventHandler(IStatusSystem statusSystem, Status status, float currentValue, float prevValue);
    public delegate void ChangedStatusStateEventHander(IStatusSystem statusSystem, Status status, EStatusState currentState, EStatusState prevState);

    public static class StatusSystemUtility
    {
        public static IStatusSystem GetStatusSystem(this GameObject gameObject)
        {
            return gameObject.GetComponent<IStatusSystem>();
        }
        public static IStatusSystem GetStatusSystem(this Transform transform)
        {
            return transform.GetComponent<IStatusSystem>();
        }
        public static IStatusSystem GetStatusSystem(this Component component)
        {
            var statusSystem = component as IStatusSystem;

            if (statusSystem is not null)
                return statusSystem;

            return component.GetComponent<IStatusSystem>();
        }
        public static bool TryGetStatusSystem(this GameObject gameObject, out IStatusSystem statusSystem)
        {
            return gameObject.TryGetComponent(out statusSystem);
        }
        public static bool TryGetStatusSystem(this Transform transform, out IStatusSystem statusSystem)
        {
            return transform.TryGetComponent(out statusSystem);
        }
        public static bool TryGetStatusSystem(this Component component, out IStatusSystem statusSystem)
        {
            statusSystem = component as IStatusSystem;

            if (statusSystem is not null)
                return true;

            return component.TryGetComponent(out statusSystem);
        }

        public static Status GetStatus(this IStatusSystem statusSystem, StatusTag statusTag)
        {
            return statusSystem.Statuses[statusTag];
        }
        public static bool TryGetStatus(this IStatusSystem statusSystem, StatusTag statusTag, out Status status)
        {
            return statusSystem.Statuses.TryGetValue(statusTag, out status);
        }
    }

    public interface IStatusSystem
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public IReadOnlyDictionary<StatusTag, Status> Statuses { get; }
        public Status GetOrCreateStatus(StatusTag tag, float maxValue = 0f, float currentValue = 1f, bool useRate = true);
        public Status SetOrCreateStatus(StatusTag tag, float maxValue = 0f, float currentValue = 1f, bool useRate = true);

        public event StatusEventHandler OnGrantedStatus;

        public event ChangedStatusValueEventHandler OnChangedStatusValue;
        public event ChangedStatusValueEventHandler OnChangedStatusMaxValue;
        public event ChangedStatusStateEventHander OnChangedStatusState;
    }

    [AddComponentMenu("StudioScor/StatusSystem/StatusSystem Component", order: 0)]
    public class StatusSystemComponent : BaseMonoBehaviour, IStatusSystem
    {
        [Header(" [ Status System Component ] ")]
        [SerializeField] private InitializationStatuses _InitializationStatuses;
        private readonly Dictionary<StatusTag, Status> _Statuses = new Dictionary<StatusTag, Status>();
        public IReadOnlyDictionary<StatusTag, Status> Statuses => _Statuses;

        public event StatusEventHandler OnGrantedStatus;
        public event ChangedStatusValueEventHandler OnChangedStatusValue;
        public event ChangedStatusValueEventHandler OnChangedStatusMaxValue;
        public event ChangedStatusStateEventHander OnChangedStatusState;
        private void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
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
                SetOrCreateStatus(initStatus.Tag, initStatus.MaxValue, initStatus.CurrentValue, initStatus.UseRate);
            }
        }

        public Status SetOrCreateStatus(StatusTag tag, float maxValue, float currentValue = 1f, bool useRate = true)
        {
            if (!tag)
                return null;

            if (Statuses.TryGetValue(tag, out Status status))
            {
                status.SetValue(maxValue, currentValue, useRate);

                return status;
            }
            else
                return CreateStatus(tag, maxValue, currentValue, useRate);
        }
        public Status GetOrCreateStatus(StatusTag tag, float maxValue = 0f, float currentValue = 1f, bool useRate = true)
        {
            if (!tag)
                return null;

            if (Statuses.TryGetValue(tag, out Status value))
                return value;
            else
                return CreateStatus(tag, maxValue, currentValue, useRate);
        }

        public Status CreateStatus(StatusTag tag, float maxValue = 0f, float currentValue = 1f, bool useRate = true)
        {
            var status = new Status(tag, maxValue, currentValue, useRate);

            _Statuses.Add(tag, status);

            Callback_OnGrantedStatus(status);

            status.OnChangedValue += Callback_OnChangedStatusValue;
            status.OnChangedMaxValue += Callback_OnChangedStatusMaxValue;
            status.OnChangedState += Callback_OnChangedStatusState;

            return status;
        }

        #region Callback
        protected void Callback_OnGrantedStatus(Status status)
        {
            Log($"On Granted Status - [ Status : {status.Name}]");

            OnGrantedStatus?.Invoke(this, status);
        }
        protected void Callback_OnChangedStatusState(Status status, EStatusState currentState, EStatusState prevState)
        {
            Log($"On Changed Status State - [ Status : {status.Name} | Current : {currentState} | Prev {prevState} ]");
            
            OnChangedStatusState?.Invoke(this, status, currentState, prevState);
        }
        protected void Callback_OnChangedStatusValue(Status status, float currentValue, float prevValue)
        {
            Log($"On Changed Status Value - [ Status : {status.Name} | Current : {currentValue:N2} | Prev {prevValue:N2} ]");

            OnChangedStatusValue?.Invoke(this, status, currentValue, prevValue);
        }
        protected void Callback_OnChangedStatusMaxValue(Status status, float currentValue, float prevValue)
        {
            Log($"On Changed Status Max Value - [ Status : {status.Name} | Current : {currentValue:N2} | Prev {prevValue:N2} ]");

            OnChangedStatusMaxValue?.Invoke(this, status, currentValue, prevValue);
        }
        #endregion
    }
}