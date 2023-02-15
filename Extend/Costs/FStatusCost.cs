using UnityEngine;

namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public struct FStatusCost
    {
        public ECalculateType Type;
        public float Value;
        [Range(0f, 100f)]public float RateValue;

        public bool useRateValue => !Type.Equals(ECalculateType.Absolute);
    }

}

