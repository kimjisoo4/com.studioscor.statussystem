using UnityEngine;
using StudioScor.Utilities;
namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public struct FInitializationStatus
    {
#if UNITY_EDITOR
        [SReadOnly] public string HeaderName;
#endif
        public StatusTag Tag;
        public float MaxValue;
        public bool UseRate;
        [SerializeField][SRange(0f, nameof(MaxValue)), SCondition(nameof(UseRate), true, true)] private float _CurrentValue;
        [SerializeField][SRange(0f, 1f), SCondition(nameof(UseRate), true)] private float _RateValue;
        public float CurrentValue => UseRate ? _RateValue : _CurrentValue;
    }
}