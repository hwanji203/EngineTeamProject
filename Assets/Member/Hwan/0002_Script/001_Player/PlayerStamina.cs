using UnityEngine;

public class PlayerStamina
{
    private PlayerStatSO statSO;

    public NotifyValue<float> CurrentStamina { get; private set; }

    public void Initialize()
    {
        CurrentStamina = new(0);
        CurrentStamina.Value = statSO.maxStamina;
    }

    public bool TryMove(PlayerMoveType type)
    {
        float useStamina = GetStamina(type);
        if (type == PlayerMoveType.Swim) useStamina *= Time.deltaTime;

        if (useStamina <= CurrentStamina.Value)
        {
            CurrentStamina.Value -= useStamina;
            return true;
        }
        return false;
    }

    private float GetStamina(PlayerMoveType type)
    {
        switch (type)
        {
            case PlayerMoveType.Dash:
                return statSO.dashStamina;
            case PlayerMoveType.Swim:
                return statSO.swimStamina;
            default:
                return 0;
        }
    }

    public void LostStamina(float damage)
    {
        if (CurrentStamina.Value <= damage)
        {
            CurrentStamina.Value = 0;
            return;
        }

        CurrentStamina.Value -= damage;
    }

    public void SetStatSO(PlayerStatSO statSO)
    {
        this.statSO = statSO;
    }
}
