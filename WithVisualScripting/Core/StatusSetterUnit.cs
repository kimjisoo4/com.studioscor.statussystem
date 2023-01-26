#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    public abstract class StatusSetterUnit : StatusGetterUnit
    {
        [DoNotSerialize]
        [PortLabel("Enter")]
        [PortLabelHidden]
        public ControlInput Enter;

        [DoNotSerialize]
        [PortLabel("Exit")]
        [PortLabelHidden]
        public ControlOutput Exit;

        protected override void Definition()
        {
            base.Definition();

            Enter = ControlInput(nameof(Enter), OnFlow);
            Exit = ControlOutput(nameof(Exit));

            Succession(Enter, Exit);
            Requirement(Status, Enter);
        }

        protected abstract ControlOutput OnFlow(Flow flow);
    }
}

#endif