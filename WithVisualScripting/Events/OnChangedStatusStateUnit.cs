#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("On Changed Status State")]
    [UnitSubtitle("StatusSystem Event")]
    [UnitCategory("Events\\StudioScor\\StatusSystem")]
    public class OnChangedStatusStateUnit : StatusEventUnit<ChangedStatusState>
    {
        protected override string HookName => StatusSystemWithVisualScripting.STATUS_ON_CHANGED_STATE;

        [DoNotSerialize]
        [PortLabel("Status")]
        public ValueOutput Status;

        [DoNotSerialize]
        [PortLabel("CurrentState")]
        public ValueOutput CurrentState;

        [DoNotSerialize]
        [PortLabel("PrevState")]
        public ValueOutput PrevState;

        protected override void Definition()
        {
            base.Definition();

            Status = ValueOutput<Status>(nameof(Status));
            CurrentState = ValueOutput<EStatusState>(nameof(CurrentState));
            PrevState = ValueOutput<EStatusState>(nameof(PrevState));
        }

        protected override void AssignArguments(Flow flow, ChangedStatusState changedStatusState)
        {
            base.AssignArguments(flow, changedStatusState);

            flow.SetValue(Status, changedStatusState.Status);
            flow.SetValue(CurrentState, changedStatusState.CurrentState);
            flow.SetValue(PrevState, changedStatusState.PrevState);
        }
    }
}

#endif