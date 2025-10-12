using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : EnemyState
{
    private float _chackDelay;
    private float _currentChackTime = 0;
    
    public ChaseState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
        _chackDelay = enemy.EnemyData.detectDelay;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnterChase");
    }

    public override void UpdateState()
    {
        Enemy.AgentMovemant.SetSpeed(Enemy.EnemyData.moveSpeed);
        
        if (_chackDelay < _currentChackTime)
        {
            Enemy.AgentMovemant.SetMoveDir(Enemy.GetTarget());
            _currentChackTime = 0;
        }

        if (Enemy.AttackInPlayer())
        {
            EnemyStateMachine.ChangeState(StateType.Attack);
        }
        
        _currentChackTime += Time.deltaTime;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
