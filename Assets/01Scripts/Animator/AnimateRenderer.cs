using UnityEngine;

namespace GGM.Animators
{
    public class AnimateRenderer : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        public void SetParam(AnimParamSO param, bool value) => _animator.SetBool(param.hashValue, value);
        public void SetParam(AnimParamSO param, float value) => _animator.SetFloat(param.hashValue, value);        
        public void SetParam(AnimParamSO param, int value) => _animator.SetInteger(param.hashValue, value);        
        public void SetParam(AnimParamSO param) => _animator.SetTrigger(param.hashValue);        
    }
}
