using UnityEngine;

namespace GGM.Animators
{
    [CreateAssetMenu(fileName = "AnimParamSO", menuName = "SO/Anim/ParamSO")]
    public class AnimParamSO : ScriptableObject
    {
        public string paramName;
        public int hashValue;

        private void OnValidate()
        {
            hashValue = Animator.StringToHash(paramName);
        }
    }
}
