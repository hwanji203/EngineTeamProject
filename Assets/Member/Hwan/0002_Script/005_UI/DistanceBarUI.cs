using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBarUI : MonoBehaviour, IUI
{
    [SerializeField] private Slider distanceBar;

    public UIType UIType => UIType.DistanceBarUI;

    [field: SerializeField] public GameObject UIObject { get; private set; }

    private StageInfoSO stageSO;
    private float maxValue;
    private float minValue;

    public void Initialize()
    {
        GameManager.Instance.Player.PlayerMovementCompo.PlayerYValue.OnValueChange += ChangeSlider;
        stageSO = GameManager.Instance.StageSO;
        minValue = stageSO.StartY;
        maxValue = stageSO.EndY;
        distanceBar.value = minValue;
        Open();
    }

    private void ChangeSlider(float value)
    {
        distanceBar.value = Mathf.Lerp(0, 1, (value - minValue) / (maxValue - minValue));
    }

    public void Open()
    {
        UIObject.SetActive(true);
    }

    public void Close()
    {
        UIObject.SetActive(false);
    }
}