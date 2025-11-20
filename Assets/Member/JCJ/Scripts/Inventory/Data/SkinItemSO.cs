    using UnityEngine;

[CreateAssetMenu(fileName = "SkinItem_", menuName = "Skin/New Skin Item")]
public class SkinItemSO : ScriptableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private SkinBodyPart bodyPart;
    [SerializeField] private int price;
    
    public int ItemID => itemID;
    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public Sprite ItemSprite => itemSprite;
    public SkinBodyPart BodyPart => bodyPart;
    public int Price => price;
    
    public void InitFromSkin(SkinSO skin)
    {
        itemID = Random.Range(1000, 9999);
        itemName = skin.SkinName;
        itemIcon = skin.SkinIcon;
        itemSprite = skin.SkinSprite;
        bodyPart = skin.BodyPart;
        price = skin.Cost;
    }
}