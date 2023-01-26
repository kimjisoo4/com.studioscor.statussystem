#if SCOR_ENABLE_VISUALSCRIPTING

namespace StudioScor.StatusSystem.VisualScripting
{
    public class ChangedStatusValue
    {
        public Status Status;
        public float CurrentValue;
        public float PrevValue;

        public ChangedStatusValue(Status status, float currentValue, float prevValue)
        {
            Status = status;
            CurrentValue = currentValue;
            PrevValue = prevValue;
        }
    }
}

#endif