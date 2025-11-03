using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;

[CreateAssetMenu(fileName = "Skill_OctopusInk2D", menuName = "Skills/Octopus Ink 2D")]
public class OctopusInkSkill : SkillSO
{
    [Header("스킬 설정")]
    [SerializeField] private float explosionRadius = 6f;
    [SerializeField] private float damageInExplosion = 40f;
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("상태 이상")]
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float slowPercent = 0.5f;
    
    [Header("이펙트")]
    [SerializeField] private AudioClip inkSound;
    
    public override void Execute(ISkillCaster caster)
    {
        caster.ConsumeMana(ManaCost);
        Debug.Log($"먹물 뿜기!");
        
        if (inkSound != null)
        {
            AudioSource.PlayClipAtPoint(inkSound, caster.Transform.position);
        }
        
        Vector2 explosionPos = caster.Transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            explosionPos,
            explosionRadius,
            enemyLayer
        );
        
        foreach (Collider2D hit in hits)
        {
            // 데미지
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(damageInExplosion);
                Debug.Log($"[{hit.name}] {damageInExplosion} 데미지!");
            }
            
            // 슬로우
            ISlowable slowable = hit.GetComponent<ISlowable>();
            if (slowable != null)
            {
                slowable.ApplySlow(slowPercent, slowDuration);
            }
        }
    }
}