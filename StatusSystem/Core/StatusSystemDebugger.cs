using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace StudioScor.StatusSystem.Editor
{
    public class StatusSystemDebugger : MonoBehaviour
    {
        [SerializeField] private StatusSystemComponent _StatusSystem;
        [SerializeField] private List<Status> _Status;

#if UNITY_EDITOR
        private void Reset()
        {
            _StatusSystem = GetComponent<StatusSystemComponent>();
        }
#endif
        private void OnEnable()
        {
            if (TryGetComponent(out _StatusSystem))
            {
                _Status = _StatusSystem.Statuses.Values.ToList();

                _StatusSystem.OnGrantedStatus += StatusSystem_OnGrantedStatus;
            }
            else
            {
                enabled = false;
            }
        }
        private void OnDisable()
        {
            if (_StatusSystem)
            {
                _Status.Clear();

                _StatusSystem.OnGrantedStatus -= StatusSystem_OnGrantedStatus;
            }
        }
        private void StatusSystem_OnGrantedStatus(StatusSystemComponent statusSystem, Status status)
        {
            _Status = _StatusSystem.Statuses.Values.ToList();
        }
    }
}