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
    }
    
    private void OnDestroy()
    {
        if (skinManager != null)
        {
            skinManager.OnSkinChanged -= OnSkinChanged;
        }
    }
}
