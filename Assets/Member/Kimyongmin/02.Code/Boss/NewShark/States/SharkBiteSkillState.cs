using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkBiteSkillState : SharkState
    {
        public SharkBiteSkillState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Debug.Log("바이트");
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
