using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "HwanSO/PlayerMovementSO")]
public class PlayerMovementSO : ScriptableObject
{
    [field: SerializeField] public PlayerSkillDictionarySO skillDictionarySO { get; private set; }

    [field: Header("DashAttack Setting")]
    [field: SerializeField] public float DashTime { get; private set; } = 0.35f;
    [field: SerializeField] public float DashPower { get; private set; } = 1350f;
    [field: SerializeField] public float DashDamping { get; private set; } = 10f;

    [field: Header("Rotate Setting")]
    [field: SerializeField] public float RotateSpeed { get; private set; } = 750f;
    [field: SerializeField] public float LimitPos { get; private set; } = 0.5f;

    [field: Header("Move Setting")]
    [field: SerializeField] public float Acceleration { get; private set; } = 700f;
    [field: SerializeField] public float DecreaseValue { get; private set; } = 3f;
    [field: SerializeField] public float MaxSpeed { get; private set; } = 2f;
    [field: SerializeField] public float MoveRotSpeed { get; private set; } = 250f;

    [field: Header("FlipAttack Setting")]
    //[field: SerializeField] public float attackTime { get; private set; } = 0.4f;
    [field: SerializeField] public float FlipRotSpeed { get; private set; } = 275;

    [Header("Others")]
    [field: SerializeField] public float GravityScale { get; private set; } = 0.325f;
}
