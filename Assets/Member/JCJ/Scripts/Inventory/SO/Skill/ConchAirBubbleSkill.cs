using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;

[CreateAssetMenu(fileName = "Skill_ConchBubble2D", menuName = "Skills/Conch Air Bubble 2D")]
public class ConchAirBubbleSkill : SkillSO
{
    [Header("스킬 설정")]
    [SerializeField] private float pullRadius = 8f;
    [SerializeField] private float pullSpeed = 8f;
    [SerializeField] private float damageOnHit = 20f;
    [SerializeField] private LayerMask enemyLayer;
    
    [Header("이펙트")]
    [SerializeField] private AudioClip pullSound;
    
    public override void Execute(ISkillCaster caster)
    {
        caster.ConsumeMana(ManaCost);
        Debug.Log($"공기 방울 끌어당기기!");
        
        if (pullSound != null)
        {
            AudioSource.PlayClipAtPoint(pullSound, caster.Transform.position);
        }
        
        Vector2 playerPos = caster.Transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(playerPos, pullRadius, enemyLayer);
        
        foreach (Collider2D hit in hits)
        {
            // 데미지
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(damageOnHit);
            }
            
            PullableEnemy pullable = hit.GetComponent<PullableEnemy>();
            if (pullable != null)
            {
                pullable.PullTowards(playerPos, pullSpeed);
            }
        }
    }
}