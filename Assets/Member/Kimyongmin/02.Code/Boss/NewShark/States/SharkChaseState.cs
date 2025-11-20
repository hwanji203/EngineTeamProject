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
            Shark.SharkMovement.SetMoveDir(Shark.GetTargetDir());
            
            Shark.SkillTick();
            
            Shark.FilpX(Shark.GetTargetDir().x);

            if (Shark.AttackInPlayer() && !Shark.IsAttack)
            {
                Shark.AttackBool(true);
                SharkStateMachine.ChangeState(SharkStateType.Attack);
            }

            if (Shark.CurrentCooltime > Shark.SkillCooltime)
            {
                if (Shark.ChargeStack > 2)
                {
                    Shark.AttackBool(true);
                    SharkStateMachine.ChangeState(SharkStateType.ChargeSkill);
                }
                
                switch (Random.Range(0,3))
                {
                    case 0:
                        Shark.AttackBool(true);
                        SharkStateMachine.ChangeState(SharkStateType.BiteSkill);
                        Shark.Charging();
                        break;
                    case 1:
                        Shark.AttackBool(true);
                        SharkStateMachine.ChangeState(SharkStateType.LaserSkill);
                        Shark.Charging();
                        break;
                    case 2:
                        Shark.AttackBool(true);
                        SharkStateMachine.ChangeState(SharkStateType.RoarSkill);
                        Shark.Charging();
                        break;
                    default:
                        Shark.AttackBool(true);
                        SharkStateMachine.ChangeState(SharkStateType.ChargeSkill);
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
