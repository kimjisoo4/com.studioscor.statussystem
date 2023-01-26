#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Get Status Current Value From StatusTag")]
    [UnitShortTitle("GetCurrentValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class GetStatusCurrentValueFromStatusTag : StatusGetterFromStatusTagUnit
    {
        [DoNotSerialize]
        [PortLabel("CurrentValue")]
        [PortLabelHidden]
        public ValueOutput Value;

        protected override void Definition()
        {
            base.Definition();

            Value = ValueOutput<float>(nameof(Value), (flow) => { return GetStatus(flow).CurrentValue; });

            Requirement(StatusSystem, Value);
            Requirement(StatusTag, Value);
        }
    }
}

#endif