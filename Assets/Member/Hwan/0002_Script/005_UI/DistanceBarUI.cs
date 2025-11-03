using System;
using UnityEngine;
using UnityEngine.UI;

public class DistanceBarUI : MonoBehaviour, IUI
{
    [SerializeField] private Slider distanceBar;

    public UIType UIType => UIType.DistanceBarUI;

    private StageInfoSO stageSO;
    private float maxValue;
    private float minValue;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        GameManager.Instance.Player.PlayerMovementCompo.PlayerYValue.OnValueChange += ChangeSlider;
        stageSO = GameManager.Instance.StageSO;
        minValue = stageSO.StartY;
        maxValue = stageSO.EndY;
        distanceBar.value = minValue;
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }

    private void ChangeSlider(float value)
    {
        distanceBar.value = Mathf.Lerp(0, 1, (value - minValue) / (maxValue - minValue));
    }
}