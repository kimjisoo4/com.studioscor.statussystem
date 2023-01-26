#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    public abstract class StatusGetterUnit : Unit
    {
        [DoNotSerialize]
        [PortLabel("Status")]
        public ValueInput Status;

        protected override void Definition()
        {
            Status = ValueInput<Status>(nameof(Status));
        }

        protected Status GetStatus(Flow flow)
        {
            return flow.GetValue<Status>(Status);
        }
    }
}

#endif