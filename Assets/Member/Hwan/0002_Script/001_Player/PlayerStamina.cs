using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float noStaminaPercent = 0.4f;

    private PlayerStatSO statSO;
    public NotifyValue<float> CurrentStamina;
    public event Action<float> OnNoAir;

    private Dictionary<PlayerMoveType, StaminaValue> staminaDictionary = new();
    private WaitForSeconds waitForSec;
    private Coroutine canMoveCoroutine;

    public void Initialize(PlayerStatSO statSO)
    {
        this.statSO = statSO;
        CurrentStamina = new(0);
        CurrentStamina.Value = statSO.MaxStamina;

        CurrentStamina.OnValueChange += NoStaminaCheck;

        waitForSec = new WaitForSeconds(statSO.FlipCool);

        foreach (StaminaValue stamina in statSO.staminaValues)
        {
            staminaDictionary.Add(stamina.MoveType, stamina);
        }
    }

    public void NoStaminaCheck(float value)
    {
        float noStaminaValue = statSO.MaxStamina * noStaminaPercent;

        float currentPercent = 1 - value / noStaminaValue;
        if (currentPercent < 0.01f) currentPercent = 0;
        else if (currentPercent > 0.99f) currentPercent = 1;

        OnNoAir?.Invoke(currentPercent);
    }

    public bool TryMove(PlayerMoveType type)
    {
        if (CurrentStamina.Value == 0) return false;

        StaminaValue stamina = staminaDictionary[type];

        float useStamina = stamina.UseStamina;

        if (stamina.IsOneShot == false)
        {
            if (canMoveCoroutine != null) return true;

            useStamina *= stamina.UseTime;
            if (useStamina > CurrentStamina.Value)
            {
                canMoveCoroutine = StartCoroutine(SpentStaminaCoroutine(stamina.UseTime * (CurrentStamina.Value / useStamina)));
                CurrentStamina.Value = 0;
            }
            else
            {
                CurrentStamina.Value -= useStamina;
                canMoveCoroutine = StartCoroutine(SpentStaminaCoroutine(stamina.UseTime));
            }
        }
        else
        {
            if (useStamina > CurrentStamina.Value) return false;

            CurrentStamina.Value -= useStamina;
        }

        return true;
    }

    public void SubOnNoAir(Action<float> method)
    {
        OnNoAir += method;
    }

    public void LostStamina(float damage)
    {
        CurrentStamina.Value = Mathf.Clamp(CurrentStamina.Value - damage, 0, statSO.MaxStamina);
    }

    public void RecoveryStamina(float value)
    {
        CurrentStamina.Value = Mathf.Clamp(CurrentStamina.Value + value, 0, statSO.MaxStamina);
    }

    public IEnumerator SpentStaminaCoroutine(float waitTime = 0)
    {
        if (waitTime == 0)
        {
            yield return waitForSec;
        }
        else
        {
            yield return new WaitForSeconds(waitTime);
        }

        canMoveCoroutine = null;
    }
}
