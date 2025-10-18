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
        playerMovement.StatSO = statSO;

        playerAttack = GetComponent<PlayerAttack>();
        playerAttack.StatSO = statSO;

        InputInitialize();
        AnimationInitialize();
    }

    private void AnimationInitialize()
    {
        playerMovement.OnMoveChange += playerAnimation.ChangeAnimation;
    }

    private void InputInitialize()
    {
        inputSO.OnMouseMove += (mousePos) => playerMovement.MousePos = mousePos;
        inputSO.OnSpaceBtnChanged += (isPerformed) => playerMovement.DoMove = isPerformed;
        inputSO.OnMouseClick += () =>
        {
            bool canDash = playerMovement.DoMove && playerMovement.CanMove;
            
            playerAttack.Attack(canDash);

            if (canDash == true)
            {
                playerMovement.DashMove();
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
