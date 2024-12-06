using GGM.Animators;
using UnityEngine;

namespace GGM.FSM
{
    [CreateAssetMenu(fileName = "StateSO", menuName = "SO/FSM/StateSO")]
    public class StateSO : ScriptableObject
    {
        public string stateName;
        public string className;
        public AnimParamSO stateParam;
    }
}
