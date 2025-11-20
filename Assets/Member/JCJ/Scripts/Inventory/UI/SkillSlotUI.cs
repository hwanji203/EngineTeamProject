using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinSlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("슬롯 설정")]
    [SerializeField] private int slotIndex;
    [SerializeField] private SkinBodyPart requiredBodyPart;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button removeButton;
    
    [Header("빈 슬롯 설정")]
    [SerializeField] private Sprite emptySlotSprite;
    
    private SkinManager skinManager;
    
    private void Start()
    {
        skinManager = SkinManager.Instance;
        
        if (skinManager != null)
        {
            skinManager.OnSkinChanged += OnSkinChanged;
        }
        
        if (removeButton != null)
        {
            removeButton.onClick.AddListener(RemoveSkin);
        }
        
        UpdateUI();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RemoveSkin();
        }
    }
    
    private void RemoveSkin()
    {
        SkinItemSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
        
        if (currentSkin != null)
        {
            skinManager.UnequipSkin(slotIndex);
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        SkinSlotUI sourceSlot = eventData.pointerDrag?.GetComponent<SkinSlotUI>();
        if (sourceSlot != null)
        {
            HandleSlotToSlotDrop(sourceSlot);
            return;
        }
        DraggableSkinItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableSkinItem>();
        if (draggedItem != null && draggedItem.SkinData != null)
        {
            HandleInventoryToDrop(draggedItem.SkinData);
            return;
        }
    }
    
    private void HandleSlotToSlotDrop(SkinSlotUI sourceSlot)
    {
        int fromSlot = sourceSlot.slotIndex;
        int toSlot = slotIndex;
        
        if (fromSlot == toSlot)
        {
            return;
        }
        
        SkinItemSO fromSkin = skinManager.GetEquippedSkin(fromSlot);
        SkinItemSO toSkin = skinManager.GetEquippedSkin(toSlot);
        
        if (toSkin == null)
        {
            skinManager.EquipSkin(toSlot, fromSkin);
            skinManager.EquipSkin(fromSlot, null);
            return;
        }
        
        skinManager.SwapSkins(fromSlot, toSlot);
    }
    
    private void HandleInventoryToDrop(SkinItemSO skin)
    {
        if (skin.BodyPart != requiredBodyPart)
        {
            return;
        }
        
        SkinItemSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
        
        if (currentSkin == null)
        {
            skinManager.EquipSkin(slotIndex, skin);
            return;
        }
        
        skinManager.EquipSkin(slotIndex, skin);
    }
    
    private void OnSkinChanged(int changedSlot, SkinItemSO newSkin)
    {
        if (changedSlot == slotIndex)
        {
            UpdateUI();
        }
    }
    
    private void UpdateUI()
    {
        SkinItemSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
    
        if (currentSkin != null)
        {
            iconImage.sprite = currentSkin.ItemIcon;
        }
        else
        {
            iconImage.sprite = emptySlotSprite;
        }
    }
    
    private void OnDestroy()
    {
        if (skinManager != null)
        {
            skinManager.OnSkinChanged -= OnSkinChanged;
        }
    }
}
 