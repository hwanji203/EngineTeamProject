using System;
using Member.Kimyongmin._02.Code.Boss.NewShark.States;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkBrain : MonoBehaviour
    {
        protected SharkStateMachine SharkStateMachine;
        protected Shark Shark;

        private void Awake()
        {
            Shark = GetComponent<Shark>();
            SharkStateMachine = new SharkStateMachine(this);
            
            SharkStateMachine.AddState(SharkStateType.Chase, new SharkChaseState(Shark, SharkStateMachine,"Chase"));
            SharkStateMachine.AddState(SharkStateType.Attack, new SharkAttackState(Shark, SharkStateMachine,"Attack"));
            SharkStateMachine.AddState(SharkStateType.BiteSkill, new SharkBiteSkillState(Shark, SharkStateMachine,"Bite"));
            SharkStateMachine.AddState(SharkStateType.ChargeSkill, new SharkChargeState(Shark, SharkStateMachine,"Charge"));
            SharkStateMachine.AddState(SharkStateType.RoarSkill, new SharkRoarState(Shark, SharkStateMachine,"Roar"));
            SharkStateMachine.AddState(SharkStateType.LaserSkill, new SharkLaserSkillState(Shark, SharkStateMachine,"Laser"));
            SharkStateMachine.AddState(SharkStateType.Hit, new SharkHitState(Shark, SharkStateMachine,"Hit"));
            SharkStateMachine.AddState(SharkStateType.Dead, new SharkDeadState(Shark, SharkStateMachine,"Dead"));
        }

        private void Start()
        {
            SharkStateMachine.Initialize(SharkStateType.Chase);
        }

        private void Update()
        {
            SharkStateMachine.currentState.UpdateState();
        }

        private void SkillState(int skillNumber)
        {
            
        }
    }
}
