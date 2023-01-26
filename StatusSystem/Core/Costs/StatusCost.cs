using UnityEngine;

namespace StudioScor.StatusSystem
{
    [CreateAssetMenu(menuName = "StudioScor/Attribute System/new Status Cost", fileName = "Cost_")]
    public class StatusCost : ScriptableObject
    {
        [Header(" [ Status Cost ] ")]
        [SerializeField] private StatusTag _Tag;
        [SerializeField] private FStatusCost _Cost;
        public FStatusCost Cost => _Cost; 

        public StatusCostSpec CreateSpec(StatusSystemComponent statusSystem)
        {
            var spec = new StatusCostSpec(this, statusSystem, _Tag);

            return spec;
        }
    }
}

