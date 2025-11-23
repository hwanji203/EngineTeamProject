using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkinSlotUI : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [Header("슬롯 설정")]
    [SerializeField] private int slotIndex = 0;
    [SerializeField] private SkinBodyPart requiredBodyPart = SkinBodyPart.Head;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button removeButton;
    
    [Header("빈 슬롯 설정")]
    [SerializeField] private Sprite emptySlotSprite;
    
    private SkinManager skinManager;
    private SkinSO previousSkin;
    
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
        SkinSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
        
        if (currentSkin != null)
        {
            // 스킨 해제 전에 인벤토리에 보여주기
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.ShowSkinIfOwned(currentSkin);
            }
            
            skinManager.UnequipSkin(slotIndex);
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        DraggableSkinItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableSkinItem>();
        if (draggedItem != null && draggedItem.SkinData != null)
        {
            HandleInventoryToDrop(draggedItem.SkinData);
            return;
        }
    }
    
    private void HandleInventoryToDrop(SkinSO skin)
    {
        if (skin.BodyPart != requiredBodyPart)
        {
            return;
        }
        
        // 이전 스킨이 있으면 인벤토리에 복귀
        SkinSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
        if (currentSkin != null && InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ShowSkinIfOwned(currentSkin);
        }
        
        skinManager.EquipSkin(slotIndex, skin);
    }
    
    private void OnSkinChanged(int changedSlot, SkinSO newSkin)
    {
        if (changedSlot == slotIndex)
        {
            UpdateUI();
        }
    }
    
    private void UpdateUI()
    {
        SkinSO currentSkin = skinManager.GetEquippedSkin(slotIndex);
    
        if (currentSkin != null)
        {
            iconImage.sprite = currentSkin.SkinIcon;
        }
        else
        {
            iconImage.sprite = emptySlotSprite;
        }
        
        previousSkin = currentSkin;
    }
    
    private void OnDestroy()
    {
        if (skinManager != null)
        {
            skinManager.OnSkinChanged -= OnSkinChanged;
        }
    }
}
