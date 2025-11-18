using UnityEngine;
using System.Collections.Generic;

public class StoreManager : MonoBehaviour
{
    [Header("Store Settings")]
    [SerializeField] private GameObject storeItemPrefab;
    [SerializeField] private Transform[] itemSlots; 
    [SerializeField] private SkillSO[] allItemDatabase;

    [Header("draggable items Settings")]
    [SerializeField] private GameObject draggableItemPrefab;
    [SerializeField] private Transform draggableItemParent;

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
            Debug.Log("구매 완료");

            GameObject obj = Instantiate(draggableItemPrefab, draggableItemParent);
            var draggable = obj.GetComponent<DraggableItem>();
            if (draggable != null)
            {
                draggable.Init(itemData.EquipmentData);
            }

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
