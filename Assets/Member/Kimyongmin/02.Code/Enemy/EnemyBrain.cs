using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private EnemyStateMachine _enemyStateMachine;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        
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
        if (_enemy.AttackInPlayer())
        {
            _enemyStateMachine.ChangeState(StateType.Attack);
        }
        else if (_enemy.ChaseInPlayer())
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
