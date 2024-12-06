using GGM.Animators;
using GGM.Entities;
using UnityEngine;

namespace GGM.Players
{
    public class PlayerJumpState : PlayerAirState
    {
        
        public PlayerJumpState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Vector2 jumpPower = new Vector2(0, _player.jumpPower.Value);
            
            _player.DecreaseJumpCount();
            
            _mover.StopImmediately(true);
            _mover.AddForceToEntity(jumpPower);
            _mover.OnMovement += HandleVelocityChange;
        }

        public override void Exit()
        {
            _mover.OnMovement -= HandleVelocityChange;
            base.Exit();
        }

        private void HandleVelocityChange(Vector2 velocity)
        {
            if(velocity.y < 0)
                _player.ChangeState("FALL");
        }
    }
}
