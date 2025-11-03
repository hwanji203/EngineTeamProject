using System;
using Member.Hwan._0002_Script._002_SO;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public PlayerStatSo StatSO { get; set; }

    public NotifyValue<float> CurrentStamina { get; private set; } = new(0);

    private Coroutine recoveryCoolCor;

    private void Start()
    {
        CurrentStamina.Value = StatSO.maxStamina;
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
                return StatSO.dashStamina;
            case PlayerMoveType.Swim:
                return StatSO.swimStamina;
            default:
                return 0;
        }
    }

    public void RecoveryStamina(PlayerMoveType type)
    {
        float useStamina = GetStamina(type);

        CurrentStamina.Value = Mathf.Clamp(CurrentStamina.Value + useStamina, 0, StatSO.maxStamina);
    }
}
