using System;
using DG.Tweening;
using GGM.EventSystem;
using GGM.Players;
using UnityEngine;

namespace GGM.UI
{
    public class MenuCanvasUI : MonoBehaviour
    {
        public enum UIWindowStatus
        {
            Closed, Closing, Opening, Opened
        }
        
        [field: SerializeField] public GameEventChannelSO UIEventChannel { get; private set; }
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private PlayerInputSO _playerInput;

        [SerializeField] private InventoryUI _inventoryUI; 
        
        private UIWindowStatus _windowStatus = UIWindowStatus.Closed;

        private void Awake()
        {
            UIEventChannel.AddListener<OpenMenu>(HandleOpenMenuEvent);
        }

        private void Start()
        {
            CloseWindow(0);
        }

        private void OnDestroy()
        {
            UIEventChannel.RemoveListener<OpenMenu>(HandleOpenMenuEvent);
        }

        private void HandleOpenMenuEvent(OpenMenu evt)
        {
            if (_windowStatus == UIWindowStatus.Closing || _windowStatus == UIWindowStatus.Opening)
                return; //진행중이라면 정지

            if (_windowStatus == UIWindowStatus.Opened) //열려있다면 닫아라.
            {
                CloseWindow();
            }else if (_windowStatus == UIWindowStatus.Closed)
            {
                OpenWindow();
            }
        }

        private void CloseWindow(float duration = 0.3f)
        {
            _windowStatus = UIWindowStatus.Closing;
            _playerInput.SetPlayerInput(true); //플레이어 입력을 복원해준다.
            _inventoryUI.Close();
            SetWindow(false, () =>
            {
                _windowStatus = UIWindowStatus.Closed;
                Time.timeScale = 1f;
            }, duration);
        }

        private void OpenWindow( float duration = 0.3f)
        {
            _windowStatus = UIWindowStatus.Opening;
            Time.timeScale = 0;
            _playerInput.SetPlayerInput(false);
            _inventoryUI.Open();
            SetWindow(true, () => _windowStatus = UIWindowStatus.Opened, duration);
        }

        private void SetWindow(bool isOpen, Action callBackAction, float duration)
        {
            float alpha = isOpen ? 1f : 0f;
            _canvasGroup.DOFade(alpha, duration).SetUpdate(true).OnComplete(() => callBackAction?.Invoke());
            _canvasGroup.blocksRaycasts = isOpen;
            _canvasGroup.interactable = isOpen;
        }
    }
}
