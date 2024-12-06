using GGM.Animators;
using GGM.Entities;
using UnityEngine;

namespace GGM.Players
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(false);
        }

        public override void Update()
        {
            base.Update();

            float xInput = _player.PlayerInput.InputDirection.x;
            
            if (Mathf.Abs(xInput) > 0)
            {
                _player.ChangeState("MOVE");
            }
        }
    }
}