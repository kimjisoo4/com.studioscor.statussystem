using UnityEngine;

namespace StudioScor.StatusSystem
{
    public enum ECalculateType
    {
        [Tooltip("Subtract the Current Value to Absolute Value")] Absolute,
        [Tooltip("Subtract the Current Value to Ratio of the MaxValue")] RatioMaxValue,
        [Tooltip("Subtract the Current Value to Ratio of ther CurrentValue")] RatioCurrentValue,
    }
}

