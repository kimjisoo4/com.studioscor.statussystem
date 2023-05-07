using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    [CreateAssetMenu(menuName = "StudioScor/Attribute System/new InitializationStatus", fileName = "InitializationStatus")]
    public class InitializationStatuses : ScriptableObject
    {
        [Header(" [ Initialization Statuses ] ")]
        [SerializeField] private FInitializationStatus[] _Statuses;
        public IReadOnlyCollection<FInitializationStatus> Statuses => _Statuses;

        private void OnValidate()
        {
#if UNITY_EDITOR
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (_Statuses[i].Tag == null)
                    return;

                float currentValue = _Statuses[i].UseRate ? _Statuses[i].MaxValue * _Statuses[i].CurrentValue : _Statuses[i].CurrentValue;

                _Statuses[i].HeaderName = $"{_Statuses[i].Tag.Name} [ {currentValue:N0} / {_Statuses[i].MaxValue:N0} ]";
            }
#endif
        }
    }
}