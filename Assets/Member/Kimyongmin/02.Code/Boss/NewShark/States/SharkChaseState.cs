using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark.States
{
    public class SharkChaseState : SharkState
    {
        public SharkChaseState(Shark shark, SharkStateMachine sharkStateMachine, string animBoolName) : base(shark, sharkStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            Debug.Log("Chase");
            Shark.SharkMovement.SetMoveDir(Shark.GetTargetDir());
            
            Shark.SkillTick();

            if (Shark.AttackInPlayer())
            {
                SharkStateMachine.ChangeState(SharkStateType.Attack);
                Shark.AttackBool(true);
            }

            if (Shark.CurrentCooltime > Shark.SkillCooltime)
            {
                if (Shark.ChargeStack > 2)
                {
                    SharkStateMachine.ChangeState(SharkStateType.ChargeSkill);
                    Shark.AttackBool(true);
                }
                
                switch (Random.Range(0,3))
                {
                    case 0:
                        SharkStateMachine.ChangeState(SharkStateType.BiteSkill);
                        Shark.AttackBool(true);
                        Shark.Charging();
                        break;
                    case 1:
                        SharkStateMachine.ChangeState(SharkStateType.LaserSkill);
                        Shark.AttackBool(true);
                        Shark.Charging();
                        break;
                    case 2:
                        SharkStateMachine.ChangeState(SharkStateType.RoarSkill);
                        Shark.AttackBool(true);
                        Shark.Charging();
                        break;
                    default:
                        SharkStateMachine.ChangeState(SharkStateType.ChargeSkill);
                        Shark.AttackBool(true);
                        break;
                }
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            Shark.ResetCooltime();
        }
    }
}
