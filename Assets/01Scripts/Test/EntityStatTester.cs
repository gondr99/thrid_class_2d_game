using System;
using GGM.Core.StatSystem;
using GGM.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGM.Test
{
    public class EntityStatTester : MonoBehaviour
    {
        [SerializeField] private EntityStat _statCompo;
        [SerializeField] private StatSO _targetStat;
        [SerializeField] private float _testValue;


        private void Update()
        {
            if(Keyboard.current.qKey.wasPressedThisFrame)
                _statCompo.AddModifier(_targetStat, this, _testValue);
            
            if(Keyboard.current.eKey.wasPressedThisFrame)
                _statCompo.RemoveModifier(_targetStat, this);
        }
    }
}
