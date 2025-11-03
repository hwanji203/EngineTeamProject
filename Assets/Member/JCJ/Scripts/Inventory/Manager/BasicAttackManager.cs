using UnityEngine;

// 장비에 따라 기본 공격이 바뀌는 시스템
public class BasicAttackManager : MonoBehaviour
{
    private SkillSO currentBasicAttack; // 현재 기본 공격
    private EquipmentManager equipmentManager;
    
    private void Start()
    {
        equipmentManager = EquipmentManager.Instance;
        
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged += OnEquipmentChanged;
            UpdateBasicAttack();
        }
    }
    
    // 장비 변경 시 기본 공격 업데이트
    private void OnEquipmentChanged(int slotIndex, EquipmentSO equipment)
    {
        UpdateBasicAttack();
    }
    
    // 현재 장착된 장비들의 기본 공격 수집
    private void UpdateBasicAttack()
    {
        // 장착된 모든 장비의 기본 공격을 수집할 수도 있음
        // 여기선 첫 번째 장비의 공격을 사용
        EquipmentSO equipment = equipmentManager.GetEquippedItem(0);
        
        if (equipment != null && equipment.BasicAttackSkill != null)
        {
            currentBasicAttack = equipment.BasicAttackSkill;
            Debug.Log($"기본 공격 변경: {currentBasicAttack.SkillName}");
        }
        else
        {
            currentBasicAttack = null;
            Debug.Log("기본 공격 없음");
        }
    }
    
    // 기본 공격 실행 (마우스 좌클릭 등)
    public void ExecuteBasicAttack(ISkillCaster caster)
    {
        if (currentBasicAttack != null)
        {
            currentBasicAttack.Execute(caster);
        }
    }
    
    public SkillSO GetCurrentBasicAttack() => currentBasicAttack;
    
    private void OnDestroy()
    {
        if (equipmentManager != null)
        {
            equipmentManager.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }
}