using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItemUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject purchasedOverlay;

    private StoreItemData data;
    private StoreManager manager;
    private bool isPurchased = false;

    public void Setup(StoreItemData itemData, StoreManager storeManager)
    {
        data = itemData;
        manager = storeManager;

        icon.sprite = data.itemIcon;
        nameText.text = data.itemName;
        priceText.text = $"{data.price} G";

        buyButton.onClick.AddListener(() => manager.TryPurchaseItem(data, this));
        UpdateUI();
    }

    public void SetPurchased()
    {
        isPurchased = true;
        UpdateUI();
    }

    private void UpdateUI()
    {
        purchasedOverlay.SetActive(isPurchased);
        buyButton.interactable = !isPurchased;
    }

    public void PlayInsufficientFundsFeedback()
    {
        priceText.color = Color.red;
        Invoke(nameof(ResetPriceColor), 0.4f);
    }

    private void ResetPriceColor()
    {
        priceText.color = Color.black;
    }
}
