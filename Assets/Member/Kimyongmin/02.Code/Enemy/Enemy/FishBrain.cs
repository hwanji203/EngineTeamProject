using Member.Kimyongmin._02.Code.Enemy.State;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class FishBrain : MonoBehaviour, IBrain
    {
        private EnemyStateMachine _enemyStateMachine;
        private global::Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<global::Enemy>();
        
            _enemyStateMachine = new EnemyStateMachine(this);
            _enemyStateMachine.AddState(StateType.Idle, new IdleState(_enemy, _enemyStateMachine, "Idle"));
            _enemyStateMachine.AddState(StateType.Chase, new ChaseState(_enemy, _enemyStateMachine, "Chase"));
        }

        private void Start()
        {
            _enemyStateMachine.Initialize(StateType.Idle);
        }

        private void Update()
        {
            if (_enemy.ChaseInPlayer())
            {
                _enemyStateMachine.ChangeState(StateType.Chase);
            }
            else
            {
                _enemyStateMachine.ChangeState(StateType.Idle);
            }
        
            _enemyStateMachine.currentState.UpdateState();
        }
    }
}
