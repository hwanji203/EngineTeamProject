using UnityEngine;

public class DashMovement : Movement
{
    public override void Move(MoveValue moveValue, Vector2 mousePos)
    {
        moveValue.Rb.AddForce(GetMoveDir(moveValue.Trn) * moveValue.MovementSO.DashPower, ForceMode2D.Force);
        moveValue.Rb.linearDamping = moveValue.MovementSO.DashDamping;
    }
}
