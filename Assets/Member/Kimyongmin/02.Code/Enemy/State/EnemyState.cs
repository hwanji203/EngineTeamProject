using UnityEngine;

public abstract class EnemyState
{
    protected Enemy Enemy;
    protected EnemyStateMachine EnemyStateMachine;
    protected string AnimBoolName;
    
    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName)
    {
        Enemy = enemy;
        EnemyStateMachine = enemyStateMachine;
        AnimBoolName = animBoolName;
    }
    public virtual void EnterState()
    {
        
    }

    public virtual void UpdateState()
    {
        
    }
    
    public virtual void ExitState()
    {
        
    }
}
