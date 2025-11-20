using UnityEngine;
using System;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }
    
    private const int slotCount = 3;
    private SkinItemSO[] equippedSkins = new SkinItemSO[slotCount];
    
    public event Action<int, SkinItemSO> OnSkinChanged;
    
    [Header("Character Sprite Renderers")]
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;
    
    [Header("Default Sprites")]
    [SerializeField] private Sprite defaultHeadSprite;
    [SerializeField] private Sprite defaultBodySprite;
    [SerializeField] private Sprite defaultWeaponSprite;
    
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
    
    private void Start()
    {
        ResetToDefaultSkins();
    }
    
    public void EquipSkin(int slotIndex, SkinItemSO skin)
    {
        if (!IsValidSlotIndex(slotIndex))
        {
            return;
        }
        
        if (equippedSkins[slotIndex] == skin)
        {
            return;
        }
        
        if (equippedSkins[slotIndex] != null)
        {
            InventoryManager.Instance?.ShowSkin(equippedSkins[slotIndex]);
        }
        
        if (skin != null)
        {
            int existingSlot = FindSkinSlot(skin.ItemID);
            
            if (existingSlot != -1 && existingSlot != slotIndex)
            {
                return;
            }
        }
        
        equippedSkins[slotIndex] = skin;
        
        ApplySkinToCharacter(slotIndex, skin);
        
        if (skin != null)
        {
            InventoryManager.Instance?.HideSkin(skin);
        }
        
        OnSkinChanged?.Invoke(slotIndex, skin);
    }
    
    private void ApplySkinToCharacter(int slotIndex, SkinItemSO skin)
    {
        Sprite spriteToApply = null;
        
        if (skin != null)
        {
            spriteToApply = skin.ItemSprite;
        }
        else
        {
            spriteToApply = GetDefaultSprite(slotIndex);
        }
        
        switch (slotIndex)
        {
            case 0: // Head
                if (headRenderer != null)
                    headRenderer.sprite = spriteToApply;
                break;
            case 1: // Body
                if (bodyRenderer != null)
                    bodyRenderer.sprite = spriteToApply;
                break;
            case 2: // Weapon
                if (weaponRenderer != null)
                    weaponRenderer.sprite = spriteToApply;
                break;
        }
    }
    
    public void SwapSkins(int fromSlot, int toSlot)
    {
        if (!IsValidSlotIndex(fromSlot) || !IsValidSlotIndex(toSlot))
        {
            return;
        }
        
        SkinItemSO temp = equippedSkins[fromSlot];
        equippedSkins[fromSlot] = equippedSkins[toSlot];
        equippedSkins[toSlot] = temp;
        
        ApplySkinToCharacter(fromSlot, equippedSkins[fromSlot]);
        ApplySkinToCharacter(toSlot, equippedSkins[toSlot]);
        
        Debug.Log($"스킨 교환: 슬롯 {fromSlot} ↔ 슬롯 {toSlot}");
        
        OnSkinChanged?.Invoke(fromSlot, equippedSkins[fromSlot]);
        OnSkinChanged?.Invoke(toSlot, equippedSkins[toSlot]);
    }
    
    public void UnequipSkin(int slotIndex)
    {
        EquipSkin(slotIndex, null);
    }
    
    private int FindSkinSlot(int skinID)
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (equippedSkins[i] != null && equippedSkins[i].ItemID == skinID)
            {
                return i;
            }
        }
        return -1;
    }
    
    private void ResetToDefaultSkins()
    {
        if (headRenderer != null)
            headRenderer.sprite = defaultHeadSprite;
        if (bodyRenderer != null)
            bodyRenderer.sprite = defaultBodySprite;
        if (weaponRenderer != null)
            weaponRenderer.sprite = defaultWeaponSprite;
    }
    
    private Sprite GetDefaultSprite(int slotIndex)
    {
        return slotIndex switch
        {
            0 => defaultHeadSprite,
            1 => defaultBodySprite,
            2 => defaultWeaponSprite,
            _ => null
        };
    }
    
    public SkinItemSO GetEquippedSkin(int slotIndex)
    {
        return IsValidSlotIndex(slotIndex) ? equippedSkins[slotIndex] : null;
    }
    
    public SkinItemSO[] GetAllEquippedSkins()
    {
        return (SkinItemSO[])equippedSkins.Clone();
    }
    
    private bool IsValidSlotIndex(int index)
    {
        return index >= 0 && index < slotCount;
    }
}
