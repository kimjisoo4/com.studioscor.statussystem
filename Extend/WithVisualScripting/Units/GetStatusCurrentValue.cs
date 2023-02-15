#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{

    [UnitTitle("Get Status Current Value")]
    [UnitShortTitle("GetCurrentValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class GetStatusCurrentValue : StatusGetterUnit
    {
        [DoNotSerialize]
        [PortLabel("CurrentValue")]
        [PortLabelHidden]
        public ValueOutput Value;

        protected override void Definition()
        {
            base.Definition();

            Value = ValueOutput<float>(nameof(Value), (flow) => { return GetStatus(flow).CurrentValue; });

            Requirement(Status, Value);
        }
    }
}

#endif