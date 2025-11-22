using UnityEngine;
using DG.Tweening;
using TMPro;

public class SlidingUI : MonoBehaviour
{
    public RectTransform containerPanel;
    public float slideDuration = 0.5f;
    
    private float mapPosX = 1920f;
    private float inventoryPosX = 0;
    private float shopPosX = -1920f;
    
    [SerializeField] private TextMeshProUGUI mapText;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private TextMeshProUGUI shopText;

    private TextMeshProUGUI currentText;
    
    public void SlideToMap()
    {
        containerPanel.DOAnchorPosX(mapPosX, slideDuration).SetEase(Ease.OutQuad);
        currentText = mapText;
        ResetText();
    }
    
    public void SlideToInventory()
    {
        containerPanel.DOAnchorPosX(inventoryPosX, slideDuration).SetEase(Ease.OutQuad);
        currentText = inventoryText;
        ResetText();
    }
    
    public void SlideToShop()
    {
        containerPanel.DOAnchorPosX(shopPosX, slideDuration).SetEase(Ease.OutQuad);
        currentText = shopText;
        ResetText();
    }

    private void ResetText()
    {
        currentText.fontSize = 72;
        if (mapText != currentText)
        {
            mapText.fontSize = 48;
        }
        if (inventoryText != currentText)
        {
            inventoryText.fontSize = 48;
        }
        if (shopText != currentText)
        {
            shopText.fontSize = 48;
        }
    }
}