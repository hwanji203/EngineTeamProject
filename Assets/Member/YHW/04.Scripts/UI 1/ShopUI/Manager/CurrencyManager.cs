using UnityEngine;
using System;

public class CurrencyManager : MonoSingleton<CurrencyManager>
{

    private int gold;
    public int Gold => gold;

    public event Action<int> OnGoldChanged;

    private bool isLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == this && !isLoaded)
        {
            LoadGold();
            isLoaded = true;
        }
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount) return false;

        gold -= amount;
        OnGoldChanged?.Invoke(gold);
        SaveGold();
        return true;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
        SaveGold();
    }
    private void SaveGold()
    {
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.Save();
    }

    private void LoadGold()
    {
        gold = PlayerPrefs.GetInt("Gold",500);
        OnGoldChanged?.Invoke(gold);
    }
}
