using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSO movementSO;
    [SerializeField] private Transform visualTrn;
    private Rigidbody2D rb;

    public bool DoMove { get; private set; } = false;
    public bool CanMove { get; private set; } = true;
    public Vector2 MousePos { get; set; }
    public Vector2 MoveDir { get; private set; }
    public event Action<PlayerState> OnStateChange;

    private Dictionary<PlayerState, MoveValue> moveValueDictionary = new();
    private Dictionary<PlayerMovementType, Movement> movementDictionary = new();

    private bool isStaminaZero = false;
    private bool isDashing = false;
    public PlayerState CurrentState { get; private set; }

    private void Awake()
    {
        AddDictionary();

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.linearDamping = movementSO.DecreaseValue;

        foreach (ValueByState value in movementSO.ValueByStates)
        {
            moveValueDictionary.Add(value.State, new MoveValue(transform, rb, movementSO, value));
        }

        ChangeState(PlayerState.Idle);
    }

    private void AddDictionary()
    {
        movementDictionary.Add(PlayerMovementType.Dash, new DashMovement());
        movementDictionary.Add(PlayerMovementType.Flip, new FlipMovement());
        movementDictionary.Add(PlayerMovementType.Swim, new SwimMovement());
        movementDictionary.Add(PlayerMovementType.Rotate, new RotateMovement());
    }

    public void ChangeState(PlayerState state)
    {
        PlayerState tempState = state;
        if (isStaminaZero == true && isDashing == false)
        {
            tempState = PlayerState.ZeroStamina;
        }
        if (tempState == CurrentState) return;
        CurrentState = tempState;
        rb.gravityScale = moveValueDictionary[CurrentState].GravityScale;
        OnStateChange?.Invoke(CurrentState);
    }

    public void EndAttack(PlayerAttackType type)
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
        rb.linearDamping = movementSO.DecreaseValue;
        CanMove = true;
    }

    public void StartAttack(PlayerAttackType type)
    {
        if (type == PlayerAttackType.Dash) isDashing = true;
        else SoundManager.Instance.Play(SFXSoundType.Spin);
            rb.linearVelocity = Vector2.zero;
        ChangeState(PlayerState.WaitForAttack);
        CanMove = false;
    }

    public void Move(PlayerMovementType type)
    {
        movementDictionary[type].Move(moveValueDictionary[CurrentState],
            CurrentState == PlayerState.ZeroStamina ? transform.position + Vector3.down : MousePos);
        switch (type)
        {
            case PlayerMovementType.Dash:
                ChangeState(PlayerState.Dash);
                break;
            case PlayerMovementType.Flip:
                ChangeState(PlayerState.Flip);
                break;
        }
    }

    public void GetStaminaIsZero(float value)
    {
        if (value == 0)
        {
            isStaminaZero = true;
            EndAttack(PlayerAttackType.Dash);
        }
        isStaminaZero = value == 0;
    }

    public void Damaged(Vector2 enemyReDir)
    {
        if (isStaminaZero == true) return;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(enemyReDir, ForceMode2D.Impulse);
        StartCoroutine(StunCoroutine(movementSO.StunTime));
    }

    private IEnumerator StunCoroutine(float waitTime)
    {
        CanMove = false;
        yield return new WaitForSeconds(waitTime);
        CanMove = true;
    }
}