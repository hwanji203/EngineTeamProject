using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : EnemyState
{
    private float _chackDelay;
    private float _currentChackTime = 0;
    
    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _chackDelay = enemy.EnemyDataSo.detectDelay;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnterChase");
    }

    public override void UpdateState()
    {
        _currentChackTime += Time.deltaTime;
        
        if (_chackDelay < _currentChackTime && Enemy.EnemyDataSo.EnemyType != EnemyType.NotAggressive)
        {
            Enemy.AgentMovemant.SetMoveDir(Enemy.GetTarget());
            _currentChackTime = 0;
        }
        else
        {
            Enemy.AgentMovemant.SetMoveDir(-Enemy.GetTarget());
            _currentChackTime = 0;
        }

        if (Enemy.AttackInPlayer())
        {
            EnemyStateMachine.ChangeState(StateType.Attack);
        }

        Enemy.FilpX(-Enemy.GetTarget().x);

    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
