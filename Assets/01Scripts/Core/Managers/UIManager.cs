using System;
using GGM.EventSystem;
using GGM.Players;
using UnityEngine;

namespace GGM.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO _playerInput;
        [field: SerializeField] public GameEventChannelSO UIEventChannel { get; private set; }

        private void Awake()
        {
            _playerInput.OpenMenuKeyEvent += HandleOpenMenu;
        }

        private void OnDestroy()
        {
            _playerInput.OpenMenuKeyEvent -= HandleOpenMenu;
        }

        private void HandleOpenMenu()
        {
            UIEventChannel.RaiseEvent(UIEvents.OpenMenu);
        }
    }
}
