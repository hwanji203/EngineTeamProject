using UnityEngine;

public class SwimMovement : Movement
{
    public override void Move(MoveValue moveValue, Vector2 mousePos)
    {
        moveValue.Rb.AddForce(GetMoveDir(moveValue.Trn) * Time.deltaTime * moveValue.MovementSO.Acceleration, ForceMode2D.Force);
        moveValue.Rb.linearVelocity = moveValue.Rb.linearVelocity.normalized * Mathf.Clamp(moveValue.Rb.linearVelocity.magnitude, 0, moveValue.MovementSO.MaxSpeed);
    }
}
