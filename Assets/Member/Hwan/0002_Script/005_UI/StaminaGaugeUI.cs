using System;
using System.CodeDom;
using UnityEngine;

public class StaminaGaugeUI : MonoBehaviour, IUI
{
    [SerializeField] private float maxSize;
    [SerializeField] private RectTransform gaugeTrn;
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType { get => UIType.GaugeUI; }

    private float maxValue;

    public void Initialize() { }

    private void SetGauge(float value)
    {
        Vector3 gaugeSize = gaugeTrn.transform.localScale;
        gaugeTrn.localScale = new Vector3(Mathf.Lerp(0, maxSize, value / maxValue), gaugeSize.y, gaugeSize.z);
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
        GameManager.Instance.Player.StaminaCompo.CurrentStamina.OnValueChange += SetGauge;
        maxValue = GameManager.Instance.Player.StatSO.MaxStamina;
        SetGauge(maxValue);
        Open();
    }
}
