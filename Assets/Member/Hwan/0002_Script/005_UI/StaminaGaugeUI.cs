using System;
using System.CodeDom;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGaugeUI : MonoBehaviour, IUI
{
    private float maxAmount;
    [SerializeField] private Image gaugeImage;
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType { get => UIType.GaugeUI; }

    public void Initialize() { }

    private void SetGauge(float value)
    {
        float gauge = value / maxAmount;
        gaugeImage.fillAmount = gauge;
    }

    public void Open()
    {
        UIObject.SetActive(true);
    }

    public void Close()
    {
        UIObject.SetActive(false);
    }

    public void LateInitialize()
    {
        maxAmount = GameManager.Instance.Player.StatSO.MaxStamina;
        GameManager.Instance.Player.StaminaCompo.CurrentStamina.OnValueChange += SetGauge;
        SetGauge(maxAmount);
        Open();
    }
}
