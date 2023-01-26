#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    public abstract class StatusEventUnit<T> : EventUnit<T>
    {
        protected override bool register => true;
        protected abstract string HookName { get; }
        public override EventHook GetHook(GraphReference reference)
        {
            if (!reference.hasData)
            {
                return HookName;
            }

            var data = reference.GetElementData<Data>(this);

            return new EventHook(HookName, data.Status);
        }

        [DoNotSerialize]
        [PortLabel("StatusSystem")]
        [NullMeansSelf]
        [PortLabelHidden]
        public ValueInput StatusSystem { get; private set; }
        [DoNotSerialize]
        [PortLabel("StatusTag")]
        [PortLabelHidden]
        public ValueInput StatusTag { get; private set; }

        public override IGraphElementData CreateData()
        {
            return new Data();
        }
        public new class Data : EventUnit<T>.Data
        {
            public StatusSystemComponent StatusSystem;
            public StatusTag StatusTag;
            public Status Status;
        }

        protected override void Definition()
        {
            base.Definition();

            StatusSystem = ValueInput<StatusSystemComponent>(nameof(StatusSystem), null).NullMeansSelf();
            StatusTag = ValueInput<StatusTag>(nameof(StatusTag), null);
        }

        private void UpdateTarget(GraphStack stack)
        {
            var data = stack.GetElementData<Data>(this);

            var wasListening = data.isListening;

            var statSystem = Flow.FetchValue<StatusSystemComponent>(StatusSystem, stack.ToReference());
            var statTag = Flow.FetchValue<StatusTag>(StatusTag, stack.ToReference());

            if (statSystem != data.StatusSystem || statTag != data.StatusTag)
            {
                if (wasListening)
                {
                    StopListening(stack);
                }

                data.StatusSystem = statSystem;
                data.StatusTag = statTag;
                data.Status = statSystem.GetOrCreateStatus(statTag);

                if (wasListening)
                {
                    StartListening(stack, false);
                }
            }
        }

        protected void StartListening(GraphStack stack, bool updateTarget)
        {
            if (updateTarget)
            {
                UpdateTarget(stack);
            }

            var data = stack.GetElementData<Data>(this);

            if (data.Status is null)
            {
                return;
            }

            if (UnityThread.allowsAPI)
            {
                var target = data.StatusSystem.gameObject;

                if (!target.TryGetComponent(out StatusMessageListener messageListener))
                {
                    messageListener = target.AddComponent<StatusMessageListener>();
                }

                messageListener.TryAddEventBus(data.Status.Tag);
            }

            base.StartListening(stack);
        }

        public override void StartListening(GraphStack stack)
        {
            StartListening(stack, true);
        }
    }
}

#endif