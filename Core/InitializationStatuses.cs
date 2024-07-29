using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    [CreateAssetMenu(menuName = "StudioScor/Attribute System/new InitializationStatus", fileName = "InitializationStatus")]
    public class InitializationStatuses : ScriptableObject
    {
        [Header(" [ Initialization Statuses ] ")]
        [SerializeField] private FInitializationStatus[] _statuses;
        public IReadOnlyCollection<FInitializationStatus> Statuses => _statuses;

        private void OnValidate()
        {
#if UNITY_EDITOR
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (_statuses[i].Tag == null)
                    return;

                float currentValue = _statuses[i].UseRate ? _statuses[i].MaxValue * _statuses[i].CurrentValue : _statuses[i].CurrentValue;

                _statuses[i].HeaderName = $"{_statuses[i].Tag.Name} [ {currentValue:N0} / {_statuses[i].MaxValue:N0} ]";
            }
#endif
        }
    }
}