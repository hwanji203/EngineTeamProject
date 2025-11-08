using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerStatSO StatSO { get; private set; }
    [SerializeField] private PlayerInputSO inputSO;

    private PlayerAnimation playerAnimation;
    public PlayerMovement PlayerMovementCompo { get; private set; }
    private PlayerAttack playerAttackCompo;
    public PlayerStamina Stamina { get; private set; } = new PlayerStamina();

    private (bool doing, PlayerAttackType attackType) currentAttackState;
    private bool DoMove;
    private Vector2 MouseScreenPos => inputSO.MousePos;

    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        PlayerMovementCompo = GetComponent<PlayerMovement>();
        playerAttackCompo = GetComponent<PlayerAttack>();
        playerAttackCompo.SetStatSO(StatSO);

        Stamina.SetStatSO(StatSO);
        Stamina.Initialize();

        InputInitialize();
        AnimationInitialize();
    }

    private void Update()
    {
        PlayerMovementCompo.MousePos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        UpdateState();
        PlayerMovementCompo.UpdateRotYValue();
    }

    private void AnimationInitialize()
    {
        playerAnimation.OnAttackStart += PlayerMovementCompo.AttackMove;
        playerAnimation.OnAttackEnd += PlayerMovementCompo.EndAttack;
        playerAnimation.OnAttackStart += playerAttackCompo.Attack;
    }

    private void InputInitialize()
    {
        inputSO.OnSpaceBtnChanged += (performed) => DoMove = performed;
        inputSO.OnMouseClickChanged += (performed, attackType) => currentAttackState = (performed, attackType);
    }

    private bool TryAttack(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                if (playerAttackCompo.DashCoolCoroutine != null) return false;
                if (Stamina.TryMove(PlayerMoveType.Dash) == false) return false;
                playerAnimation.ChangeAnimation(PlayerState.Dash);
                break;
            case PlayerAttackType.Flip:
                if (playerAttackCompo.FlipCoolCoroutine != null) return false;
                playerAnimation.ChangeAnimation(PlayerState.Flip);
                break;
        }

        PlayerMovementCompo.StartAttack();

        return true;
    }

    private void UpdateState()
    {
        PlayerMovementCompo.Rotate();
        if (PlayerMovementCompo.CanMove == false) return;

        if (currentAttackState.doing == true && playerAnimation.CanAttack() == true)
        {
            if (TryAttack(currentAttackState.attackType) == true) return;
        }

        playerAnimation.ChangeAnimation(PlayerState.Idle);
        PlayerMovementCompo.SetRotateSpeed(PlayerState.Idle);

        if (DoMove == false || Stamina.TryMove(PlayerMoveType.Swim) == false) return;

        PlayerMovementCompo.SetRotateSpeed(PlayerState.Move);
        playerAnimation.ChangeAnimation(PlayerState.Move);
        PlayerMovementCompo.GoDirectionMove();
    }
}