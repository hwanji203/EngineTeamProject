using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class AttackState : EnemyState
    {
        public AttackState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.AgentMovemant.SetSpeed(0, 0);
            Enemy.Attack();
        }

        public override void UpdateState()
        {

        }

        public override void ExitState()
        {
            base.ExitState();
            if (!Enemy.AgentMovemant.IsDashing)
                Enemy.AgentMovemant.RbCompo.AddForce(-Enemy.GetTarget() * 2f, ForceMode2D.Impulse);
        }
    }
}
