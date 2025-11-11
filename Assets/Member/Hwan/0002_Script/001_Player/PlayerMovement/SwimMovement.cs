using UnityEngine;

public class SwimMovement : IMovement
{
    public void Move(MoveValue moveValue)
    {
        moveValue.Rb.AddForce(moveValue.GetMoveDir() * Time.deltaTime * moveValue.MovementSO.Acceleration, ForceMode2D.Force);
        moveValue.Rb.linearVelocity = moveValue.Rb.linearVelocity.normalized * Mathf.Clamp(moveValue.Rb.linearVelocity.magnitude, 0, moveValue.MovementSO.MaxSpeed);
    }
}
