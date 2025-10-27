using System;
using TMPro;
using UnityEngine;

public class GoldUpdater : MonoBehaviour
{
    TextMeshProUGUI goldText;

    private void Awake()
    {
        goldText = GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Start()
    {
        CurrencyManager.Instance.OnGoldChanged += UpdateGoldDisplay;
    }

    private void UpdateGoldDisplay(int currentGold)
    {
        goldText.SetText($"gold : {currentGold}");
    }
}
