using UnityEngine;

namespace KimScor.StatusSystem
{
    [System.Serializable]
    public class StatusCostSO
    {
        [SerializeField] private StatusTag _StatusTag;
        [SerializeField] private EConsumeType _ConsumeType;
        [SerializeField] private float _ConsumePoint;
        [SerializeField, Range(0f, 100f)] private float _ConsumeRatioPoint;

        public StatusTag StatusTag => _StatusTag;
        public EConsumeType ConsumeType => _ConsumeType; 
        public float ConsumePoint => _ConsumePoint;
        public float ConsumeRatioPoint => _ConsumeRatioPoint;

        public StatusCostSpec CreateSpec(StatusSystem statusSystem)
        {
            var spec = new StatusCostSpec(this, statusSystem);

            return spec;
        }
    }
}

