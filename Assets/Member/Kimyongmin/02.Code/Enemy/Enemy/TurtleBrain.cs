using Member.Kimyongmin._02.Code.Enemy.State;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class TurtleBrain : MonoBehaviour, IBrain
    {
        private EnemyStateMachine _enemyStateMachine;
        private global::Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<global::Enemy>();
        
            _enemyStateMachine = new EnemyStateMachine(this);
            _enemyStateMachine.AddState(StateType.Idle, new IdleState(_enemy, _enemyStateMachine, "Idle"));
            _enemyStateMachine.AddState(StateType.Chase, new ChaseState(_enemy, _enemyStateMachine, "Chase"));
            _enemyStateMachine.AddState(StateType.Attack, new AttackState(_enemy, _enemyStateMachine, "Attack"));
        }

        private void Start()
        {
            _enemyStateMachine.Initialize(StateType.Idle);
        }

        private void Update()
        {
            _enemyStateMachine.currentState.UpdateState();
        }

        public void ChaseChange()
        {
            _enemyStateMachine.ChangeState(StateType.Chase);
        }
        
    }
}
