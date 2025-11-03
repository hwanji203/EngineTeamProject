using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    // [SerializeField]로 인스펙터에 노출하되 외부 수정은 불가능하게 함
    [SerializeField] private string skillName;        // 스킬 이름
    [SerializeField] private Sprite skillIcon;        // 스킬 아이콘 이미지
    [SerializeField] private float cooldown;          // 스킬 쿨타임 (초)
    [SerializeField] private float manaCost;          // 스킬 사용에 필요한 마나
    
    // 프로퍼티로 읽기 전용 접근 제공 (캡슐화)
    public string SkillName => skillName;             // 스킬 이름 읽기
    public Sprite SkillIcon => skillIcon;             // 스킬 아이콘 읽기
    public float Cooldown => cooldown;                // 쿨타임 읽기
    public float ManaCost => manaCost;                // 마나 비용 읽기
    
    /// 스킬 실행 로직 (추상 메서드 - 하위 클래스에서 반드시 구현)
    /// 템플릿 메서드 패턴: 기본 구조는 여기서 정의하고 세부 구현은 하위 클래스에 위임
    public abstract void Execute(ISkillCaster caster);
    
    /// 스킬 사용 가능 여부 체크 (기본 구현: 마나 체크)
    /// virtual로 하위 클래스에서 오버라이드 가능
    public virtual bool CanExecute(ISkillCaster caster)
    {
        // 현재 마나가 스킬 비용 이상인지 확인
        return caster.CurrentMana >= manaCost;
    }
}