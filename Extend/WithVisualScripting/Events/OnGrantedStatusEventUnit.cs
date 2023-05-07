#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("On Granted Status")]
    [UnitSubtitle("StatusSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatusSystem")]
    public class OnGrantedStatusEventUnit : StatusEventUnit<Status>
    {
        protected override string HookName => StatusSystemWithVisualScripting.STATUS_ON_GRANTED_STATUS;

        [DoNotSerialize]
        [PortLabel("Status")]
        [PortLabelHidden]
        public ValueOutput Status { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            Status = ValueOutput<Status>(nameof(Status));
        }

        protected override void AssignArguments(Flow flow, Status status)
        {
            base.AssignArguments(flow, status);

            flow.SetValue(Status, status);
        }
    }
}

#endif