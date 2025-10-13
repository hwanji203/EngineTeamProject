using UnityEngine;

public class IdleState : EnemyState
{
    public IdleState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Enemy.AgentMovemant.SetStat(0,0);
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
