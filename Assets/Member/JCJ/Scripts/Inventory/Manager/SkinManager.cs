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
    
    [Header("Default Sprite")]
    [SerializeField] private Sprite defaultHeadSprite;
    
    [Header("All Skins Database")]
    [SerializeField] private SkinSO[] allSkins;
    
    private bool isInitialized = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SkinManager 인스턴스 생성 및 DontDestroyOnLoad 적용");
        }
        else if (Instance != this)
        {
            Debug.Log("SkinManager 중복 인스턴스 제거");
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        if (!isInitialized)
        {
            LoadEquippedSkins();
            ApplyAllSkins();
            isInitialized = true;
            Debug.Log("SkinManager 초기화 완료");
        }
    }
    
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        Debug.Log($"씬 로드됨: {scene.name}");
        
        if (headRenderer == null)
        {
            FindRenderersInScene();
        }
        
        ApplyAllSkins();
    }
    
    private void FindRenderersInScene()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            Transform[] children = player.GetComponentsInChildren<Transform>();
            
            foreach (Transform child in children)
            {
                if (child.name.Contains("Head") && headRenderer == null)
                {
                    headRenderer = child.GetComponent<SpriteRenderer>();
                    break;
                }
            }
            
            Debug.Log($"렌더러 재연결: Head={headRenderer != null}");
        }
    }
    
    public void EquipSkin(int slotIndex, SkinSO skin)
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
            int existingSlot = FindSkinSlot(skin.SkinID);
            
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
        
        SaveEquippedSkins();
    }
    
    private void ApplySkinToCharacter(int slotIndex, SkinSO skin)
    {
        Sprite spriteToApply = null;
        
        if (skin != null)
        {
            spriteToApply = skin.SkinSprite;
        }
        else
        {
            spriteToApply = defaultHeadSprite;
        }
        
        if (headRenderer != null)
        {
            headRenderer.sprite = spriteToApply;
        }
    }
    
    private void ApplyAllSkins()
    {
        Debug.Log("모든 스킨 적용 중...");
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
                Debug.Log($"스킨 저장: 슬롯 {i} = ID {equippedSkins[i].SkinID}");
            }
            else
            {
                PlayerPrefs.DeleteKey($"EquippedSkin_{i}");
                Debug.Log($"스킨 제거: 슬롯 {i}");
            }
        }
        PlayerPrefs.Save();
    }
    
    private void LoadEquippedSkins()
    {
        Debug.Log("장착된 스킨 불러오기 시작");
        for (int i = 0; i < slotCount; i++)
        {
            if (PlayerPrefs.HasKey($"EquippedSkin_{i}"))
            {
                int skinID = PlayerPrefs.GetInt($"EquippedSkin_{i}");
                SkinSO skin = FindSkinByID(skinID);
                
                if (skin != null)
                {
                    equippedSkins[i] = skin;
                    Debug.Log($"스킨 불러오기: 슬롯 {i} = {skin.SkinName} (ID {skinID})");
                }
                else
                {
                    Debug.LogWarning($"스킨 ID {skinID}를 찾을 수 없음");
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
}
