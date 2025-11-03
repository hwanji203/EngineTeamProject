using Member.Kimyongmin._02.Code.Enemy.State;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class PuffleBrain : EnemyBrain
    {
        private new void Awake()
        {
            base.Awake();
            EnemyStateMachine.AddState(StateType.Idle, new IdleState(Enemy, EnemyStateMachine, "Idle"));
            EnemyStateMachine.AddState(StateType.Chase, new ChaseState(Enemy, EnemyStateMachine, "Chase"));
            EnemyStateMachine.AddState(StateType.Attack, new AttackState(Enemy, EnemyStateMachine, "Attack"));
        }

        private new void Start()
        {
            base.Start();
            EnemyStateMachine.Initialize(StateType.Idle);
        }

        private void Update()
        {
            EnemyStateMachine.currentState.UpdateState();
        }
    }
}
