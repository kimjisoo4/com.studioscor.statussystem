#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Set Status Current Value")]
    [UnitShortTitle("SetCurrentValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class SetStatusCurrentValueUnit : StatusSetterUnit
    {
        [DoNotSerialize]
        [PortLabel("Type")]
        [PortLabelHidden]
        public ValueInput Type { get; private set; }
        [DoNotSerialize]
        [PortLabel("Value")]
        public ValueInput Value { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            Type = ValueInput<ECalculateType>(nameof(Type), ECalculateType.Absolute);
            Value = ValueInput<float>(nameof(Value), 0f);

            Requirement(Type, Enter);
            Requirement(Value, Enter);
        }
        protected override ControlOutput OnFlow(Flow flow)
        {
            var value = flow.GetValue<float>(Value);
            var type = flow.GetValue<ECalculateType>(Type);

            GetStatus(flow).SetCurrentValue(value, type);

            return Exit;
        }
    }
}

#endif