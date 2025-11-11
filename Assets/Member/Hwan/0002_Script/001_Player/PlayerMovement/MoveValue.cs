using UnityEngine;

public class MoveValue
{
    public MoveValue(Transform trn, Rigidbody2D rb, PlayerMovementSO so)
    {
        Trn = trn;
        Rb = rb;
        MovementSO = so;
    }

    public Transform Trn { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerMovementSO MovementSO { get; private set; }
    public float RotateSpeed { get; set; }
    public Vector2 MousePos { get; set; }

    public Vector2 GetMoveDir()
    {
        float rad = Trn.eulerAngles.z * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}