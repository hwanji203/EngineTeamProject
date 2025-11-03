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
            if (!Enemy.AttackInPlayer() && !Enemy.IsAttack)
            {
                EnemyStateMachine.ChangeState(StateType.Idle);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
