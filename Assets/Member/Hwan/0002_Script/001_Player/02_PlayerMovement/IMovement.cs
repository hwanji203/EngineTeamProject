using UnityEngine;

public abstract class Movement
{
    public abstract void Move(MoveValue moveValue, Vector2 mousePos);
    public Vector2 GetMoveDir(Transform trn)
    {
        float rad = trn.eulerAngles.z * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }
}
