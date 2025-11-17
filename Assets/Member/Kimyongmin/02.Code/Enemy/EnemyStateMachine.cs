using System.Collections.Generic;
using Member.Kimyongmin._02.Code.Enemy.State;

namespace Member.Kimyongmin._02.Code.Enemy
{
    public enum StateType
    { 
        Idle,
        Chase,
        Attack,
        Hit,
        Dead
    }
    public class EnemyStateMachine
    {
        public Dictionary<StateType, EnemyState> StateDictionary = new();

        public EnemyState currentState;
        private EnemyBrain _stateEnemyBrain;

        public EnemyStateMachine(EnemyBrain enemyBrain)
        {
            _stateEnemyBrain = enemyBrain;
        }

        public void Initialize(StateType startState)
        {
            currentState = StateDictionary[startState];
            currentState.EnterState();
        }

        public void AddState(StateType stateType, EnemyState state)
        {
            StateDictionary.Add(stateType, state);
        }

        public void ChangeState(StateType stateType)
        {
            if (currentState != StateDictionary[stateType])
            {
                if (currentState != null)
                    currentState.ExitState();
            
                currentState = StateDictionary[stateType];

                if (currentState != null)
                    currentState.EnterState();
            }
        }
    }
}