using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveController
{
    private PlayerAttack playerAttackCompo;
    private PlayerStamina playerStamina;
    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;

    private (bool doing, PlayerAttackType attackType) currentAttackState;
    private bool doMove;

    public void Initialize(PlayerAttack attack, PlayerStamina stamina, PlayerAnimation animation, PlayerMovement movement)
    {
        playerAttackCompo = attack;
        playerStamina = stamina;
        playerAnimation = animation;
        playerMovement = movement;
    }

    public void SetDoMove(bool doMove)
    {
        this.doMove = doMove;
    }
    public void SetState(bool performed, PlayerAttackType attackType)
    {
        currentAttackState = (performed, attackType);
    }

    public void UpdateState()
    {
        if (playerMovement.CanMove == false) return;

        if (currentAttackState.doing == true && playerAnimation.CanAttack() == true && playerMovement.CurrentState != PlayerState.ZeroStamina)
        {
            if (TryAttack(currentAttackState.attackType) == true) return;
        }

        playerMovement.Move(PlayerMovementType.Rotate);

        if (doMove == false || playerStamina.TryMove(PlayerMoveType.Swim) == false)
        {
            playerAnimation.ChangeAnimation(PlayerState.Idle);
            playerMovement.ChangeState(PlayerState.Idle);

            return;
        }

        playerMovement.ChangeState(PlayerState.Move);
        playerAnimation.ChangeAnimation(PlayerState.Move);
        playerMovement.Move(PlayerMovementType.Swim);
    }

    private bool TryAttack(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                if (playerAttackCompo.DashCoolCoroutine != null) return false;
                if (playerStamina.TryMove(PlayerMoveType.Dash) == false) return false;
                playerAnimation.ChangeAnimation(PlayerState.Dash);
                break;
            case PlayerAttackType.Flip:
                if (playerAttackCompo.FlipCoolCoroutine != null) return false;
                playerAnimation.ChangeAnimation(PlayerState.Flip);
                break;
        }

        playerMovement.StartAttack(type);
        return true;
    }
}
