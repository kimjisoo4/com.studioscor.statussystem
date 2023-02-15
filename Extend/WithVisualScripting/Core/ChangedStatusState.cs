#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEditor;
using UnityEngine;
namespace StudioScor.StatusSystem.VisualScripting
{
    public class ChangedStatusState
    {
        public Status Status;
        public EStatusState CurrentState;
        public EStatusState PrevState;

        public ChangedStatusState(Status status, EStatusState currentState, EStatusState prevState)
        {
            Status = status;
            CurrentState = currentState;
            PrevState = prevState;
        }
    }
}

#endif