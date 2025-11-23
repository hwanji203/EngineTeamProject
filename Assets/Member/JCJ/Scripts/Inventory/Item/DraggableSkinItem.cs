using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableSkinItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("스킨 데이터")]
    [SerializeField] private SkinSO skinData;
    
    private Image iconImage;
    
    public SkinSO SkinData => skinData;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector2 originalPosition;
    private int originalSiblingIndex;
    private Canvas canvas;
    
    private void OnEnable()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        var image = GetComponent<UnityEngine.UI.Image>();
        if (image != null)
            image.raycastTarget = true;
    }
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        iconImage = GetComponent<Image>();
    }

    public void Init(SkinSO data)
    {
        skinData = data;

        if (iconImage == null)
        {
            iconImage = GetComponent<Image>();
        }

        if (iconImage != null && skinData != null)
        {
            iconImage.sprite = skinData.SkinIcon;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        originalSiblingIndex = transform.GetSiblingIndex();
        
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        
        transform.SetParent(canvas.transform);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        // 드롭 성공 여부 확인
        bool droppedOnSlot = false;
        
        if (eventData.pointerEnter != null)
        {
            var slotUI = eventData.pointerEnter.GetComponent<SkinSlotUI>();
            if (slotUI != null)
            {
                droppedOnSlot = true;
            }
        }
        
        // 드롭 실패 시에만 원위치 복귀
        if (!droppedOnSlot)
        {
            transform.SetParent(originalParent);
            transform.SetSiblingIndex(originalSiblingIndex);
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;
        }
    }
}
