using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "HwanSO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    [field: SerializeField] public StaminaValue[] staminaValues;
    [field: SerializeField] public float MaxStamina { get; private set; } = 5f;
    [field: SerializeField] public float DefaultDmg { get; private set; } = 10f;
    [field: SerializeField] public float FlipCool { get; private set; } = 1f;
    [field: SerializeField] public float DashCool { get; private set; } = 0.35f;
    [field: SerializeField] public int MaxSkillCount { get; private set; } = 3;
}

[Serializable]
public class StaminaValue
{
    [field: SerializeField] public PlayerMoveType MoveType { get; private set; }
    [field: SerializeField] public float UseStamina { get; private set; }
    [field: SerializeField] public float UseTime { get; private set; }
    [field: SerializeField] public bool IsOneShot { get; private set; }
}
