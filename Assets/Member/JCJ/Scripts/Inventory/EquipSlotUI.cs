using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("슬롯 설정")]
    [SerializeField] private int slotIndex;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button removeButton;
    
    [Header("빈 슬롯 설정")]
    [SerializeField] private Sprite emptySlotSprite;
    
    private EquipmentManager equipmentManager;
    
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
        
        if (removeButton != null)
        {
            removeButton.onClick.AddListener(RemoveEquipment);
        }
        
        UpdateUI();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RemoveEquipment();
        }
    }
    
    public void RemoveEquipment()
    {
        EquipmentSO currentItem = equipmentManager.GetEquippedItem(slotIndex);
        
        if (currentItem != null)
        {
            Debug.Log($"Slot {slotIndex} in '{currentItem.ItemName}'is UnEquipped");
            equipmentManager.UnequipItem(slotIndex);
        }
        else
        {
            Debug.Log($"Slot {slotIndex} is null");
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"Slot {slotIndex} In Drop");
        
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
        
        Debug.LogWarning("DraggableItem is null");
    }
    
    private void HandleSlotToSlotDrop(EquipSlotUI sourceSlot)
    {
        int fromSlot = sourceSlot.slotIndex;
        int toSlot = slotIndex;
        
        if (fromSlot == toSlot)
        {
            Debug.Log("Same Slot");
            return;
        }
        
        EquipmentSO fromItem = equipmentManager.GetEquippedItem(fromSlot);
        EquipmentSO toItem = equipmentManager.GetEquippedItem(toSlot);
        
        if (toItem == null)
        {
            equipmentManager.EquipItem(toSlot, fromItem);
            equipmentManager.EquipItem(fromSlot, null);
            Debug.Log($"Item Move: Slot {fromSlot} → Slot {toSlot}");
            return;
        }
        
        equipmentManager.SwapItems(fromSlot, toSlot);
        Debug.Log($"Item Trade: Slot {fromSlot} ↔ Slot {toSlot}");
    }
    
    private void HandleInventoryToDrop(EquipmentSO equipment)
    {
        EquipmentSO currentItem = equipmentManager.GetEquippedItem(slotIndex);
        
        if (currentItem == null)
        {
            equipmentManager.EquipItem(slotIndex, equipment);
            Debug.Log($"Item Equiped : Slot {slotIndex} In {equipment.ItemName}");
            return;
        }
        
        equipmentManager.EquipItem(slotIndex, equipment);
        Debug.Log($"Item Trade : Slot {slotIndex} In {equipment.ItemName}");
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
        }
        else
        {
            iconImage.sprite = emptySlotSprite;
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
