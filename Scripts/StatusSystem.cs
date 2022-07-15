using UnityEngine;
using System.Collections.Generic;

namespace KimScor.StatusSystem
{
    public class StatusSystem : MonoBehaviour
    {
        #region Events
        public delegate void StatusEventHandler(StatusSystem statusSystem, Status status);
        #endregion

        public Dictionary<StatusTag, Status> Statuses = new Dictionary<StatusTag, Status>();

        public event StatusEventHandler OnAddedStatus;

        public Status CreateOrSetValue(StatusTag Tag, float minValue, float maxValue)
        {
            if (!Statuses.TryGetValue(Tag, out Status value))
            {
                value = new Status(Tag, minValue, maxValue);

                Statuses.Add(Tag, value);

                OnAddStatus(value);
            }
            else
            {
                value.SetPoint(minValue, maxValue);
            }

            return value;
        }

        public Status GetValue(StatusTag Tag)
        {
            if (!Tag)
            {
                return null;
            }

            if (Statuses.TryGetValue(Tag, out Status value))
                return value;

            return null;
        }

        public Status GetOrCreateValue(StatusTag Tag, float minValue = 0f, float maxValue = 0f)
        {
            if (!Tag)
            {
                return null;
            }

            if (Statuses.TryGetValue(Tag, out Status value))
                return value;
            else
            {
                value = new Status(Tag, minValue, maxValue);

                Statuses.Add(Tag, value);

                OnAddStatus(value);

                return value;
            }
        }
        public Status GetOrCreateValue(FInitializationStatus Status)
        {
            return GetOrCreateValue(Status.StatusTag, Status.MinValue, Status.MaxValue);
        }

        #region Callback
        public void OnAddStatus(Status status)
        {
            OnAddedStatus?.Invoke(this, status);
        }
        #endregion
    }
}