#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Set Status Max Value")]
    [UnitShortTitle("SetMaxValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class SetStatusMaxValueUnit : StatusSetterUnit
    {
        [DoNotSerialize]
        [PortLabel("Value")]
        public ValueInput Value { get; private set; }

        [DoNotSerialize]
        [PortLabel("UseRate")]
        public ValueInput UseRate { get; private set; }

        [DoNotSerialize]
        [PortLabel("UseChangeCurrentValue")]
        public ValueInput UseChangeCurrentValue { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            Value = ValueInput<float>(nameof(Value), 100f);
            UseRate = ValueInput<bool>(nameof(UseRate), false);
            UseChangeCurrentValue = ValueInput<bool>(nameof(UseChangeCurrentValue), true);

            Requirement(Value, Enter);
            Requirement(UseChangeCurrentValue, Enter);
            Requirement(UseRate, Enter);
        }

        protected override ControlOutput OnFlow(Flow flow)
        {
            var maxValue = flow.GetValue<float>(Value);
            var useRate = flow.GetValue<bool>(UseRate);
            var useChangeCurrentValue = flow.GetValue<bool>(UseChangeCurrentValue);

            GetStatus(flow).SetMaxValue(maxValue, useRate, useChangeCurrentValue);

            return Exit;
        }
    }
}

#endif