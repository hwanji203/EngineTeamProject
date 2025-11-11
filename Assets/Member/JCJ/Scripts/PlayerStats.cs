using UnityEngine;

//플레이어 스탯을 계산하고 관리
public class PlayerStats : MonoBehaviour, ISkillCaster
{
    [Header("기본 능력치")]
    [SerializeField] private int baseAttack = 100;      // 기본 공격력
    [SerializeField] private int baseDefense = 50;      // 기본 방어력
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    [Header("마나")]
    [SerializeField] private float maxMana = 100f;
    private float currentMana = 100f;
    [SerializeField] private float manaRegenRate = 5f;
    
    private EquipmentManager equipmentManager;
    private int totalAttackBonus = 0;           // 고정 공격력 증가
    private float totalAttackBonusPercent = 0;  // % 공격력 증가
    private int totalDefenseBonus = 0;          // 고정 방어력 증가
    private float totalDefenseBonusPercent = 0; // % 방어력 증가
    
    public Transform Transform => transform;
    public float CurrentMana => currentMana;
    public int CurrentHealth => currentHealth;
    
    // 최종 공격력 계산 (기본 + 고정 + %)
    public int GetTotalAttack()
    {
        float baseWithFixed = baseAttack + totalAttackBonus;
        float finalAttack = baseWithFixed * (1f + totalAttackBonusPercent / 100f);
        return Mathf.RoundToInt(finalAttack);
    }
    
    // 최종 방어력 계산 (기본 + 고정 + %)
    public int GetTotalDefense()
    {
        float baseWithFixed = baseDefense + totalDefenseBonus;
        float finalDefense = baseWithFixed * (1f + totalDefenseBonusPercent / 100f);
        return Mathf.RoundToInt(finalDefense);
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
        equipmentManager = EquipmentManager.Instance;
        
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged += OnEquipmentChanged;
            UpdateStats();
        }
    }
    
    private void Update()
    {
        RegenerateMana();
    }
    
    // 장비 변경 시 스탯 재계산
    private void OnEquipmentChanged(int slotIndex, EquipmentSO equipment)
    {
        UpdateStats();
    }
    
    // 모든 장비의 능력치를 합산
    private void UpdateStats()
    {
        totalAttackBonus = 0;
        totalAttackBonusPercent = 0;
        totalDefenseBonus = 0;
        totalDefenseBonusPercent = 0;
        
        // 모든 슬롯의 장비 확인
        for (int i = 0; i < 3; i++)
        {
            EquipmentSO equipment = equipmentManager.GetEquippedItem(i);
            
            if (equipment != null)
            {
                totalAttackBonus += equipment.AttackBonus;
                totalAttackBonusPercent += equipment.AttackBonusPercent;
                totalDefenseBonus += equipment.DefenseBonus;
                totalDefenseBonusPercent += equipment.DefenseBonusPercent;
            }
        }
        
        Debug.Log($"최종 공격력: {GetTotalAttack()}, 최종 방어력: {GetTotalDefense()}");
    }
    
    private void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana = Mathf.Min(currentMana + manaRegenRate * Time.deltaTime, maxMana);
        }
    }
    
    public void ConsumeMana(float amount)
    {
        currentMana = Mathf.Max(currentMana - amount, 0f);
    }
    
    public void ApplyDefenseBuff(float multiplier, float duration)
    {
        // 구현 생략
    }
    
    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(1, damage - GetTotalDefense() / 10);
        currentHealth -= actualDamage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
        
        Debug.Log($"데미지: {actualDamage}, 남은 체력: {currentHealth}");
    }
    
    private void Die()
    {
        Debug.Log("플레이어 사망!");
        // 게임 오버 처리
    }
    
    private void OnDestroy()
    {
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }
}

