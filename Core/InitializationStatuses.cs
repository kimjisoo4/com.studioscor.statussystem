using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.StatusSystem
{
    [CreateAssetMenu(menuName = "StudioScor/Attribute System/new InitializationStatus", fileName = "InitializationStatus")]
    public class InitializationStatuses : ScriptableObject
    {
        [SerializeField] private FInitializationStatus[] _Statuses;
        public IReadOnlyCollection<FInitializationStatus> Statuses => _Statuses;

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (_Statuses[i].Tag == null)
                    return;

                float currentValue = _Statuses[i].UseRate ? _Statuses[i].MaxValue * _Statuses[i].RateValue : _Statuses[i].CurrentValue;

                _Statuses[i].HeaderName = _Statuses[i].Tag.Name + 
                    " [" + currentValue + " / " + _Statuses[i].MaxValue + "]";
            }
        }
#endif
    }
}