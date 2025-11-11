using UnityEngine;

public class DashMovement : IMovement
{
    public void Move(MoveValue moveValue)
    {
        moveValue.Rb.AddForce(moveValue.GetMoveDir() * moveValue.MovementSO.DashPower, ForceMode2D.Force);
        moveValue.Rb.linearDamping = moveValue.MovementSO.DashDamping;
    }
}
