#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Set Status Value")]
    [UnitShortTitle("SetValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class SetStatusValueUnit : StatusSetterUnit
    {
        [DoNotSerialize]
        [PortLabel("MaxValue")]
        public ValueInput MaxValue { get; private set; }
        [DoNotSerialize]
        [PortLabel("MinValue")]
        public ValueInput MinValue { get; private set; }
        [DoNotSerialize]
        [PortLabel("UseRate")]
        public ValueInput UseRate { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            MaxValue = ValueInput<float>(nameof(MaxValue), 100f);
            MinValue = ValueInput<float>(nameof(MinValue), 1f);
            UseRate = ValueInput<bool>(nameof(UseRate), true);

            Requirement(MaxValue, Enter);
            Requirement(MinValue, Enter);
            Requirement(UseRate, Enter);
        }

        protected override ControlOutput OnFlow(Flow flow)
        {
            var maxValue = flow.GetValue<float>(MaxValue);
            var minValue = flow.GetValue<float>(MinValue);
            var useRate = flow.GetValue<bool>(UseRate);

            GetStatus(flow).SetValue(maxValue, minValue, useRate);

            return Exit;
        }
    }
}

#endif