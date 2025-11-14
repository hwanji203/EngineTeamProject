using Member.Kimyongmin._02.Code.Enemy.SO;

namespace Member.Kimyongmin._02.Code.Enemy.State
{
    public abstract class EnemyState
    {
        protected global::Enemy Enemy;
        protected EnemyStateMachine EnemyStateMachine;
        protected string AnimBoolName;
    
        public EnemyState(global::Enemy enemy, EnemyStateMachine enemyStateMachine, string animBoolName)
        {
            Enemy = enemy;
            EnemyStateMachine = enemyStateMachine;
            AnimBoolName = animBoolName;
        }
        public virtual void EnterState()
        {
            if (Enemy.EnemyDataSo.enemyType != EnemyType.NotAggressive)
                Enemy.Animator.SetBool(AnimBoolName,true);
        }

        public virtual void UpdateState()
        {
        
        }
    
        public virtual void ExitState()
        {
            if (Enemy.EnemyDataSo.enemyType != EnemyType.NotAggressive)
                Enemy.Animator.SetBool(AnimBoolName, false);
        }
    }
}
