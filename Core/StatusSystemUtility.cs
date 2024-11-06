using UnityEngine;

namespace StudioScor.StatusSystem
{
    public static class StatusSystemUtility
    {
        public static IStatusSystem GetStatusSystem(this GameObject gameObject)
        {
            return gameObject.GetComponent<IStatusSystem>();
        }
        public static IStatusSystem GetStatusSystem(this Component component)
        {
            return component.GetComponent<IStatusSystem>();
        }

        public static bool TryGetStatusSystem(this GameObject gameObject, out IStatusSystem statusSystem)
        {
            return gameObject.TryGetComponent(out statusSystem);
        }
        public static bool TryGetStatusSystem(this Component component, out IStatusSystem statusSystem)
        {
            return component.TryGetComponent(out statusSystem);
        }


        public static Status GetStatus(this IStatusSystem statusSystem, StatusTag statusTag)
        {
            return statusSystem.Statuses[statusTag];
        }
        public static Status GetStatus(this GameObject gameObject, StatusTag statusTag)
        {
            if (gameObject.TryGetComponent(out IStatusSystem statusSystem))
            {
                return statusSystem.Statuses[statusTag];
            }
            else
            {
                return null;
            }
        }
        public static Status GetStatus(this Component component, StatusTag statusTag)
        {
            return component.gameObject.GetStatus(statusTag);
        }

        public static bool TryGetStatus(this IStatusSystem statusSystem, StatusTag statusTag, out Status status)
        {
            return statusSystem.Statuses.TryGetValue(statusTag, out status);
        }
        public static bool TryGetStatus(this GameObject gameObject, StatusTag statusTag, out Status status)
        {
            if(gameObject.TryGetStatusSystem(out IStatusSystem statusSystem))
            {
                return statusSystem.TryGetStatus(statusTag, out status);
            }
            else
            {
                status = null;
                return false;
            }
        }
        public static bool TryGetStatus(this Component component, StatusTag statusTag, out Status status)
        {
            return component.gameObject.TryGetStatus(statusTag, out status);
        }


        public static bool HasStatus(this IStatusSystem statusSystem, StatusTag statusTag)
        {
            return statusSystem.Statuses.ContainsKey(statusTag);
        }
        public static bool HasStatus(this GameObject gameObject, StatusTag statusTag)
        {
            if(gameObject.TryGetStatusSystem(out IStatusSystem statusSystem))
            {
                return statusSystem.HasStatus(statusTag);
            }
            else
            {
                return false;
            }
        }
        public static bool HasStatus(this Component conponent, StatusTag statusTag)
        {
            return conponent.gameObject.HasStatus(statusTag);
        }
    }
}