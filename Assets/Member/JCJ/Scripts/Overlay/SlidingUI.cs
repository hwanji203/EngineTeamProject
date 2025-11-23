using UnityEngine;
using DG.Tweening;
using TMPro;

public class SlidingUI : MonoBehaviour
{
    public RectTransform containerPanel;
    public float slideDuration = 0.5f;
    public float textAnimDuration = 0.3f;
    
    private float mapPosX = 1920f;
    private float inventoryPosX = 0;
    private float shopPosX = -1920f;
    
    [SerializeField] private TextMeshProUGUI mapText;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private TextMeshProUGUI shopText;

    private float maxSize = 72f;
    private float minSize = 48f;

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
        TextSizeAnimation(currentText, maxSize);
        
        if (mapText != currentText)
        {
            TextSizeAnimation(mapText, minSize);
        }
        if (inventoryText != currentText)
        {
            TextSizeAnimation(inventoryText, minSize);
        }
        if (shopText != currentText)
        {
            TextSizeAnimation(shopText, minSize);
        }
    }

    private void TextSizeAnimation(TextMeshProUGUI text, float targetSize)
    {
        float startSize = text.fontSize;
        
        DOTween.To(() => startSize, x => {
            startSize = x;
            text.fontSize = startSize;
        }, targetSize, textAnimDuration).SetEase(Ease.OutQuad);
    }
}