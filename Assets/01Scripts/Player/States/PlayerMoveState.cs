using GGM.Animators;
using GGM.Entities;
using UnityEngine;

namespace GGM.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            
            _mover.SetMovement(xInput);

            if (Mathf.Approximately(xInput, 0))
            {
                _player.ChangeState("IDLE");
            }
        }
    }
}
