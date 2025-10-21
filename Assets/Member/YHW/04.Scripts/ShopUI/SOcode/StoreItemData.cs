using UnityEngine;

[CreateAssetMenu(fileName = "NewStoreItem", menuName = "SO/Item")]
public class StoreItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int price;
    [TextArea] public string description;
}
