using System;
using System.CodeDom;
using UnityEngine;

public class StaminaGaugeUI : MonoBehaviour, IUI
{
    [SerializeField] private float maxSize;
    [SerializeField] private RectTransform gaugeTrn;

    public UIType UIType { get => UIType.GaugeUI; }

    private float maxValue;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        GameManager.Instance.Player.Stamina.CurrentStamina.OnValueChange += SetGauge;
        maxValue = GameManager.Instance.Player.StatSO.maxStamina;
        SetGauge(maxValue);
    }

    private void SetGauge(float value)
    {
        Vector3 gaugeSize = gaugeTrn.transform.localScale;
        gaugeTrn.localScale = new Vector3(Mathf.Lerp(0, maxSize, value / maxValue), gaugeSize.y, gaugeSize.z);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
