using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    private EnemyStateMachine _enemyStateMachine;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        
        _enemyStateMachine = new EnemyStateMachine(this);
        _enemyStateMachine.AddState(StateType.Chase, new ChaseState(_enemy, _enemyStateMachine, "Chase"));
        _enemyStateMachine.AddState(StateType.Attack, new AttackState(_enemy, _enemyStateMachine, "Attack"));
    }

    private void Start()
    {
        _enemyStateMachine.Initialize(StateType.Chase);
    }

    private void Update()
    {
        if (_enemy.AttackInPlayer())
        {
            _enemyStateMachine.ChangeState(StateType.Attack);
        }
        else
        {
            _enemyStateMachine.ChangeState(StateType.Chase);
        }
        
        _enemyStateMachine.currentState.UpdateState();
    }
}
