using UnityEngine;
using System;

public class EquipmentManager : MonoBehaviour, IEquipmentReader
{
    public static EquipmentManager Instance { get; private set; }
    
    private const int SLOT_COUNT = 3;
    private EquipmentSO[] equippedItems = new EquipmentSO[SLOT_COUNT];
    public event Action<int, EquipmentSO> OnEquipmentChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 장비 장착 (중복 체크)
    public void EquipItem(int slotIndex, EquipmentSO equipment)
    {
        if (!IsValidSlotIndex(slotIndex))
        {
            Debug.LogWarning($"잘못된 슬롯 인덱스: {slotIndex}");
            return;
        }
        
        // 같은 아이템을 같은 슬롯에 다시 장착하려면 무시
        if (equippedItems[slotIndex] == equipment)
        {
            return;
        }
        
        // 새로운 아이템이 이미 다른 슬롯에 있으면
        if (equipment != null)
        {
            int existingSlot = FindItemSlot(equipment.ItemID);
            
            // 다른 슬롯에 이미 있음
            if (existingSlot != -1 && existingSlot != slotIndex)
            {
                Debug.LogWarning($"'{equipment.ItemName}'은 이미 슬롯 {existingSlot}에 장착되어 있습니다!");
                return;
            }
        }
        
        // 장착 성공
        equippedItems[slotIndex] = equipment;
        Debug.Log($"장비 장착: 슬롯 {slotIndex}, 장비 {equipment?.ItemName ?? "해제"}");
        OnEquipmentChanged?.Invoke(slotIndex, equipment);
    }
    
    // 두 슬롯의 아이템을 교환
    public void SwapItems(int fromSlot, int toSlot)
    {
        if (!IsValidSlotIndex(fromSlot) || !IsValidSlotIndex(toSlot))
        {
            Debug.LogWarning("잘못된 슬롯 인덱스");
            return;
        }
        
        // 임시 저장
        EquipmentSO temp = equippedItems[fromSlot];
        
        // 교환
        equippedItems[fromSlot] = equippedItems[toSlot];
        equippedItems[toSlot] = temp;
        
        Debug.Log($"아이템 교환: 슬롯 {fromSlot} ↔ 슬롯 {toSlot}");
        
        // 두 슬롯 모두 업데이트 알림
        OnEquipmentChanged?.Invoke(fromSlot, equippedItems[fromSlot]);
        OnEquipmentChanged?.Invoke(toSlot, equippedItems[toSlot]);
    }
    
    // 슬롯에서 아이템 제거
    public void UnequipItem(int slotIndex)
    {
        EquipItem(slotIndex, null);
    }
    
    // 특정 아이템 ID가 어느 슬롯에 있는지 찾기
    private int FindItemSlot(int itemID)
    {
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (equippedItems[i] != null && equippedItems[i].ItemID == itemID)
            {
                return i;
            }
        }
        return -1;
    }
    
    public EquipmentSO GetEquippedItem(int slotIndex)
    {
        return IsValidSlotIndex(slotIndex) ? equippedItems[slotIndex] : null;
    }
    
    public EquipmentSO[] GetAllEquippedItems()
    {
        return (EquipmentSO[])equippedItems.Clone();
    }
    
    public int GetTotalAttackBonus()
    {
        int total = 0;
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (equippedItems[i] != null)
                total += equippedItems[i].AttackBonus;
        }
        return total;
    }
    
    public int GetTotalDefenseBonus()
    {
        int total = 0;
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (equippedItems[i] != null)
                total += equippedItems[i].DefenseBonus;
        }
        return total;
    }
    
    private bool IsValidSlotIndex(int index)
    {
        return index >= 0 && index < SLOT_COUNT;
    }
}
