using System.Collections.Generic;
using Member.Kimyongmin._02.Code.Boss.NewShark;

public enum SharkStateType
{ 
    Idle,
    Chase,
    Attack,
    BiteSkill,
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
    private SharkBrain _stateSharkBrain;

    public SharkStateMachine(SharkBrain sharkBrain)
    {
        _stateSharkBrain = sharkBrain;
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
