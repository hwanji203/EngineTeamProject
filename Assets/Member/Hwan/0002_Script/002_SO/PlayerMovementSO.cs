using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "HwanSO/PlayerMovementSO")]
public class PlayerMovementSO : ScriptableObject
{
    [Header("DashAttack Setting")]
    public float dashTime; 
    public float dashPower;
    public float dashDamping;

    [Header("Rotate Setting")]
    public float rotateSpeed;
    public float limitPos;

    [Header("Move Setting")]
    public float acceleration;
    public float decreaseValue;
    public float maxSpeed;

    [Header("FlipAttack Setting")]
    public float attackSpeed;
    public float attackTime;

    [Header("Others")]
    public float gravityScale;
}
