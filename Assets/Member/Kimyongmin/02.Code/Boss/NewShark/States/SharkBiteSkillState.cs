using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkBiteSkillState : SharkState
    {
        private float _currentTime;
        private int _skillUsedNum;
        
        private readonly int _biteHash = Animator.StringToHash("Bite");
        public SharkBiteSkillState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            _currentTime = 0f;
            _skillUsedNum = 1;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("dd");
            _currentTime += Time.deltaTime;
            
            if (_skillUsedNum > 2)
                SharkStateMachine.ChangeState(SharkStateType.Chase);
                
            if (_currentTime > 1f)
            {
                _currentTime = 0f;
                Shark.Animator.SetBool(_biteHash, true);
                Shark.SharkSkills.Bite(0.9f, Shark.LayerMask, Shark.SharkData, Shark.SharkMovement.ShortDash, Shark.GetTargetDir(), 20);
                _skillUsedNum++;
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
