using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkHitState : SharkState
    {
        private float _currentTime = 0;
        public SharkHitState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            if (0.1f < _currentTime)
                SharkStateMachine.ChangeState(SharkStateType.Chase);
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
