using UnityEngine;

public enum SkinBodyPart
{
    Head
}

[CreateAssetMenu(fileName = "Skin_", menuName = "Skin/New Skin")]
public class SkinSO : ScriptableObject
{
    [Header("고유 ID")]
    [SerializeField] private int skinID;
    
    [SerializeField] private string skinName;
    [SerializeField] private Sprite skinIcon;
    [SerializeField] private Sprite skinSprite;
    [SerializeField] private int cost;
    [SerializeField] private SkinBodyPart bodyPart = SkinBodyPart.Head;
    
    public int SkinID => skinID;
    public string SkinName => skinName;
    public Sprite SkinIcon => skinIcon;
    public Sprite SkinSprite => skinSprite;
    public int Cost => cost;
    public SkinBodyPart BodyPart => bodyPart;
}