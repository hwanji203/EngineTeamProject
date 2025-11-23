using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkStunState : SharkState
    {
        private float _currentTime;
        private readonly int _hitHash = Animator.StringToHash("Hit");
        
        public SharkStunState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            _currentTime = 0;
            Shark.SharkMovement.SetSpeed(0);
            Shark.SharkMovement.RbCompo.linearVelocity = Vector3.zero;
            
            Shark.Healthsystem.SetInvincibility(false);
        }

        public override void UpdateState()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= Shark.SharkData.StunDuration)
            {
                Shark.RoarDir = -1;
                SharkStateMachine.ChangeState(SharkStateType.RoarSkill);
            }
            if (Shark.Healthsystem.Health <= 0)
            {
                Shark.RoarDir = -1;
                SharkStateMachine.ChangeState(SharkStateType.Dead);
                Shark.Death();
            }
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
            Shark.Healthsystem.SetInvincibility(true);
        }
    }
}
