using UnityEngine;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform[] itemSlots; 
    [SerializeField] private SkillSO[] allItemDatabase;

    private List<SkillSO> activeItems = new List<SkillSO>();

    private void Start()
    {
        InitStore();
    }

    private void InitStore()
    {
        activeItems.Clear();
        var randomItems = GetRandomItems(6);

        for (int i = 0; i < randomItems.Count; i++)
        {
            var itemData = randomItems[i];
            var slot = itemSlots[i];

            var itemObj = Instantiate(storeItemPrefab, slot);
            var itemUI = itemObj.GetComponent<StoreItemUI>();
            itemUI.Setup(itemData, this);

            activeItems.Add(itemData);
        }
    }

    private List<SkillSO> GetRandomItems(int count)
    {
        List<SkillSO> tempList = new List<SkillSO>(allItemDatabase);
        List<SkillSO> result = new List<SkillSO>();

        for (int i = 0; i < count && tempList.Count > 0; i++)
        {
            int index = Random.Range(0, tempList.Count);
            result.Add(tempList[index]);
            tempList.RemoveAt(index);
        }
        return result;
    }

    public void TryPurchaseItem(SkillSO itemData, StoreItemUI itemUI)
    {
        if (CurrencyManager.Instance.SpendGold(itemData.Cost))
        {
            itemUI.SetPurchased();
            Debug.Log($"���ż���");
            AfterBuying();
        }
        else
        {
            itemUI.PlayInsufficientFundsFeedback();
            Debug.Log($"���� ����");
        }
    }

    private void AfterBuying()
    {
        
    }
}
