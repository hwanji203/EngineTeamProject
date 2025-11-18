using UnityEngine;
using System;

public class CurrencyManager : MonoSingleton<CurrencyManager>
{

    private int gold = 500;
    public int Gold => gold;

    public event Action<int> OnGoldChanged;

    private void Start()
    {
        PlayerPrefs.SetInt("Gold", gold);
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }
}
