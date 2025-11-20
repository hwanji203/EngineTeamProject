using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    [Header("인벤토리 설정")]
    [SerializeField] private GameObject draggableItemPrefab;
    [SerializeField] private Transform inventoryParent;
    
    private List<SkinItemSO> ownedSkins = new List<SkinItemSO>();
    
    private Dictionary<SkinItemSO, GameObject> skinUIObjects = new Dictionary<SkinItemSO, GameObject>();
    
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
    
    public void AddSkin(SkinItemSO skin)
    {
        if (!ownedSkins.Contains(skin))
        {
            ownedSkins.Add(skin);
            CreateSkinUI(skin);
        }
    }
    
    private void CreateSkinUI(SkinItemSO skin)
    {
        GameObject obj = Instantiate(draggableItemPrefab, inventoryParent);
        var draggable = obj.GetComponent<DraggableSkinItem>();
        if (draggable != null)
        {
            draggable.Init(skin);
        }
        
        skinUIObjects[skin] = obj;
    }
    
    public void HideSkin(SkinItemSO skin)
    {
        if (skinUIObjects.ContainsKey(skin))
        {
            var obj = skinUIObjects[skin];
            var canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup != null) {
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
            }
            var image = obj.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
                image.raycastTarget = true;
        
            if (obj.transform.parent != inventoryParent)
                obj.transform.SetParent(inventoryParent, false);
            obj.SetActive(false);
        }
    }

    
    public void ShowSkin(SkinItemSO skin)
    {
        if (skinUIObjects.ContainsKey(skin))
        {
            var obj = skinUIObjects[skin];

            if (obj.transform.parent != inventoryParent)
                obj.transform.SetParent(inventoryParent, false);

            var rect = obj.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = Vector2.zero;

            obj.SetActive(true);
        }
    }

    
    public bool HasSkin(SkinItemSO skin)
    {
        return ownedSkins.Contains(skin);
    }
}
