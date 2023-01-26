using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.StatusSystem
{

    [CreateAssetMenu(fileName = "new Status", menuName = "StudioScor/Attribute System/new Status")]
    public class StatusTag : ScriptableObject
    {
        [Header("[Name]")]
        [SerializeField] private string _Name;
        [Header("[Text]")]
        [SerializeField] private string _Description;

        public string Name => _Name;
        public string Description => _Description;
    }
}