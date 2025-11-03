using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "HwanSO/PlayerMovementSO")]
public class PlayerMovementSO : ScriptableObject
{
    [field: Header("DashAttack Setting")]
    [field: SerializeField] public float dashPower { get; private set; } = 1350f;
    [field: SerializeField] public float dashDamping { get; private set; } = 10f;

    [field: Header("Rotate Setting")]
    [field: SerializeField] public float rotateSpeed { get; private set; } = 750f;
    [field: SerializeField] public float limitPos { get; private set; } = 0.5f;

    [field: Header("Move Setting")]
    [field: SerializeField] public float acceleration { get; private set; } = 600f;
    [field: SerializeField] public float decreaseValue { get; private set; } = 3f;
    [field: SerializeField] public float maxSpeed { get; private set; } = 1.65f;
    [field: SerializeField] public float moveRotSpeed { get; private set; } = 250f;

    [field: Header("FlipAttack Setting")]
    [field: SerializeField] public float attackTime { get; private set; } = 0.4f;
    [field: SerializeField] public float flipRotSpeed { get; private set; } = 375f;

    [Header("Others")]
    [field: SerializeField] public float gravityScale { get; private set; } = 0.325f;
}
