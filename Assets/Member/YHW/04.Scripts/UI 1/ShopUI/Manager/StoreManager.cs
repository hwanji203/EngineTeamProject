using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform[] itemSlots; 
    [SerializeField] private SkinSO[] allItemDatabase;

    private List<SkinSO> activeItems = new List<SkinSO>();

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
            
            if (InventoryManager.Instance != null && InventoryManager.Instance.HasSkin(itemData))
            {
                itemUI.SetPurchased();
            }
        }
    }

    private List<SkinSO> GetRandomItems(int count)
    {
        List<SkinSO> tempList = new List<SkinSO>(allItemDatabase);
        List<SkinSO> result = new List<SkinSO>();

        for (int i = 0; i < count && tempList.Count > 0; i++)
        {
            int index = Random.Range(0, tempList.Count);
            result.Add(tempList[index]);
            tempList.RemoveAt(index);
        }
        return result;
    }

    public void TryPurchaseItem(SkinSO itemData, StoreItemUI itemUI)
    {
        if (InventoryManager.Instance != null && InventoryManager.Instance.HasSkin(itemData))
        {
            Debug.LogWarning($"이미 보유한 스킨입니다:{itemData.SkinName}");
            itemUI.SetPurchased();
            return;
        }
        if (CurrencyManager.Instance.SpendGold(itemData.Cost))
        {
            itemUI.SetPurchased();
            Debug.Log("구매 완료");
            InventoryManager.Instance?.AddSkin(itemData);

            AfterBuying();
        }
        else
        {
            itemUI.PlayInsufficientFundsFeedback();
            Debug.Log("골드 부족");
        }
    }

    private void AfterBuying()
    {
        
    }
}
