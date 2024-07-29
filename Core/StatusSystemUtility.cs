using UnityEngine;

namespace StudioScor.StatusSystem
{
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
}