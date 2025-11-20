using System.Collections;
using System.Collections.Generic;
using Member.Kimyongmin._02.Code.Boss.NewShark;
using UnityEngine;

public enum SharkStateType
{ 
    Chase,
    Attack,
    LaserSkill,
    RoarSkill,
    ChargeSkill,
    Hit,
    Stun,
    Dead
}
public class SharkStateMachine
{
    public Dictionary<SharkStateType, SharkState> StateDictionary = new();

    public SharkState currentState;
    public SharkBrain StateSharkBrain {get; private set;}

    public SharkStateMachine(SharkBrain sharkBrain)
    {
        StateSharkBrain = sharkBrain;
    }

    public void Initialize(SharkStateType startState)
    {
        currentState = StateDictionary[startState];
        currentState.EnterState();
    }

    public void AddState(SharkStateType stateType, SharkState state)
    {
        StateDictionary.Add(stateType, state);
    }

    public void ChangeState(SharkStateType stateType)
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
