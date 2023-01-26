#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    [UnitTitle("Can Status Subtract Value from StatusTag")]
    [UnitShortTitle("CanSubtractCurrentValue")]
    [UnitSubtitle("StatusSystem Unit")]
    [UnitCategory("StudioScor\\StatusSystem")]
    public class CanStatusSubtractValueFromStatusTagUnit : StatusGetterFromStatusTagUnit
    {
        [DoNotSerialize]
        [PortLabel("Type")]
        [PortLabelHidden]
        public ValueInput Type { get; private set; }

        [DoNotSerialize]
        [PortLabel("Add")]
        public ValueInput Value { get; private set; }

        [DoNotSerialize]
        [PortLabel("CanSubtract")]
        [PortLabelHidden]
        public ValueOutput CanSubtract { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            Type = ValueInput<ECalculateType>(nameof(Type), ECalculateType.Absolute);
            Value = ValueInput<float>(nameof(Value), 0f);

            CanSubtract = ValueOutput<bool>(nameof(CanSubtract), CanSubtractValue);

            Requirement(StatusSystem, CanSubtract);
            Requirement(StatusTag, CanSubtract);
            Requirement(Type, CanSubtract);
            Requirement(Value, CanSubtract);
        }

        private bool CanSubtractValue(Flow flow)
        {
            var type = flow.GetValue<ECalculateType>(Type);
            var value = flow.GetValue<float>(Value);

            return GetStatus(flow).CanSubtractValue(value, type);
        }
    }
}

#endif