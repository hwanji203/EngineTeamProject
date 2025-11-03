using UnityEngine;

public interface ISkillCaster
{
    // 스킬 시전자의 Transform (위치, 회전 정보)
    Transform Transform { get; }
    
    // 현재 마나량 (스킬 사용 가능 여부 판단)
    float CurrentMana { get; }
    
    // 마나 소모 처리
    void ConsumeMana(float amount);
    
    // 방어력 버프 적용 (방패 스킬 등에서 사용)
    void ApplyDefenseBuff(float multiplier, float duration);
}