#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    public abstract class StatusGetterFromStatusTagUnit : Unit
    {
        [DoNotSerialize]
        [PortLabel("StatusSystem")]
        [PortLabelHidden]
        [NullMeansSelf]
        public ValueInput StatusSystem;

        [DoNotSerialize]
        [PortLabel("StatusTag")]
        [PortLabelHidden]
        public ValueInput StatusTag;

        protected override void Definition()
        {
            StatusSystem = ValueInput<StatusSystemComponent>(nameof(StatusSystem), null).NullMeansSelf();
            StatusTag = ValueInput<StatusTag>(nameof(StatusTag), null);
        }

        protected Status GetStatus(Flow flow)
        {
            var statusSystem = flow.GetValue<StatusSystemComponent>(StatusSystem);
            var statusTag = flow.GetValue<StatusTag>(StatusTag);

            return statusSystem.GetOrCreateStatus(statusTag);
        }
    }
}

#endif