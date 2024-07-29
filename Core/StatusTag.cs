using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatusSystem
{

    [CreateAssetMenu(fileName = "Status_", menuName = "StudioScor/Attribute System/new Status")]
    public class StatusTag : ScriptableObject
    {
        [Header("[ Name ]")]
        [SerializeField] private string _name;
        [Header("[ Description ]")]
        [SerializeField] private string _description;

        public string Name => _name;
        public string Description => _description;
    }
}