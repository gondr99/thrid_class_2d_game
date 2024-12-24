using GGM.EventSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGM.Enviroments
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private string _nextSceneName;
        [SerializeField] private GameEventChannelSO _systemEventChannel;
    
        private bool _isTriggered = false;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isTriggered) return;
            
            if (other.CompareTag("Player"))
            {
                _isTriggered = true;

                FadeEvent evt = SystemEvents.FadeEvent;
                evt.isFadeIn = false;
                _systemEventChannel.AddListener<FadeEndEvent>(HandleFadeEnd);
                _systemEventChannel.RaiseEvent(evt);
            }
        }

        private void HandleFadeEnd(FadeEndEvent evt)
        {
            _systemEventChannel.RemoveListener<FadeEndEvent>(HandleFadeEnd);            
            SceneManager.LoadScene(_nextSceneName);
        }
    }
}
