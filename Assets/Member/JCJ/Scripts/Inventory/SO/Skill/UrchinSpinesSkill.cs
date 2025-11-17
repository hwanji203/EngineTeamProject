using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;

[CreateAssetMenu(fileName = "Skill_UrchinSpines2D", menuName = "Skills/Urchin Spines 2D")]
public class UrchinSpinesSkill : SkillSO
{
    [Header("스킬 설정")]
    [SerializeField] private int spineCount = 3;
    [SerializeField] private int damagePerSpine = 25;
    [SerializeField] private float shootAngle = 60f;
    [SerializeField] private float spineSpeed = 20f;
    [SerializeField] private float spineLifetime = 3f;
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("프리팹")]
    [SerializeField] private GameObject spinePrefab;
    
    [Header("이펙트")]
    [SerializeField] private AudioClip shootSound;
    
    public override void Execute(ISkillCaster caster)
    {
        Debug.Log($"Urchin x{spineCount}");
        
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, caster.Transform.position);
        }
        
        float angleStep = (spineCount > 1) ? (shootAngle / (spineCount - 1)) : 0f;
        float startAngle = -shootAngle / 2f;
        
        for (int i = 0; i < spineCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward) * caster.Transform.rotation;
            Vector3 direction = rotation * Vector3.right; //오른쪽으로 발사
            
            if (spinePrefab != null)
            {
                GameObject spine = Instantiate(
                    spinePrefab,
                    caster.Transform.position + (Vector3)direction * 0.5f,
                    rotation
                );
                
                UrchinSpine spineScript = spine.GetComponent<UrchinSpine>();
                if (spineScript != null)
                {
                    spineScript.Initialize(damagePerSpine, spineSpeed, spineLifetime, direction, enemyLayer);
                }
            }
        }
    }
}
