#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Get Status State")]
    [UnitShortTitle("GetState")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class GetStatusState : StatusGetterUnit
    {
        [DoNotSerialize]
        [PortLabel("State")]
        [PortLabelHidden]
        public ValueOutput State;

        protected override void Definition()
        {
            base.Definition();

            State = ValueOutput<EStatusState>(nameof(State), (flow) => { return GetStatus(flow).CurrentState; });

            Requirement(Status, State);
        }
    }
}

#endif