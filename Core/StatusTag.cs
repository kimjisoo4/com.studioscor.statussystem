using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatusSystem
{

    [CreateAssetMenu(fileName = "Status_", menuName = "StudioScor/Attribute System/new Status")]
    public class StatusTag : ScriptableObject
    {
        [Header("[ Name ]")]
        [SerializeField] private string _Name;
        [Header("[ Description ]")]
        [SerializeField] private string _Description;

        public string Name => _Name;
        public string Description => _Description;
    }
}