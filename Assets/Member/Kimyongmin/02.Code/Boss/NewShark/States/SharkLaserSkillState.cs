using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkLaserSkillState : SharkState
    {
        public SharkLaserSkillState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Shark.transform.DOKill(true);
            Shark.SharkSkills.LaserFocusOn(Shark.GetTargetDir());
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
            Shark.transform.parent.DORotate(new  Vector3(Shark.transform.rotation.x, Shark.transform.rotation.y, 0), 0);
        }
    }
}
