using Member.Kimyongmin._02.Code.Enemy.Enemy;
using Unity.Cinemachine;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class AttackState : EnemyState
    {
        private Jellyfish _jellyfish;
        public AttackState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.AgentMovement.SetSpeed(0, Enemy.EnemyDataSo.detectDelay);
            Enemy.Attack();
            
            if (Enemy is Jellyfish)
            {
                _jellyfish = Enemy as Jellyfish;
            }
        }

        public override void UpdateState()
        {
            if (!Enemy.AttackInPlayer() && !Enemy.IsAttack)
            {
                EnemyStateMachine.ChangeState(StateType.Idle);
            }

            if (_jellyfish != null)
                _jellyfish.LineSetting(Enemy.TargetPos());
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
