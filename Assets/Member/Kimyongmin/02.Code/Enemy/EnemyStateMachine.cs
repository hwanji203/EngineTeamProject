using System.Collections.Generic;

namespace Member.Kimyongmin._02.Code.Enemy
{
    public enum StateType
    { 
        Idle,
        Chase,
        Attack
    }
    public class EnemyStateMachine
    {
        public Dictionary<StateType, EnemyState> StateDictionary = new();

        public EnemyState currentState;
        private IBrain _stateBrain;

        public EnemyStateMachine(IBrain brain)
        {
            _stateBrain = brain;
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
            if (currentState != null)
                currentState.ExitState();
        
            currentState = StateDictionary[stateType];
        
            if (currentState != null)
                currentState.EnterState();
        }
    }
}