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
        if (CurrencyManager.Instance.SpendGold(itemData.Cost))
        {
            itemUI.SetPurchased();
            Debug.Log("구매 완료");
            SkinItemSO skinItem = CreateSkinItemFromSkin(itemData);
            InventoryManager.Instance?.AddSkin(skinItem);

            AfterBuying();
        }
        else
        {
            itemUI.PlayInsufficientFundsFeedback();
            Debug.Log("골드 부족");
        }
    }

    private SkinItemSO CreateSkinItemFromSkin(SkinSO skinData)
    {
        SkinItemSO skinItem = ScriptableObject.CreateInstance<SkinItemSO>();
        skinItem.InitFromSkin(skinData);
        return skinItem;
    }

    private void AfterBuying()
    {
        
    }
}
