using UnityEngine;
using System;

public class CurrencyManager : MonoSingleton<CurrencyManager>
{

    [SerializeField] private int gold = 5000;
    public int Gold => gold;

    public event Action<int> OnGoldChanged;

    

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
