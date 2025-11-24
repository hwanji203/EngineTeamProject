using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkChargeState : SharkState
    {
        public SharkChargeState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            SoundManager.Instance.Play(SFXSoundType.SharkSpin);
            Shark.AttackBool(true);
            Shark.ChargeBool(true);
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
            Shark.ChargeBool(false);
            Shark.AttackBool(false);
        }
    }
}
