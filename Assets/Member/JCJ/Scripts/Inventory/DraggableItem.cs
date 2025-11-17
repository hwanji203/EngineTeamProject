using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("아이템 데이터")]
    [SerializeField] private EquipmentSO equipmentData;
    
    private Image iconImage;
    
    public EquipmentSO EquipmentData => equipmentData;
    
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector2 originalPosition;
    private Canvas canvas;
    
    private void Awake()
    {
        iconImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        
        if (equipmentData != null && iconImage != null)
        {
            iconImage.sprite = equipmentData.ItemIcon;
        }
    }

    public void Init(EquipmentSO data)
    {
        equipmentData = data;

        if (iconImage != null && equipmentData != null)
        {
            iconImage.sprite = equipmentData.ItemIcon;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Start Drag : {equipmentData?.ItemName}");
        
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        
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
        Debug.Log($"End Drag : {equipmentData?.ItemName}");
        
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
}
