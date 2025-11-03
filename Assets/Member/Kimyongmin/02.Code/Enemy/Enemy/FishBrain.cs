using Member.Kimyongmin._02.Code.Enemy.State;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class FishBrain : EnemyBrain
    {

        private new void Awake()
        {
            base.Awake();
            EnemyStateMachine.AddState(StateType.Idle, new IdleState(Enemy, EnemyStateMachine, "Idle"));
            EnemyStateMachine.AddState(StateType.Chase, new ChaseState(Enemy, EnemyStateMachine, "Chase"));
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
