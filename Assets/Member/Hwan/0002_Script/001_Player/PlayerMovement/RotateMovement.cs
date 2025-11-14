using UnityEngine;

public class RotateMovement : Movement
{
    public override void Move(MoveValue moveValue, Vector2 mousePos)
    {
        Vector2 dir = mousePos - (Vector2)moveValue.Trn.position;

        float targetRad = Mathf.Atan2(dir.y, dir.x);
        float targetDeg = Mathf.MoveTowardsAngle(moveValue.Trn.eulerAngles.z, targetRad * Mathf.Rad2Deg, moveValue.RotateSpeed * Time.deltaTime);

        Vector2 moveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));
        moveValue.Trn.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x)) * Mathf.Rad2Deg);
    }
}
