using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "HwanSO/PlayerMovementSO")]
public class PlayerMovementSO : ScriptableObject
{
    [Header("DashAttack Setting")]
    public float dashTime; 
    public float dashPower;

    [Header("Rotate Setting")]
    public float rotateSpeed;

    [Header("Move Setting")]
    public float acceleration;
    public float decreaseValue;
    public float maxSpeed;

    [Header("FlipAttack Setting")]
    public float attackSpeed;
    public float attackTime;
}
