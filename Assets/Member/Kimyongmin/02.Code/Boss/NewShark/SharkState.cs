using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkState
    {
        protected Shark Shark;
        protected SharkStateMachine SharkStateMachine;
        protected string AnimBoolName;
    
        public SharkState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName)
        {
            Shark = shark;
            SharkStateMachine = sharkStateMachine;
            AnimBoolName = animBoolName;
        }
        public virtual void EnterState()
        {
            Shark.Animator.SetBool(AnimBoolName, true);
        }

        public virtual void UpdateState()
        {
            
        }
    
        public virtual void ExitState()
        {
            Shark.Animator.SetBool(AnimBoolName, false);
        }
    }
}
