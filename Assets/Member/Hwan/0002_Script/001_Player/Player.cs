using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStatSO statSO;
    [SerializeField] private PlayerInputSO inputSO;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    private bool IsMoving;

    private void Awake()
    {
        playerAnimation = GetComponentInChildren<PlayerAnimation>();

        playerMovement = GetComponent<PlayerMovement>();

        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.StatSO = statSO;

        InputInitialize();
        AnimationInitialize();
    }

    private void AnimationInitialize()
    {
        playerMovement.OnMoveChange += playerAnimation.ChangeAnimation;
        playerAnimation.OnAttack += playerMovement.AttackMove;
    }

    private void InputInitialize()
    {
        inputSO.OnMouseMove += (mousePos) => playerMovement.MousePos = mousePos;
        inputSO.OnSpaceBtnChanged += (isPerformed) => playerMovement.DoMove = isPerformed;
        inputSO.OnMouseClick += () =>
        {
            if (playerMovement.CanMove == false) return;

            playerMovement.CanMove = false;

            if (playerMovement.DoMove)
            {
                playerAnimation.ChangeAnimation(PlayerState.Dash);
            }
            else
            {
                playerAnimation.ChangeAnimation(PlayerState.Attack);
            }
        };
    }

    private void Update()
    {
        
    }
}
