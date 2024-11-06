using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    [CreateAssetMenu(menuName = "StudioScor/StatusSystem/new InitializationStatus", fileName = "InitializationStatus_")]
    public class InitializationStatuses : ScriptableObject
    {
        [Header(" [ Initialization Statuses ] ")]
        [SerializeField] private FInitializationStatus[] _statuses;
        public IReadOnlyCollection<FInitializationStatus> Statuses => _statuses;
    }
}