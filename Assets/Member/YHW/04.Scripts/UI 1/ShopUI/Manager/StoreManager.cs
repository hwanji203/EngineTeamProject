using UnityEngine;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform[] itemSlots; 
    [SerializeField] private StoreItemData[] allItemDatabase;

    private List<StoreItemData> activeItems = new List<StoreItemData>();

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

    private List<StoreItemData> GetRandomItems(int count)
    {
        List<StoreItemData> tempList = new List<StoreItemData>(allItemDatabase);
        List<StoreItemData> result = new List<StoreItemData>();

        for (int i = 0; i < count && tempList.Count > 0; i++)
        {
            int index = Random.Range(0, tempList.Count);
            result.Add(tempList[index]);
            tempList.RemoveAt(index);
        }
        return result;
    }

    public void TryPurchaseItem(StoreItemData itemData, StoreItemUI itemUI)
    {
        if (CurrencyManager.Instance.SpendGold(itemData.price))
        {
            itemUI.SetPurchased();
            Debug.Log($"구매성공");
            AfterBuying();
        }
        else
        {
            itemUI.PlayInsufficientFundsFeedback();
            Debug.Log($"구매 실패");
        }
    }

    private void AfterBuying()
    {
        
    }
}
