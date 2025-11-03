using UnityEngine;

public enum EquipmentTier //등급 일단 enum으로 만들어둠
{
    Common,
    Rare,
    Epic
}

[CreateAssetMenu(fileName = "Equipment_", menuName = "Equipment/New Equipment")]
public class EquipmentSO : ScriptableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    
    [Header("스탯 보너스")]
    [SerializeField] private int attackBonus;           // 고정 공격력
    [SerializeField] private float attackBonusPercent;  // 공격력 % (예: 10 = +10%)
    [SerializeField] private int defenseBonus;          // 고정 방어력
    [SerializeField] private float defenseBonusPercent; // 방어력 % (예: 15 = +15%)
    
    [Header("등급 및 가격")]
    [SerializeField] private EquipmentTier tier;
    [SerializeField] private int price;
    
    [Header("기본 공격 스킬")]
    [SerializeField] private SkillSO basicAttackSkill;   // 장비별 기본 공격
    
    
    [SerializeField] private SkillSO linkedSkill;    // ← 필드
    public SkillSO LinkedSkill => linkedSkill;       // ← 프로퍼티

    public int ItemID => itemID;
    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public int AttackBonus => attackBonus;
    public float AttackBonusPercent => attackBonusPercent;
    public int DefenseBonus => defenseBonus;
    public float DefenseBonusPercent => defenseBonusPercent;
    public EquipmentTier Tier => tier;
    public int Price => price;
    public SkillSO BasicAttackSkill => basicAttackSkill;
}