using UnityEngine;

public class MoveValue
{
    public MoveValue(Transform trn, Rigidbody2D rb, PlayerMovementSO so, ValueByState valueByState)
    {
        Trn = trn;
        Rb = rb;
        MovementSO = so;
        RotateSpeed = valueByState.RotateSpeed;
    }

    public Transform Trn { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerMovementSO MovementSO { get; private set; }
    public float RotateSpeed { get; set; }
}