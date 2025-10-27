using System;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public PlayerStatSO StatSO { get; set; }

    public NotifyValue<float> CurrentStamina { get; private set; } = new(0);

    private Coroutine recoveryCoolCor;

    private void Start()
    {
        CurrentStamina.Value = StatSO.maxStamina;
    }

    private void Update()
    {
        Debug.Log(CurrentStamina.Value);
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
