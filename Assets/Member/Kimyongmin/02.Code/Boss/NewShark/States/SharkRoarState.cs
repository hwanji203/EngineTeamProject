using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkRoarState : SharkState
    {
        private float _currentTime;
        public SharkRoarState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            _currentTime = 0;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public void SetRoarDir(float a)
        {
            
        }
    }
}
