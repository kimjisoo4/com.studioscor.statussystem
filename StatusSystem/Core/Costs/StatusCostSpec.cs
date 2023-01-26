namespace StudioScor.StatusSystem
{
    [System.Serializable]
    public class StatusCostSpec : BaseStatusCost
    {
        private readonly StatusCost _StatusCost;
        public override FStatusCost Cost => _StatusCost.Cost;

        public StatusCostSpec(StatusCost statusCostSO, StatusSystemComponent statusSystem, StatusTag statusTag) : base(statusSystem, statusTag)
        {
            _StatusCost = statusCostSO;
        }
    }
}

