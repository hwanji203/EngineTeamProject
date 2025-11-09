using DG.Tweening;
using Member.Kimyongmin._02.Code.Enemy.State;
using System;
using System.Collections.Generic;
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
    public NotifyValue<float> PlayerRotYValue { get; private set; } = new();

    private Dictionary<PlayerState, ValueByState> valueByStateDictionary = new();
    private float rotateSpeed;
    private bool isFlipping = false;
    private bool isStaminaZero = false;
    private bool isDashing = false;

    private void Awake()
    {
        foreach (ValueByState value in movementSO.ValueByStates)
        {
            valueByStateDictionary.Add(value.State, value);
        }

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.linearDamping = movementSO.DecreaseValue;

        CanMove = true;
        DoMove = false;

        ChangeState(PlayerState.Idle);
    }

    public void UpdateRotYValue() 
    {
        PlayerRotYValue.Value = transform.position.y; 
    }

    public void ChangeState(PlayerState state)
    {
        Debug.Log(state);
        ValueByState currentState = valueByStateDictionary[state];
        if (isStaminaZero == true && isDashing == false)
        {
            currentState = valueByStateDictionary[PlayerState.ZeroStamina];
        }
        rb.gravityScale =  currentState.Gravity;
        rotateSpeed = currentState.RotateSpeed;
    }

    public void StartAttack(PlayerAttackType type)
    {
        if (type == PlayerAttackType.Dash) isDashing = true;
        rb.linearVelocity = Vector2.zero;
        ChangeState(PlayerState.WaitForAttack);
        CanMove = false;
    }

    public void GoDirectionMove()
    {
        rb.AddForce(MoveDir * Time.deltaTime * movementSO.Acceleration, ForceMode2D.Force);
        rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Clamp(rb.linearVelocity.magnitude, 0, movementSO.MaxSpeed);
    }
    
    public void Rotate()
    {
        if (isFlipping == true || CanMove == false) return;
        if ((MousePos - (Vector2)transform.position).magnitude < movementSO.LimitPos) return;

        RotateMove(MousePos - (Vector2)transform.position);
    }

    private void RotateMove(Vector2 rotateDir)
    {
        if (isStaminaZero == true) rotateDir = Vector2.down;

        float targetRad = Mathf.Atan2(rotateDir.y, rotateDir.x);
        float targetDeg = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRad * Mathf.Rad2Deg, rotateSpeed * Time.deltaTime);

        MoveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(MoveDir.y, MoveDir.x)) * Mathf.Rad2Deg);
    }

    public void AttackMove(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                ChangeState(PlayerState.Dash);
                rb.AddForce(MoveDir * movementSO.DashPower, ForceMode2D.Force);
                rb.linearDamping = movementSO.DashDamping;
                break;
            case PlayerAttackType.Flip:
                isFlipping = true;
                ChangeState(PlayerState.Flip);
                visualTrn.DOBlendableRotateBy(new Vector3(0, 0, 360f),
                    movementSO.skillDictionarySO.Dictionary[PlayerSkillType.Flip].AttackTime, RotateMode.FastBeyond360)
                    .SetEase(Ease.OutCirc);
                break;
        }
    }
    
    public void EndAttack(PlayerAttackType type)
    {
        isFlipping = false;
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
        rb.linearDamping = movementSO.DecreaseValue;
        CanMove = true;
    }

    public void GetStaminaIsZero(float value)
    {
        isStaminaZero = value == 0;
    }
}