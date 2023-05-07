using UnityEngine;

namespace StudioScor.StatusSystem
{
    public enum EStatusState
    {
        None,
        [Tooltip(" Current Value is Full(max) State.")] Fulled,
        [Tooltip(" Current Value is Empty(zero) State.")] Emptied,
        [Tooltip(" Current Value is Cusumed States.")] Consumed,
    }
}