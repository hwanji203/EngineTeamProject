using UnityEngine;

public enum SkinBodyPart
{
    Head,
    Body,
    Weapon
}

[CreateAssetMenu(fileName = "Skin_", menuName = "Skin/New Skin")]
public class SkinSO : ScriptableObject
{
    [SerializeField] private string skinName;
    [SerializeField] private Sprite skinIcon;
    [SerializeField] private Sprite skinSprite;
    [SerializeField] private int cost;
    [SerializeField] private SkinBodyPart bodyPart;
    
    public string SkinName => skinName;
    public Sprite SkinIcon => skinIcon;
    public Sprite SkinSprite => skinSprite;
    public int Cost => cost;
    public SkinBodyPart BodyPart => bodyPart;
}