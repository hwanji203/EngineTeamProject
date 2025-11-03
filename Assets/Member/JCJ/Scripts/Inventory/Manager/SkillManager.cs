using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 스킬 슬롯 개수 상수
    private const int SKILL_COUNT = 3;
    
    // 현재 활성화된 스킬 배열 (EquipmentSO의 LinkedSkill을 참조)
    private SkillSO[] activeSkills = new SkillSO[SKILL_COUNT];
    
    // 쿨타임 데이터 배열 (struct 배열로 메모리 효율적)
    private CooldownData[] cooldowns = new CooldownData[SKILL_COUNT];
    
    // 스킬을 시전할 캐릭터 (ISkillCaster 인터페이스 참조)
    private ISkillCaster caster;
    
    
    // 쿨타임 데이터 구조체
    private struct CooldownData
    {
        public float remainingTime;  // 남은 쿨타임 (초)
        public float totalTime;      // 전체 쿨타임 (초)
        
        // 쿨타임이 끝났는지 확인하는 프로퍼티
        public bool IsReady => remainingTime <= 0f;
        
        // 쿨타임 진행도 (0.0 ~ 1.0, 1.0이면 준비 완료)
        public float Progress => totalTime > 0 ? 1f - (remainingTime / totalTime) : 1f;
    }
    
    private void Awake()
    {
        // 같은 GameObject에 있는 ISkillCaster 구현체 가져오기
        caster = GetComponent<ISkillCaster>();
        
        // 없으면 에러 로그 (필수 컴포넌트)
        if (caster == null)
        {
            Debug.LogError("ISkillCaster가 필요합니다! PlayerController를 추가하세요.");
        }
    }
    
    private void Start()
    {
        // EquipmentManager의 장비 변경 이벤트 구독
        if (EquipmentManager.Instance != null)
        {
            // 이벤트에 OnEquipmentChanged 함수 등록
            EquipmentManager.Instance.OnEquipmentChanged += OnEquipmentChanged;
            
            // 게임 시작 시 현재 장착된 장비의 스킬로 초기화
            SyncSkillsWithEquipment();
        }
    }
    
    private void Update()
    {
        // 쿨타임 감소 처리
        UpdateCooldowns(Time.deltaTime);
    }
    /// 장비 변경 이벤트 핸들러 (EquipmentManager에서 호출)
    private void OnEquipmentChanged(int slotIndex, EquipmentSO newEquipment)
    {
        // 유효한 슬롯 인덱스인지 확인
        if (slotIndex >= 0 && slotIndex < SKILL_COUNT)
        {
            // 새 장비의 스킬로 교체 (null이면 스킬 비활성화)
            // ?. 연산자: newEquipment가 null이 아닐 때만 LinkedSkill 접근
            activeSkills[slotIndex] = newEquipment?.LinkedSkill;
        }
    }
    
    /// 게임 시작 시 현재 장착된 장비와 스킬 동기화
    private void SyncSkillsWithEquipment()
    {
        // 모든 슬롯 순회
        for (int i = 0; i < SKILL_COUNT; i++)
        {
            // EquipmentManager에서 장착된 장비 가져오기
            EquipmentSO equipment = EquipmentManager.Instance.GetEquippedItem(i);
            
            // 장비의 스킬을 activeSkills에 저장 (null이면 null 저장)
            activeSkills[i] = equipment?.LinkedSkill;
        }
    }
    
    /// 스킬 실행 시도 (키 입력 시 호출)
    public bool TryExecuteSkill(int slotIndex)
    {
        // 유효성 검사 1: 슬롯 인덱스가 올바른지
        if (!IsValidSlot(slotIndex)) return false;
        
        // 유효성 검사 2: 해당 슬롯에 스킬이 있는지
        if (activeSkills[slotIndex] == null) return false;
        
        // 유효성 검사 3: 쿨타임이 끝났는지
        if (!cooldowns[slotIndex].IsReady) return false;
        
        // 실행할 스킬 참조 가져오기
        SkillSO skill = activeSkills[slotIndex];
        
        // 유효성 검사 4: 스킬 사용 가능 여부 (마나 등)
        if (!skill.CanExecute(caster))
        {
            Debug.LogWarning($"{skill.SkillName} 사용 불가 (마나 부족 등)");
            return false; // 사용 불가
        }
        
        // 스킬 실행
        skill.Execute(caster);
        
        // 쿨타임 시작 (struct는 값 타입이므로 전체 재할당 필요)
        cooldowns[slotIndex].remainingTime = skill.Cooldown; // 남은 시간 초기화
        cooldowns[slotIndex].totalTime = skill.Cooldown;     // 전체 시간 저장
        
        return true; // 실행 성공
    }
    /// 쿨타임 업데이트 (매 프레임 호출)
    private void UpdateCooldowns(float deltaTime)
    {
        // 모든 스킬 슬롯 순회
        for (int i = 0; i < SKILL_COUNT; i++)
        {
            // 쿨타임이 남아있으면 감소
            if (cooldowns[i].remainingTime > 0)
            {
                cooldowns[i].remainingTime -= deltaTime; // 시간 감소
            }
        }
    }
    
    
    /// 특정 슬롯의 활성 스킬 가져오기
    public SkillSO GetActiveSkill(int slotIndex) => 
        IsValidSlot(slotIndex) ? activeSkills[slotIndex] : null;
    
    /// 특정 슬롯의 쿨타임 진행도 가져오기 (UI 표시용)
    public float GetCooldownProgress(int slotIndex) =>
        IsValidSlot(slotIndex) ? cooldowns[slotIndex].Progress : 1f;
    
    /// 특정 슬롯의 스킬이 사용 가능한지 확인
    public bool IsSkillReady(int slotIndex) =>
        IsValidSlot(slotIndex) && cooldowns[slotIndex].IsReady;
    
    /// 슬롯 인덱스 유효성 검사 헬퍼 함수
    private bool IsValidSlot(int index) => index >= 0 && index < SKILL_COUNT;
    
    private void OnDestroy()
    {
        // 이벤트 구독 해제 (메모리 누수 방지)
        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }
}
