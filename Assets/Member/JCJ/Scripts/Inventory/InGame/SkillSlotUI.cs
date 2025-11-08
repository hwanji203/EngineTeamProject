using UnityEngine;
using UnityEngine.UI;

// 관리 화면 슬롯용 (인벤토리, 캐릭터 창 등)
public class SkillSlotUI : MonoBehaviour
{
    [System.Serializable]
    private class SkillSlot
    {
        public Image skillIcon;
    }
    
    [Header("슬롯 UI")]
    [SerializeField] private SkillSlot[] skillSlots = new SkillSlot[3];
    [SerializeField] private Sprite emptySkillSprite;
    
    private SkillManager skillManager;
    
    private void Start()
    {
        skillManager = GetComponent<SkillManager>();
        if (EquipmentManager.Instance != null)
        {
            EquipmentManager.Instance.OnEquipmentChanged += UpdateSlotUI;
        }
        UpdateAllSlots();
    }
    
    private void UpdateSlotUI(int slotIndex, EquipmentSO equipment)
    {
        if (slotIndex < 0 || slotIndex >= skillSlots.Length) return;
        
        SkillSO skill = skillManager.GetActiveSkill(slotIndex);
        
        if (skill != null)
        {
            skillSlots[slotIndex].skillIcon.sprite = skill.SkillIcon;
            skillSlots[slotIndex].skillIcon.color = Color.white;
        }
        // else
        // {
        //     skillSlots[slotIndex].skillIcon.sprite = emptySkillSprite;
        //     skillSlots[slotIndex].skillIcon.color = new Color(1f, 1f, 1f, 0.3f);
        // }
    }
    
    private void UpdateAllSlots()
    {
        for (int i = 0; i < skillSlots.Length; i++)
            UpdateSlotUI(i, null);
    }
    
    private void OnDestroy()
    {
        if (EquipmentManager.Instance != null)
            EquipmentManager.Instance.OnEquipmentChanged -= UpdateSlotUI;
    }
}