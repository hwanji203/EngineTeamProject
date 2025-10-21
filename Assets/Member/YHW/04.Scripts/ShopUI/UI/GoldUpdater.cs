using System;
using UnityEngine;

public class GoldUpdater : MonoBehaviour
{
    TMPro.TextMeshProUGUI goldText;

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
