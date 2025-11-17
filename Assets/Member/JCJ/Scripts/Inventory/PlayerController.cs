using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, ISkillCaster
{
    
    [Header("스킬 키 설정")]
    [SerializeField] private KeyCode skill1Key = KeyCode.E;
    [SerializeField] private KeyCode skill2Key = KeyCode.R;
    [SerializeField] private KeyCode skill3Key = KeyCode.T;
    [SerializeField] private KeyCode basicAttackKey = KeyCode.Mouse0; // 좌클릭
    
    private SkillManager skillManager;
    private BasicAttackManager basicAttackManager;
    
    public Transform Transform => transform;
        
    private void Awake()
    {
        skillManager = GetComponent<SkillManager>();
        basicAttackManager = GetComponent<BasicAttackManager>();
    }
    
    private void Update()
    {
        HandleInput();
    }
    
    private void HandleInput()
    {
        // 기본 공격
        if (Input.GetKeyDown(basicAttackKey))
        {
            basicAttackManager.ExecuteBasicAttack(this);
        }
        
        // 특수 스킬 (E, R, T)
        if (Input.GetKeyDown(skill1Key))
            skillManager.TryExecuteSkill(0);
        
        if (Input.GetKeyDown(skill2Key))
            skillManager.TryExecuteSkill(1);
        
        if (Input.GetKeyDown(skill3Key))
            skillManager.TryExecuteSkill(2);
    }
    
    public void ApplyDefenseBuff(float duration)
    {
        StartCoroutine(DefenseBuffCoroutine(duration));
    }
    
    private IEnumerator DefenseBuffCoroutine( float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
