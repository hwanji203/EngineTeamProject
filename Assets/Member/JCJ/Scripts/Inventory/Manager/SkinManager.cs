using UnityEngine;
using System;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }
    
    private const int slotCount = 1;
    private SkinSO[] equippedSkins = new SkinSO[slotCount];
    
    public event Action<int, SkinSO> OnSkinChanged;
    
    [Header("Character Sprite Renderer")]
    [SerializeField] private SpriteRenderer headRenderer;
    
    [Header("Auto Find Settings")]
    [SerializeField] private bool autoFindRenderer = true;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string headObjectName = "Head";
    
    [Header("Default Sprite")]
    [SerializeField] private Sprite defaultHeadSprite;
    
    [Header("All Skins Database")]
    [SerializeField] private SkinSO[] allSkins;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        if (autoFindRenderer && headRenderer == null)
        {
            FindHeadRenderer();
        }
        
        if (headRenderer == null)
        {
            Debug.LogError("[SkinManager] Cannot find Head Renderer");
            return;
        }
        
        LoadEquippedSkins();
        ApplyAllSkins();
    }
    
    private void FindHeadRenderer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
    
        if (players.Length == 0)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                players = new GameObject[] { player };
            }
            else
            {
                return;
            }
        }
    
        GameObject targetPlayer = players[0];
    
        Transform headTransform = FindChildByName(targetPlayer.transform, headObjectName);
    
        if (headTransform == null)
        {
            return;
        }
    
        headRenderer = headTransform.GetComponent<SpriteRenderer>();
    }
    
    private Transform FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }
        }
        
        foreach (Transform child in parent)
        {
            Transform found = FindChildByName(child, childName);
            if (found != null)
            {
                return found;
            }
        }
        
        return null;
    }
    
    public void RefreshHeadRenderer()
    {
        FindHeadRenderer();
        
        if (headRenderer != null)
        {
            ApplyAllSkins();
        }
    }
    
    public void EquipSkin(int slotIndex, SkinSO skin)
    {
        if (!IsValidSlotIndex(slotIndex))
        {
            Debug.LogWarning($"[SkinManager] Invalid slot index: {slotIndex}");
            return;
        }
        
        if (equippedSkins[slotIndex] == skin)
        {
            Debug.Log($"[SkinManager] Skin already equipped: {skin?.SkinName}");
            return;
        }
        
        if (equippedSkins[slotIndex] != null)
        {
            InventoryManager.Instance?.ShowSkin(equippedSkins[slotIndex]);
        }
        
        if (skin != null)
        {
            int existingSlot = FindSkinSlot(skin.SkinID);
            
            if (existingSlot != -1 && existingSlot != slotIndex)
            {
                Debug.LogWarning($"[SkinManager] Skin already equipped in another slot.");
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
        
        SaveEquippedSkins();
        
        Debug.Log($"[SkinManager] Skin equipped: {skin?.SkinName ?? "None"}");
    }
    
    private void ApplySkinToCharacter(int slotIndex, SkinSO skin)
    {
        if (headRenderer == null)
        {
            Debug.LogError("[SkinManager] Cannot apply skin - Head Renderer is null!");
            return;
        }
        
        Sprite spriteToApply = null;
        
        if (skin != null)
        {
            spriteToApply = skin.SkinSprite;
            Debug.Log($"[SkinManager] Applying skin sprite: {skin.SkinName}");
        }
        else
        {
            spriteToApply = defaultHeadSprite;
            Debug.Log("[SkinManager] Applying default sprite");
        }
        
        if (spriteToApply != null)
        {
            headRenderer.sprite = spriteToApply;
        }
        else
        {
            Debug.LogWarning("[SkinManager] No sprite to apply!");
        }
    }
    
    private void ApplyAllSkins()
    {
        Debug.Log("[SkinManager] ApplyAllSkins called");
        
        for (int i = 0; i < slotCount; i++)
        {
            ApplySkinToCharacter(i, equippedSkins[i]);
        }
    }
    
    public void UnequipSkin(int slotIndex)
    {
        EquipSkin(slotIndex, null);
    }
    
    private int FindSkinSlot(int skinID)
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (equippedSkins[i] != null && equippedSkins[i].SkinID == skinID)
            {
                return i;
            }
        }
        return -1;
    }
    
    public SkinSO GetEquippedSkin(int slotIndex)
    {
        return IsValidSlotIndex(slotIndex) ? equippedSkins[slotIndex] : null;
    }
    
    public SkinSO[] GetAllEquippedSkins()
    {
        return (SkinSO[])equippedSkins.Clone();
    }
    
    private bool IsValidSlotIndex(int index)
    {
        return index >= 0 && index < slotCount;
    }
    
    private void SaveEquippedSkins()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (equippedSkins[i] != null)
            {
                PlayerPrefs.SetInt($"EquippedSkin_{i}", equippedSkins[i].SkinID);
            }
            else
            {
                PlayerPrefs.DeleteKey($"EquippedSkin_{i}");
            }
        }
        PlayerPrefs.Save();
    }
    
    private void LoadEquippedSkins()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (PlayerPrefs.HasKey($"EquippedSkin_{i}"))
            {
                int skinID = PlayerPrefs.GetInt($"EquippedSkin_{i}");
                SkinSO skin = FindSkinByID(skinID);
                
                if (skin != null)
                {
                    equippedSkins[i] = skin;
                    Debug.Log($"[SkinManager] Loaded saved skin: {skin.SkinName}");
                }
            }
        }
    }
    
    private SkinSO FindSkinByID(int skinID)
    {
        foreach (var skin in allSkins)
        {
            if (skin != null && skin.SkinID == skinID)
            {
                return skin;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
