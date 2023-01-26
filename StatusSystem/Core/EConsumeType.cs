using UnityEngine;

namespace StudioScor.StatusSystem
{
    public enum ECalculateType
    {
        [Tooltip("절대값으로 스테이터스를 차감함")] Absolute,
        [Tooltip("최대 값을 비율로 차감함")] RateMax,
        [Tooltip("현재 값을 비율로 차감함")] RateCurret,
    }
}

