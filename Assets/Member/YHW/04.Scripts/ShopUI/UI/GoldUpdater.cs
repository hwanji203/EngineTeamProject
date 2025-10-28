using System;
using TMPro;
using UnityEngine;

public class GoldUpdater : MonoBehaviour
{
    TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        CurrencyManager.Instance.OnGoldChanged += UpdateGoldDisplay;
        goldText.SetText($"gold : {CurrencyManager.Instance.Gold}");
    }

    private void UpdateGoldDisplay(int currentGold)
    {
        goldText.SetText($"gold : {currentGold}");
    }
}
