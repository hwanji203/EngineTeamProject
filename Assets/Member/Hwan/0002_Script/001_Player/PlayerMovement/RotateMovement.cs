using UnityEngine;

public class RotateMovement : IMovement
{
    public Vector2 MoveDir { get; private set; }

    public void Move(MoveValue moveValue)
    {
        Vector2 dir = moveValue.MousePos - (Vector2)moveValue.Trn.position;

        float targetRad = Mathf.Atan2(dir.y, dir.x);
        float targetDeg = Mathf.MoveTowardsAngle(moveValue.Trn.eulerAngles.z, targetRad * Mathf.Rad2Deg, moveValue.RotateSpeed * Time.deltaTime);

        MoveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));
        moveValue.Trn.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(MoveDir.y, MoveDir.x)) * Mathf.Rad2Deg);
    }
}
