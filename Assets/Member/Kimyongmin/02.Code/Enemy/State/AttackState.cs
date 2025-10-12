using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName) : base(enemy, enemyStateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnterAttack");
        Enemy.AgentMovemant.SetSpeed(0);
    }

    public override void UpdateState()
    {
        
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
