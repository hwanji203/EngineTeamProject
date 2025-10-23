using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public NotifyValue<float> CurrentStamina { get; private set; } = new(0);

    private void Awake()
    {
        CurrentStamina.Value = StatSO.maxStamina;
    }

    public bool TryMove(PlayerMoveType type)
    {
        float useStamina = GetStamina(type);

        if (useStamina >= CurrentStamina.Value)
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

        }

        return 1;
    }

    public void RecoveryStamina(PlayerMoveType type)
    {
        float useStamina = GetStamina(type);

        CurrentStamina.Value = Mathf.Clamp(CurrentStamina.Value + useStamina, 0, StatSO.maxStamina);
    }
}
