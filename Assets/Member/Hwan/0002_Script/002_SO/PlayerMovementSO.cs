using NUnit.Framework.Constraints;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "HwanSO/PlayerMovementSO")]
public class PlayerMovementSO : ScriptableObject
{
    [field: SerializeField]
    public ValueByState[] ValueByStates { get; private set; } =
    {
        new ValueByState(PlayerState.Idle, 0.325f, 750f),
        new ValueByState(PlayerState.Move, 0.325f, 250f),
        new ValueByState(PlayerState.Dash, 0f, 0f),
        new ValueByState(PlayerState.Flip, 0.325f, 275f),
        new ValueByState(PlayerState.ZeroStamina, 1f, 0f),
        new ValueByState(PlayerState.WaitForAttack, 0f, 0f),
    };
    [field: SerializeField] public PlayerSkillDictionarySO skillDictionarySO { get; private set; }

    [field: Header("DashAttack Setting")]
    [field: SerializeField] public float DashTime { get; private set; } = 0.35f;
    [field: SerializeField] public float DashPower { get; private set; } = 1350f;
    [field: SerializeField] public float DashDamping { get; private set; } = 10f;

    [field: Header("Rotate Setting")]
    [field: SerializeField] public float LimitPos { get; private set; } = 0.5f;

    [field: Header("Move Setting")]
    [field: SerializeField] public float Acceleration { get; private set; } = 700f;
    [field: SerializeField] public float DecreaseValue { get; private set; } = 3f;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 2f;
}

[Serializable]
public class ValueByState
{
    public ValueByState(PlayerState state, float gravity, float rotateSpeed)
    {
        State = state;
        Gravity = gravity;
        RotateSpeed = rotateSpeed;
    }
    [field: SerializeField] public PlayerState State { get; private set; }
    [field: SerializeField] public float Gravity { get; private set; }
    [field: SerializeField] public float RotateSpeed { get; private set; }
}