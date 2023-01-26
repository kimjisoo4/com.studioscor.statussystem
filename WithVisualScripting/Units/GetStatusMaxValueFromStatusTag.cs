#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Get Status Max Value from StatusTag")]
    [UnitShortTitle("GetMaxValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class GetStatusMaxValueFromStatusTag : StatusGetterFromStatusTagUnit
    {
        [DoNotSerialize]
        [PortLabel("MaxValue")]
        [PortLabelHidden]
        public ValueOutput Value;

        protected override void Definition()
        {
            base.Definition();

            Value = ValueOutput<float>(nameof(Value), (flow) => { return GetStatus(flow).MaxValue; });

            Requirement(StatusSystem, Value);
            Requirement(StatusTag, Value);
        }
    }
}

#endif