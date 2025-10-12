using System.Collections.Generic;
using UnityEngine;

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
    private EnemyBrain _stateBrain;

    public EnemyStateMachine(EnemyBrain brain)
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
