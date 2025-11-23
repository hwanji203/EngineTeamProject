using DG.Tweening;
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
            Shark.transform.DORotate(new Vector3(0, Shark.GetTargetDir().x < 0 ? 180 : 0, 0), 0);
        }

        private int _skillSelectNum = 0;
        public override void UpdateState()
        {
            base.UpdateState();
            Shark.SharkMovement.SetMoveDir(Shark.GetTargetDir());
            
            Shark.CooltimeTick();
            
            if (!Shark.IsAttack)
                Shark.FilpX(Shark.GetTargetDir().x);
            

            if (Shark.AttackInPlayer() && Shark.CurrentAttackCool > Shark.SharkData.AttackDelay)
            {
                Shark.AttackBool(true);
                Shark.ResetAttackCooltime();
                Shark.SkillCoolPlus(2);
                SharkAttackState attackState = (SharkAttackState)SharkStateMachine.StateDictionary[SharkStateType.Attack];
                attackState.SetPower(5);
                SharkStateMachine.ChangeState(SharkStateType.Attack);
            }

            if (Shark.CurrentCooltime > Shark.SkillCooltime)
            {
                _skillSelectNum = Random.Range(0, 3);
                if (Shark.ChargeStack > Random.Range(1,3))
                {
                    _skillSelectNum = 3;
                    Shark.ResetCharging();
                }
                
                switch (_skillSelectNum)
                {
                    case 0:
                        Shark.AttackBool(true);
                        SharkStateMachine.StateSharkBrain.BiteAttack();
                        Shark.Charging();
                        break;
                    case 1:
                        Shark.AttackBool(true);
                        SharkStateMachine.ChangeState(SharkStateType.LaserSkill);
                        Shark.Charging();
                        break;
                    case 2:
                        Shark.AttackBool(true);
                        Shark.RoarDir = 1;
                        SharkStateMachine.ChangeState(SharkStateType.RoarSkill);
                        Shark.Charging();
                        break;
                    default:
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
