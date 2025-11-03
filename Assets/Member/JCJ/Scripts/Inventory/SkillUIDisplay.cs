using UnityEngine;
using UnityEngine.UI;

// 스킬 UI 표시와 쿨타임 시각화 담당
//UI 표시만 담당 (스킬 실행은 SkillManager가 담당)
public class SkillUIDisplay : MonoBehaviour
{
    [System.Serializable] // 인스펙터에 표시되도록
    private class SkillUISlot
    {
        public Image skillIcon;          // 스킬 아이콘 이미지
        public Image cooldownOverlay;    // 쿨타임 표시 오버레이 (fillAmount 사용)
        public Text cooldownText;        // 남은 쿨타임 숫자 표시 (옵션)
    }
    
    [Header("스킬 UI 슬롯들")]
    [SerializeField] private SkillUISlot[] skillSlots = new SkillUISlot[3]; // 3개 슬롯
    
    [Header("빈 스킬 아이콘")]
    [SerializeField] private Sprite emptySkillSprite; // 스킬 없을 때 표시할 이미지
    
    // SkillManager 캐싱
    private SkillManager skillManager;
    
    /// Unity 생명주기: 게임 시작 시
    private void Start()
    {
        // SkillManager 가져오기 (같은 GameObject에 있어야 함)
        skillManager = GetComponent<SkillManager>();
        
        // EquipmentManager 이벤트 구독 (장비 변경 시 스킬 UI 업데이트)
        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.OnEquipmentChanged += OnEquipmentChanged;
        }
        
        // 초기 UI 업데이트
        UpdateAllSkillUI();
    }
    
    /// Unity 생명주기: 매 프레임 (쿨타임 표시 업데이트)
    private void Update()
    {
        // 쿨타임 진행 상태 시각화
        UpdateCooldownDisplay();
    }
    
    /// 장비 변경 이벤트 핸들러
    private void OnEquipmentChanged(int slotIndex, EquipmentSO equipment)
    {
        // 변경된 슬롯의 스킬 UI만 업데이트 (최적화)
        UpdateSkillUI(slotIndex);
    }
    
    /// 특정 슬롯의 스킬 UI 업데이트
    private void UpdateSkillUI(int slotIndex)
    {
        // 유효성 검사
        if (slotIndex < 0 || slotIndex >= skillSlots.Length) return;
        
        // 해당 슬롯의 활성 스킬 가져오기
        SkillSO skill = skillManager.GetActiveSkill(slotIndex);
        
        // UI 슬롯 참조 가져오기
        SkillUISlot uiSlot = skillSlots[slotIndex];
        
        // 스킬이 있으면
        if (skill != null)
        {
            // 스킬 아이콘 표시
            uiSlot.skillIcon.sprite = skill.SkillIcon;
            uiSlot.skillIcon.color = Color.white; // 불투명하게
        }
        else
        {
            // 스킬이 없으면 빈 아이콘 표시
            uiSlot.skillIcon.sprite = emptySkillSprite;
            uiSlot.skillIcon.color = new Color(1f, 1f, 1f, 0.3f); // 반투명하게
        }
    }
    
    /// 모든 스킬 UI 업데이트 (초기화 시)
    private void UpdateAllSkillUI()
    {
        // 모든 슬롯 순회하며 업데이트
        for (int i = 0; i < skillSlots.Length; i++)
        {
            UpdateSkillUI(i);
        }
    }
    
    /// 쿨타임 표시 업데이트 (매 프레임 호출)
    private void UpdateCooldownDisplay()
    {
        // 모든 스킬 슬롯 순회
        for (int i = 0; i < skillSlots.Length; i++)
        {
            // SkillManager에서 쿨타임 진행도 가져오기 (0.0~1.0)
            float progress = skillManager.GetCooldownProgress(i);
            
            // UI 슬롯 참조
            SkillUISlot uiSlot = skillSlots[i];
            
            // 쿨타임 오버레이 업데이트 (Image.fillAmount 사용)
            if (uiSlot.cooldownOverlay != null)
            {
                // fillAmount: 1 - progress (1.0이면 준비 완료 = 오버레이 없음)
                uiSlot.cooldownOverlay.fillAmount = 1f - progress;
            }
            
            // 쿨타임 텍스트 업데이트 (옵션)
            if (uiSlot.cooldownText != null)
            {
                // 쿨타임 중이면 (progress < 1)
                if (progress < 1f)
                {
                    // 근사 남은 시간 계산 (정확하지 않지만 UI용으로 충분)
                    float remainingTime = (1f - progress) * 10f; // 임시 계산
                    
                    // 텍스트로 표시 ("F1" = 소수점 1자리)
                    uiSlot.cooldownText.text = remainingTime.ToString("F1");
                    uiSlot.cooldownText.enabled = true; // 텍스트 보이기
                }
                else
                {
                    // 쿨타임 끝나면 텍스트 숨기기
                    uiSlot.cooldownText.enabled = false;
                }
            }
        }
    }
    
    /// Unity 생명주기: 오브젝트 파괴 시
    private void OnDestroy()
    {
        // 이벤트 구독 해제 (메모리 누수 방지)
        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.OnEquipmentChanged -= OnEquipmentChanged;
        }
    }
}
