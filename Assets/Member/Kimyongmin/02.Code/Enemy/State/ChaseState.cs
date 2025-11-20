using Member.Kimyongmin._02.Code.Enemy.Enemy;
using Member.Kimyongmin._02.Code.Enemy.SO;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public class ChaseState : EnemyState
    {
        private float _chackDelay;
        private float _currentChackTime = 0;
    
        public ChaseState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
        {
            _chackDelay = enemy.EnemyDataSo.detectDelay;
        }

        public override void EnterState()
        {
            base.EnterState();
            Enemy.transform.rotation = Quaternion.Euler(Enemy.transform.rotation.x,Enemy.transform.rotation.y,0);
            Enemy.AgentMovement.SetSpeed(Enemy.EnemyDataSo.moveSpeed,Enemy.EnemyDataSo.detectDelay);
            if (Enemy is Turtle turtle)
            {
                turtle.DashEnd();
            }
            
        }

        public override void UpdateState()
        {
            _currentChackTime += Time.deltaTime;
        
            if (_chackDelay < _currentChackTime && Enemy.EnemyDataSo.enemyType != EnemyType.NotAggressive)
            {
                Enemy.AgentMovement.SetMoveDir(Enemy.GetTarget());
                _currentChackTime = 0;
            }
            else
            {
                Enemy.AgentMovement.SetMoveDir(-Enemy.GetTarget());
                _currentChackTime = 0;
            }

            if (Enemy.AttackInPlayer() && Enemy.CanAttack)
            {
                EnemyStateMachine.ChangeState(StateType.Attack);
            }
            else if (!Enemy.ChaseInPlayer())
            {
                EnemyStateMachine.ChangeState(StateType.Idle);
            }

            if (Enemy.EnemyDataSo.enemyType == EnemyType.NotAggressive && !Enemy.IsAttack)
                Enemy.FilpX(-Enemy.GetTarget().x);
            else
                Enemy.FilpX(Enemy.GetTarget().x);

        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}
