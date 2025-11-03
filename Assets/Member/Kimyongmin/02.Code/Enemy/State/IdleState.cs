using Member.Kimyongmin._02.Code.Enemy.SO;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class IdleState : EnemyState
    {
        private float _standTime = 0;
        private float _currentTime;
        public IdleState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.AgentMovement.SetSpeed(Enemy.EnemyDataSo.idleSpeed,Enemy.EnemyDataSo.detectDelay);
            Enemy.IsAttack = false;
            Enemy.AgentMovement.IsDashing = false;
        }

        public override void UpdateState()
        {
            Vector2 moveDir = Vector2.zero;
        
            _currentTime += Time.deltaTime;

            if (_standTime < _currentTime)
            {
                _currentTime = 0;
                _standTime = Random.Range(1f, 3f);
                moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                Enemy.AgentMovement.SetMoveDir(moveDir);
            }

            if (Enemy.AttackInPlayer())
            {
                EnemyStateMachine.ChangeState(StateType.Attack);
            }
            else if (Enemy.ChaseInPlayer())
            {
                EnemyStateMachine.ChangeState(StateType.Chase);
            }
            
            Enemy.FilpX(moveDir.x);
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
