using System;
using System.Collections;
using Member.Hwan._0002_Script._002_SO;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatSo statSO;
    [SerializeField] private PlayerInputSO inputSO;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerStamina playerStamina;

    private (bool doing, PlayerAttackType attackType) currentAttackState;
    private bool DoMove;

    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.StatSO = statSO;
        playerStamina = GetComponent<PlayerStamina>();
        playerStamina.StatSO = statSO;

        InputInitialize();
        AnimationInitialize();
    }

    private void Update()
    {
        UpdateState();
    }

    private void AnimationInitialize()
    {
        playerAnimation.OnAttackStart += playerMovement.AttackMove;
        playerAnimation.OnAttackEnd += playerMovement.EndAttack;
        playerAnimation.OnAttackEnd += playerAttack.StartAttack;
        playerAnimation.OnAttackStart += playerAttack.Attack;
    }

    private void InputInitialize()
    {
        inputSO.OnMouseMove += (mousePos) => playerMovement.MousePos = mousePos;
        inputSO.OnSpaceBtnChanged += (performed) => DoMove = performed;
        inputSO.OnMouseClickChanged += (performed, attackType) => currentAttackState = (performed, attackType);
    }

    private bool TryAttack(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                if (playerAttack.DashCoolCoroutine != null) return false;
                if (playerStamina.TryMove(PlayerMoveType.Dash) == false) return false;
                playerAnimation.ChangeAnimation(PlayerState.Dash);
                break;
            case PlayerAttackType.Flip:
                if (playerAttack.FlipCoolCoroutine != null) return false;
                playerAnimation.ChangeAnimation(PlayerState.Flip);
                break;
        }

        playerMovement.StartAttack();

        return true;
    }

    private void UpdateState()
    {
        playerMovement.Rotate();
        if (playerMovement.CanMove == false) return;

        if (currentAttackState.doing == true && playerAnimation.CanAttack() == true)
        {
            if (TryAttack(currentAttackState.attackType) == true) return;
        }

        playerAnimation.ChangeAnimation(PlayerState.Idle);
        playerMovement.SetRotateSpeed(PlayerState.Idle);

        if (DoMove == false || playerStamina.TryMove(PlayerMoveType.Swim) == false) return;

        playerMovement.SetRotateSpeed(PlayerState.Move);
        playerAnimation.ChangeAnimation(PlayerState.Move);
        playerMovement.GoDirectionMove();
    }
}
