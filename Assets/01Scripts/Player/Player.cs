using System;
using System.Collections.Generic;
using GGM.Core.StatSystem;
using GGM.Entities;
using GGM.FSM;
using UnityEngine;

namespace GGM.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        public List<StateSO> states;
        private StateMachine _stateMachine;

        public StatSO jumpPower, jumpCount;

        //점프 파워와 점프 카운트 스탯을 가지고 있어야 해.
        public float CurrentJumpCount { get; private set; }
        public bool CanAirJump => CurrentJumpCount > 0;
        
        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            _stateMachine = new StateMachine(states, this);
            
            GetStatFromComponent();
        }

        private void GetStatFromComponent()
        {
            var statCompo = GetCompo<EntityStat>();
            jumpPower = statCompo.GetStat(jumpPower);
            jumpCount = statCompo.GetStat(jumpCount);
        }

        private void Start()
        {
            _stateMachine.Initialize("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateFSM();
        }

        public void ChangeState(string newState)
            => _stateMachine.ChangeState(newState);
        
        public void DecreaseJumpCount() => CurrentJumpCount--;
        public void ResetJumpCount() => CurrentJumpCount = jumpCount.Value;
    }
}
