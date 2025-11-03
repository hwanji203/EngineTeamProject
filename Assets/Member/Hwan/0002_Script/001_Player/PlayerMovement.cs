using DG.Tweening;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSO movementSO;
    [SerializeField] private Transform visualTrn;

    private Rigidbody2D rb;
    public Vector2 MousePos { get; set; }
    public bool DoMove { get; private set; }
    public bool CanMove { get; private set; }
    public Vector2 MoveDir { get; private set; }
    public NotifyValue<float> PlayerYValue { get; private set; } = new();

    private bool isFlipping;
    private float rotateSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.linearDamping = movementSO.DecreaseValue;
        rb.gravityScale = movementSO.GravityScale;

        CanMove = true;
        DoMove = false;

        rotateSpeed = movementSO.RotateSpeed;
    }

    public void UpdateYValue() { PlayerYValue.Value = transform.position.y; }

    public void SetRotateSpeed(PlayerState stateType)
    {
        switch(stateType)
        {
            case PlayerState.Flip:
                rotateSpeed = movementSO.FlipRotSpeed;
                break;
            case PlayerState.Idle:
                rotateSpeed = movementSO.RotateSpeed;
                break;
            case PlayerState.Move:
                rotateSpeed = movementSO.MoveRotSpeed;
                break;
        }
    }

    public void StartAttack()
    {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        CanMove = false;
    }

    public void GoDirectionMove()
    {
        rb.AddForce(MoveDir * Time.deltaTime * movementSO.Acceleration, ForceMode2D.Force);
        rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Clamp(rb.linearVelocity.magnitude, 0, movementSO.MaxSpeed);
    }
    
    public void Rotate()
    {
        if (!(isFlipping == true || CanMove == true)) return;
        if ((MousePos - (Vector2)transform.position).magnitude < movementSO.LimitPos) return;

        float targetRad = Mathf.Atan2(MousePos.y - transform.position.y, MousePos.x - transform.position.x);
        float targetDeg = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRad * Mathf.Rad2Deg, rotateSpeed * Time.deltaTime);

        MoveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(MoveDir.y, MoveDir.x)) * Mathf.Rad2Deg);
    }

    public void AttackMove(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                rb.AddForce(MoveDir * movementSO.DashPower, ForceMode2D.Force);
                rb.linearDamping = movementSO.DashDamping;
                break;
            case PlayerAttackType.Flip:
                isFlipping = true;
                SetRotateSpeed(PlayerState.Flip);
                visualTrn.DOBlendableRotateBy(new Vector3(0, 0, 360f), movementSO.skillDictionarySO.Dictionary[PlayerSkillType.Flip].AttackTime, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCirc);
                break;
        }
    }
    
    public void EndAttack(PlayerAttackType type)
    {
        isFlipping = false;
        SetRotateSpeed(PlayerState.Idle);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = movementSO.GravityScale;
        rb.linearDamping = movementSO.DashDamping;
        CanMove = true;
    }
}