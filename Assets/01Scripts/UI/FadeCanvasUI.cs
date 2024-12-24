using System;
using DG.Tweening;
using GGM.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GGM.UI
{
    public class FadeCanvasUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _systemEventChannel;
        [SerializeField] private Image _fadeImage;
        
        private readonly int _circleSizeHash = Shader.PropertyToID("_CircleSize");

        private void Awake()
        {
            _fadeImage.material = new Material(_fadeImage.material);
            _systemEventChannel.AddListener<FadeEvent>(HandleFadeScreen);
        }

        private void OnDestroy()
        {
            _systemEventChannel.RemoveListener<FadeEvent>(HandleFadeScreen);
        }

        private void HandleFadeScreen(FadeEvent evt)
        {
            float fadeValue = evt.isFadeIn ? 2.5f : 0;
            float startValue = evt.isFadeIn ? 0 : 2.5f;
            
            _fadeImage.material.SetFloat(_circleSizeHash, startValue);
            _fadeImage.material.DOFloat(fadeValue, _circleSizeHash, 0.8f)
                .OnComplete(() => _systemEventChannel.RaiseEvent(SystemEvents.FadeEndEvent));
        }
    }
}
