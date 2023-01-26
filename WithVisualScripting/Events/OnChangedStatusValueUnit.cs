#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("On Changed Status Value")]
    [UnitSubtitle("StatusSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatusSystem")]
    public class OnChangedStatusValueUnit : StatusEventUnit<ChangedStatusValue>
    {
        protected override string HookName => StatusSystemWithVisualScripting.STATUS_ON_CHANGED_VALUE;

        [DoNotSerialize]
        [PortLabel("Status")]
        public ValueOutput Status;

        [DoNotSerialize]
        [PortLabel("CurrentValue")]
        public ValueOutput CurrentValue;

        [DoNotSerialize]
        [PortLabel("PrevValue")]
        public ValueOutput PrevValue;

        protected override void Definition()
        {
            base.Definition();

            Status = ValueOutput<Status>(nameof(Status));
            CurrentValue = ValueOutput<float>(nameof(CurrentValue));
            PrevValue = ValueOutput<float>(nameof(PrevValue));
        }

        protected override void AssignArguments(Flow flow, ChangedStatusValue changedStatusValue)
        {
            base.AssignArguments(flow, changedStatusValue);

            flow.SetValue(Status, changedStatusValue.Status);
            flow.SetValue(CurrentValue, changedStatusValue.CurrentValue);
            flow.SetValue(PrevValue, changedStatusValue.PrevValue);
        }
    }
}

#endif