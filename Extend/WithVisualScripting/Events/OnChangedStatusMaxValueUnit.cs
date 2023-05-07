#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{

    [UnitTitle("On Changed Status MaxValue")]
    [UnitSubtitle("StatusSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatusSystem")]
    public class OnChangedStatusMaxValueUnit : StatusEventUnit<ChangedStatusValue>
    {
        protected override string HookName => StatusSystemWithVisualScripting.STATUS_ON_CHANGED_MAX_VALUE;

        [DoNotSerialize]
        [PortLabel("StatusTag")]
        [PortLabelHidden]
        public ValueInput StatusTag { get; private set; }

        [DoNotSerialize]
        [PortLabel("Status")]
        public ValueOutput Status { get; private set; }

        [DoNotSerialize]
        [PortLabel("CurrentValue")]
        public ValueOutput CurrentValue { get; private set; }

        [DoNotSerialize]
        [PortLabel("PrevValue")]
        public ValueOutput PrevValue { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            StatusTag = ValueInput<StatusTag>(nameof(StatusTag), null);

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

        protected override bool ShouldTrigger(Flow flow, ChangedStatusValue changedStatusValue)
        {
            if (!base.ShouldTrigger(flow, changedStatusValue))
                return false;

            return flow.GetValue<StatusTag>(StatusTag) == changedStatusValue.Status.Tag;
        }
    }
}

#endif