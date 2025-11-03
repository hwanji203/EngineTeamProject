using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private PlayerStatSO statSO;

    public NotifyValue<float> CurrentStamina { get; private set; } = new(0);

    private void Start()
    {
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

    public void RecoveryStamina(PlayerMoveType type)
    {
        float useStamina = GetStamina(type);

        CurrentStamina.Value = Mathf.Clamp(CurrentStamina.Value + useStamina, 0, statSO.maxStamina);
    }

    public void SetStatSO(PlayerStatSO statSO)
    {
        this.statSO = statSO;
    }
}
