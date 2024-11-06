using UnityEngine;
using StudioScor.Utilities;
namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public struct FInitializationStatus
    {
        public StatusTag Tag;
        public float MaxValue;
        public bool UseRatio;
        public float CurrentValue;
    }
}