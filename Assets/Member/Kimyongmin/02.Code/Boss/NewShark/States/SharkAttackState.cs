
namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkAttackState : SharkState
    {
        private float _power;
        public SharkAttackState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Shark.SharkSkills.Bite(0.9f, Shark.LayerMask, Shark.SharkData, Shark.SharkMovement.ShortDash, Shark.GetTargetDir(), _power);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public void SetPower(float power)
        {
            _power = power;
        }
    }
}
