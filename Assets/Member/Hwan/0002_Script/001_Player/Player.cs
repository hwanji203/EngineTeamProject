using Member.Kimyongmin._02.Code.Enemy;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statSO;
    [SerializeField] private PlayerInputSO inputSO;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerStamina playerStamina;

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

    private void AnimationInitialize()
    {
        playerMovement.OnMoveChange += playerAnimation.ChangeAnimation;
        playerAnimation.OnAttackChange += playerMovement.AttackMove;
        playerAnimation.OnAttackChange += (PlayerAttackType type, bool start) => { if (start == true) playerAttack.Attack(type); };
    }

    private void InputInitialize()
    {
        inputSO.OnMouseMove += (mousePos) => playerMovement.MousePos = mousePos;
        inputSO.OnSpaceBtnChanged += playerMovement.ChangeMove;
        inputSO.OnMouseClick += OnClick;
    }

    private void OnClick()
    {
        if (playerMovement.CanMove == false) return;

        playerMovement.StartAttack();

        if (playerMovement.DoMove)
        {
            playerAnimation.ChangeAnimation(PlayerState.Dash);
        }
        else
        {
            playerAnimation.ChangeAnimation(PlayerState.Attack);
        }
    }

    private void Update()
    {
        MovementUpdate();
    }

    private void MovementUpdate()
    {
        if (playerMovement.CanMove == false) return;

        playerMovement.Rotate();
        playerMovement.Move();
    }
}
