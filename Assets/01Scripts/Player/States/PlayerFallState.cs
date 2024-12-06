using GGM.Animators;
using GGM.Entities;

namespace GGM.Players
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_mover.IsGroundDetected())
            {
                _player.ResetJumpCount();
                _player.ChangeState("IDLE");
            }
        }
    }
}
