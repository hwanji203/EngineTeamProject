using UnityEngine;

public abstract class SkillSO : ScriptableObject
{
    [SerializeField] private string skillName;        // 스킬 이름
    [SerializeField] private Sprite skillIcon;        // 스킬 아이콘 이미지
    [SerializeField] private float cooldown;          // 스킬 쿨타임 (초)
    [SerializeField] private int cost;
    
    public string SkillName => skillName;             // 스킬 이름 읽기
    public Sprite SkillIcon => skillIcon;             // 스킬 아이콘 읽기
    public float Cooldown => cooldown;                // 쿨타임 읽기
    public int Cost => cost;              // 마나 비용 읽기\
    public abstract void Execute(ISkillCaster caster);
}