using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SkillCooldownUI : MonoBehaviour
{
    [Header("프리팹 설정")]
    [SerializeField] private GameObject skillIconPrefab;
    [SerializeField] private Transform skillIconsContainer;
    [SerializeField] private Sprite emptySkillSprite;
    
    private SkillManager skillManager;
    private EquipmentManager equipmentManager;
    private List<SkillCooldownSlot> cooldownSlots = new List<SkillCooldownSlot>();
    
    private void Awake()
    {
        if (FindObjectsOfType<SkillCooldownUI>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private class SkillCooldownSlot
    {
        public int slotIndex;
        public Image skillIcon;
        public Image cooldownOverlay;
        public TextMeshProUGUI cooldownText; // ← Text → TextMeshProUGUI
        public GameObject slotObject;
    }
    
    private void Start()
    {
        skillManager = FindObjectOfType<SkillManager>();
        equipmentManager = EquipmentManager.Instance;
        
        if (skillManager == null || equipmentManager == null)
        {
            Debug.LogError("SkillManager 또는 EquipmentManager가 없습니다!");
            return;
        }
        
        equipmentManager.OnEquipmentChanged += OnEquipmentChanged;
        RefreshSkillIcons();
    }
    
    private void Update()
    {
        UpdateCooldownDisplay();
    }
    
    private void OnEquipmentChanged(int slotIndex, EquipmentSO equipment)
    {
        RefreshSkillIcons();
    }
    
    private void RefreshSkillIcons()
    {
        foreach (var slot in cooldownSlots)
        {
            Destroy(slot.slotObject);
        }
        cooldownSlots.Clear();
        
        for (int i = 0; i < 3; i++)
        {
            EquipmentSO equipment = equipmentManager.GetEquippedItem(i);
            
            if (equipment?.LinkedSkill != null)
            {
                CreateSkillIcon(i, equipment.LinkedSkill);
            }
        }
    }
    
    private void CreateSkillIcon(int slotIndex, SkillSO skill)
    {
        GameObject newSlot = Instantiate(skillIconPrefab, skillIconsContainer);
        
        // GetChild로 순서대로 찾기
        Image skillIconImage = newSlot.transform.GetChild(0)?.GetComponent<Image>();
        Image cooldownOverlay = newSlot.transform.GetChild(1)?.GetComponent<Image>();
        TextMeshProUGUI cooldownText = newSlot.transform.GetChild(2)?.GetComponent<TextMeshProUGUI>(); // ← 변경
        
        // 초기화
        if (skillIconImage != null)
        {
            skillIconImage.sprite = skill.SkillIcon;
            skillIconImage.color = Color.white;
        }
        
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 0;
        }
        
        if (cooldownText != null)
        {
            cooldownText.text = "";
            cooldownText.color = Color.white;
            cooldownText.fontSize = 36;
        }
        
        SkillCooldownSlot slot = new SkillCooldownSlot
        {
            slotIndex = slotIndex,
            skillIcon = skillIconImage,
            cooldownOverlay = cooldownOverlay,
            cooldownText = cooldownText,
            slotObject = newSlot
        };
        
        cooldownSlots.Add(slot);
    }
    
    private void UpdateCooldownDisplay()
    {
        if (skillManager == null || equipmentManager == null) return;
        
        foreach (var slot in cooldownSlots)
        {
            EquipmentSO equipment = equipmentManager.GetEquippedItem(slot.slotIndex);
            SkillSO skill = equipment?.LinkedSkill;
            
            if (skill == null) continue;
            
            float progress = skillManager.GetCooldownProgress(slot.slotIndex);
            
            // [1] 아이콘
            if (slot.skillIcon != null)
            {
                slot.skillIcon.sprite = skill.SkillIcon;
            }
            
            // [2] 오버레이
            if (slot.cooldownOverlay != null)
            {
                slot.cooldownOverlay.fillAmount = 1f - progress;
            }
            
            // [3] 텍스트 (TextMeshProUGUI)
            if (slot.cooldownText != null)
            {
                if (progress < 1f)
                {
                    float remainingTime = (1f - progress) * skill.Cooldown;
                    slot.cooldownText.text = remainingTime.ToString("F1");
                    slot.cooldownText.enabled = true;
                }
                else
                {
                    slot.cooldownText.text = "";
                    slot.cooldownText.enabled = false;
                }
            }
        }
    }
    
    private void OnDestroy()
    {
        if (equipmentManager != null)
            equipmentManager.OnEquipmentChanged -= OnEquipmentChanged;
    }
}
