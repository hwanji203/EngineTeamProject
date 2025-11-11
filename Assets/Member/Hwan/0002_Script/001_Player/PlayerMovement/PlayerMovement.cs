using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSO movementSO;
    [SerializeField] private Transform visualTrn;
    private Rigidbody2D rb;

    public bool DoMove { get; private set; } = false;
    public bool CanMove { get; private set; } = true;
    public Vector2 MoveDir { get; private set; }
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    private Dictionary<PlayerState, ValueByState> valueByStateDictionary = new();
    private Dictionary<PlayerMovementType, IMovement> movementDictionary = new();

    private bool isStaminaZero = false;
    private bool isDashing = false;
    public MoveValue CurrentValue { get; private set; }

    private void Awake()
    {
        foreach (ValueByState value in movementSO.ValueByStates)
        {
            valueByStateDictionary.Add(value.State, value);
        }
        AddDictionary();

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.linearDamping = movementSO.DecreaseValue;

        CurrentValue = new MoveValue(transform, rb, movementSO);
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
        ValueByState value = valueByStateDictionary[state];
        if (isStaminaZero == true && isDashing == false)
        {
            value = valueByStateDictionary[PlayerState.ZeroStamina];
        }
        CurrentValue.RotateSpeed = value.RotateSpeed;
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
        rb.linearVelocity = Vector2.zero;
        ChangeState(PlayerState.WaitForAttack);
        CanMove = false;
    }

    public void Move(PlayerMovementType type)
    {
        movementDictionary[type].Move(CurrentValue);
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
        isStaminaZero = value == 0;
    }

    public void Damaged(Vector2 enemyPos)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(((Vector2)transform.position - enemyPos), ForceMode2D.Impulse);
        StartCoroutine(StunCoroutine(movementSO.StunTime));
    }

    private IEnumerator StunCoroutine(float waitTime)
    {
        CanMove = false;
        yield return new WaitForSeconds(waitTime);
        CanMove = true;
    }
}