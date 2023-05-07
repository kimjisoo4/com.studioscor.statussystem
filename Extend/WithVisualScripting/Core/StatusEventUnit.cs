#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
using System;
using StudioScor.Utilities.VisualScripting;

namespace StudioScor.StatusSystem.VisualScripting
{
    public abstract class StatusEventUnit<T> : CustomInterfaceEventUnit<IStatusSystem, T>
    {
        public override Type MessageListenerType => typeof(StatusSystemMessageListener);
    }
}

#endif