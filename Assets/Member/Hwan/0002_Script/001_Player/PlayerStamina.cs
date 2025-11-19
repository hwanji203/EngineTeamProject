using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float noAirBenchmark = 0.4f;

    private PlayerStatSO statSO;
    public NotifyValue<float> CurrentStamina;
    public Action<float> OnNoAir;

    private Dictionary<PlayerMoveType, StaminaValue> staminaDictionary = new();
    private WaitForSeconds waitForSec;
    private Coroutine canMoveCoroutine;

    public void Initialize(PlayerStatSO statSO)
    {
        this.statSO = statSO;
        CurrentStamina = new(0);
        CurrentStamina.Value = statSO.MaxStamina;

        float noAirValue = statSO.MaxStamina * noAirBenchmark;
        CurrentStamina.OnValueChange += (value) =>
        {
            if (value < noAirValue) OnNoAir?.Invoke(1 - value / noAirValue);
        };

        waitForSec = new WaitForSeconds(statSO.FlipCool);

        foreach (StaminaValue stamina in statSO.staminaValues)
        {
            staminaDictionary.Add(stamina.MoveType, stamina);
        }
    }

    public bool TryMove(PlayerMoveType type)
    {
        //타입에 따라 스테미나를 사용해 코루틴을 실행하거나 실행하지 않기, 코루틴이 이미 시작되어 있으면 시작되어야 하는 애는 그냥 true 반환하기]\
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
