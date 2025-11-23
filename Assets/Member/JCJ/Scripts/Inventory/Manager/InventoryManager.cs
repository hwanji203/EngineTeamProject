using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [Header("인벤토리 설정")]
    [SerializeField] private GameObject draggableItemPrefab;
    [SerializeField] private Transform inventoryParent;
    
    [Header("All Skins Database")]
    [SerializeField] private SkinSO[] allSkins;
    
    private List<SkinSO> ownedSkins = new List<SkinSO>();
    private Dictionary<SkinSO, GameObject> skinUIObjects = new Dictionary<SkinSO, GameObject>();
    private SkinManager skinManager;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        skinManager = SkinManager.Instance;
        
        if (skinManager != null)
        {
            skinManager.OnSkinChanged += OnSkinEquipChanged;
        }
        
        LoadOwnedSkins();
        CreateAllSkinUI();
        
        HideEquippedSkins();
    }
    
    private void HideEquippedSkins()
    {
        if (skinManager == null) return;
        
        for (int i = 0; i < 10; i++)
        {
            SkinSO equippedSkin = skinManager.GetEquippedSkin(i);
            if (equippedSkin != null)
            {
                HideSkin(equippedSkin);
            }
        }
    }
    private void OnSkinEquipChanged(int slotIndex, SkinSO skin)
    {
        if (skin != null)
        {
            HideSkin(skin);
        }
    }
    
    public void ShowSkinIfOwned(SkinSO skin)
    {
        if (HasSkin(skin))
        {
            ShowSkin(skin);
        }
    }
    
    public void AddSkin(SkinSO skin)
    {
        if (!ownedSkins.Contains(skin))
        {
            ownedSkins.Add(skin);
            CreateSkinUI(skin);
            SaveOwnedSkins();
        }
    }
    
    private void CreateSkinUI(SkinSO skin)
    {
        if (skinUIObjects.ContainsKey(skin))
        {
            return;
        }
        
        GameObject obj = Instantiate(draggableItemPrefab, inventoryParent);
        var draggable = obj.GetComponent<DraggableSkinItem>();
        if (draggable != null)
        {
            draggable.Init(skin);
        }
        
        skinUIObjects[skin] = obj;
        
        if (skinManager != null && IsCurrentlyEquipped(skin))
        {
            obj.SetActive(false);
        }
    }
    private bool IsCurrentlyEquipped(SkinSO skin)
    {
        if (skinManager == null) return false;
        
        for (int i = 0; i < 10; i++)
        {
            if (skinManager.GetEquippedSkin(i) == skin)
            {
                return true;
            }
        }
        return false;
    }
    
    private void CreateAllSkinUI()
    {
        foreach (var skin in ownedSkins)
        {
            CreateSkinUI(skin);
        }
    }
    
    public void HideSkin(SkinSO skin)
    {
        if (skinUIObjects.ContainsKey(skin))
        {
            var obj = skinUIObjects[skin];
            
            var canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null) 
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            
            var image = obj.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
                image.raycastTarget = true;
        
            if (obj.transform.parent != inventoryParent)
                obj.transform.SetParent(inventoryParent, false);
            
            var rect = obj.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = Vector2.zero;
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;
            }
            
            obj.SetActive(false);
        }
    }
    
    public void ShowSkin(SkinSO skin)
    {
        if (skinUIObjects.ContainsKey(skin))
        {
            var obj = skinUIObjects[skin];

            if (obj.transform.parent != inventoryParent)
                obj.transform.SetParent(inventoryParent, false);

            var rect = obj.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition = Vector2.zero;
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;
            }
            var canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }

            obj.SetActive(true);
        }
    }
    
    public bool HasSkin(SkinSO skin)
    {
        return ownedSkins.Contains(skin);
    }
    
    private void SaveOwnedSkins()
    {
        PlayerPrefs.SetInt("OwnedSkinsCount", ownedSkins.Count);
        
        for (int i = 0; i < ownedSkins.Count; i++)
        {
            PlayerPrefs.SetInt($"OwnedSkin_{i}", ownedSkins[i].SkinID);
        }
        
        PlayerPrefs.Save();
    }
    
    private void LoadOwnedSkins()
    {
        ownedSkins.Clear();
        
        int count = PlayerPrefs.GetInt("OwnedSkinsCount", 0);
        
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.HasKey($"OwnedSkin_{i}"))
            {
                int skinID = PlayerPrefs.GetInt($"OwnedSkin_{i}");
                SkinSO skin = FindSkinByID(skinID);
                
                if (skin != null && !ownedSkins.Contains(skin))
                {
                    ownedSkins.Add(skin);
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
        if (skinManager != null)
        {
            skinManager.OnSkinChanged -= OnSkinEquipChanged;
        }
        
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
