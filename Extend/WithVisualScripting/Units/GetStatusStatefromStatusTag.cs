#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Get Status State from StatusTag")]
    [UnitShortTitle("GetState")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class GetStatusStatefromStatusTag : StatusGetterFromStatusTagUnit
    {
        [DoNotSerialize]
        [PortLabel("State")]
        [PortLabelHidden]
        public ValueOutput State;

        protected override void Definition()
        {
            base.Definition();

            State = ValueOutput<EStatusState>(nameof(State), (flow) => { return GetStatus(flow).CurrentState; });

            Requirement(StatusSystem, State);
            Requirement(StatusTag, State);
        }
    }
}

#endif