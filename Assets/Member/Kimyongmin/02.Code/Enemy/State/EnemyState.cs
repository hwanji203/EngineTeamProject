using Member.Kimyongmin._02.Code.Enemy;
using Member.Kimyongmin._02.Code.Enemy.SO;
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
        if (Enemy.EnemyDataSo.EnemyType != EnemyType.NotAggressive)
            Enemy.Animator.SetBool(AnimBoolName,true);
    }

    public virtual void UpdateState()
    {
        
    }
    
    public virtual void ExitState()
    {
        if (Enemy.EnemyDataSo.EnemyType != EnemyType.NotAggressive)
            Enemy.Animator.SetBool(AnimBoolName, false);
    }
}
