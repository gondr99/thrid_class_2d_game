using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGM.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        public event Action JumpEvent;
        public event Action AttackEvent;
        public event Action OpenMenuKeyEvent;

        #region UIEvents
        public event Action<Vector2> UINavigationKeyEvent;

        #endregion
        
        public Vector2 InputDirection {get; private set;}
        
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
            }
            _controls.Player.Enable();
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.UI.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            InputDirection = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                AttackEvent?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
                JumpEvent?.Invoke();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            if(context.performed)
                UINavigationKeyEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
        }
        
        public void OnOpenMenu(InputAction.CallbackContext context)
        {
            if(context.performed)
                OpenMenuKeyEvent?.Invoke();   
        }

        #region  not used event

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }
        #endregion
        
        public void SetPlayerInput(bool isEnable)
        {
            if(isEnable)
                _controls.Player.Enable();
            else
                _controls.Player.Disable();
        }
    }
}
