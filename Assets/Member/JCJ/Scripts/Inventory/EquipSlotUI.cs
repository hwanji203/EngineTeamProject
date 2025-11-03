using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipSlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("슬롯 설정")]
    [SerializeField] private int slotIndex;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button removeButton;
    
    [Header("빈 슬롯 설정")]
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private Color emptyColor = new Color(1f, 1f, 1f, 0.3f);
    
    private EquipmentManager equipmentManager;
    private Color originalBackgroundColor;
    
    private void Start()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        equipmentManager = EquipmentManager.Instance;
        
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged += OnEquipmentChanged;
        }
        
        originalBackgroundColor = backgroundImage.color;
        
        // 버튼 설정
        if (removeButton != null)
        {
            removeButton.onClick.AddListener(RemoveEquipment);
        }
        
        UpdateUI();
    }
    
    /// 우클릭 감지
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RemoveEquipment();
        }
    }
    
    /// 장착해제 함수
    public void RemoveEquipment()
    {
        EquipmentSO currentItem = equipmentManager.GetEquippedItem(slotIndex);
        
        if (currentItem != null)
        {
            Debug.Log($"슬롯 {slotIndex}의 '{currentItem.ItemName}' 제거됨");
            equipmentManager.UnequipItem(slotIndex);
        }
        else
        {
            Debug.Log($"슬롯 {slotIndex}에 장착된 아이템이 없습니다.");
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"슬롯 {slotIndex}에 드롭");
        
        EquipSlotUI sourceSlot = eventData.pointerDrag?.GetComponent<EquipSlotUI>();
        if (sourceSlot != null)
        {
            HandleSlotToSlotDrop(sourceSlot);
            return;
        }
        
        DraggableItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (draggedItem != null && draggedItem.EquipmentData != null)
        {
            HandleInventoryToDrop(draggedItem.EquipmentData);
            return;
        }
        
        Debug.LogWarning("드래그된 아이템이 없습니다.");
    }
    
    private void HandleSlotToSlotDrop(EquipSlotUI sourceSlot)
    {
        int fromSlot = sourceSlot.slotIndex;
        int toSlot = slotIndex;
        
        if (fromSlot == toSlot)
        {
            Debug.Log("같은 슬롯입니다.");
            return;
        }
        
        EquipmentSO fromItem = equipmentManager.GetEquippedItem(fromSlot);
        EquipmentSO toItem = equipmentManager.GetEquippedItem(toSlot);
        
        if (toItem == null)
        {
            equipmentManager.EquipItem(toSlot, fromItem);
            equipmentManager.EquipItem(fromSlot, null);
            Debug.Log($"아이템 이동: 슬롯 {fromSlot} → 슬롯 {toSlot}");
            return;
        }
        
        equipmentManager.SwapItems(fromSlot, toSlot);
        Debug.Log($"아이템 교환: 슬롯 {fromSlot} ↔ 슬롯 {toSlot}");
    }
    
    private void HandleInventoryToDrop(EquipmentSO equipment)
    {
        EquipmentSO currentItem = equipmentManager.GetEquippedItem(slotIndex);
        
        if (currentItem == null)
        {
            equipmentManager.EquipItem(slotIndex, equipment);
            Debug.Log($"아이템 장착: 슬롯 {slotIndex}에 {equipment.ItemName}");
            return;
        }
        
        equipmentManager.EquipItem(slotIndex, equipment);
        Debug.Log($"아이템 교체: 슬롯 {slotIndex}에 {equipment.ItemName}");
    }

    private void OnEquipmentChanged(int changedSlot, EquipmentSO newEquipment)
    {
        if (changedSlot == slotIndex)
        {
            UpdateUI();
        }
    }
    
    private void UpdateUI()
    {
        EquipmentSO currentEquipment = equipmentManager.GetEquippedItem(slotIndex);
        
        if (currentEquipment != null)
        {
            iconImage.sprite = currentEquipment.ItemIcon;
            iconImage.color = Color.white;
        }
        else
        {
            iconImage.sprite = emptySlotSprite;
            iconImage.color = emptyColor;
        }
    }
    
    private void OnDestroy()
    {
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }
}
