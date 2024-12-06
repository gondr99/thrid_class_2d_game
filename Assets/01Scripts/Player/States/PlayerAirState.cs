using GGM.Animators;
using GGM.Entities;
using GGM.FSM;
using Unity.VisualScripting;
using UnityEngine;

namespace GGM.Players
{
    public abstract class PlayerAirState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        
        protected PlayerAirState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetSpeedMultiplier(0.7f);
            _player.PlayerInput.JumpEvent += HandleAirJump;
        }

        public override void Exit()
        {
            _mover.SetSpeedMultiplier(1f);
            _player.PlayerInput.JumpEvent -= HandleAirJump;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            if(Mathf.Abs(xInput) > 0)
                _mover.SetMovement(xInput);
        }

        private void HandleAirJump()
        {
            //더블점프 관련 로직은 이따가 만들어줍니다.
            if(_player.CanAirJump)
                _player.ChangeState("JUMP");
        }
    }
}
