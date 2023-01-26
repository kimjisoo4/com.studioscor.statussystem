using UnityEngine;

namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public struct FInitializationStatus
    {
#if UNITY_EDITOR
        public string HeaderName;
#endif
        public StatusTag Tag;
        public float MaxValue;
        public bool UseRate;
        public float CurrentValue;
        [Range(0f, 1f)]public float RateValue;
    }
}