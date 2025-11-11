using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private float explosionRadius;
    private float lifetime;
    private Rigidbody rb;
    private bool hasHit = false;
    
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject impactEffectPrefab;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Initialize(float dmg, float spd, float radius, float lifeTime = 5f)
    {
        damage = dmg;
        speed = spd;
        explosionRadius = radius;
        lifetime = lifeTime;
        
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            HandleHit(other.transform.position);
        }
    }
    
    private void HandleHit(Vector3 hitPos)
    {
        hasHit = true;
        
        if (impactEffectPrefab != null)
        {
            Instantiate(impactEffectPrefab, hitPos, Quaternion.identity);
        }
        
        if (explosionRadius > 0)
        {
            Collider[] hits = Physics.OverlapSphere(hitPos, explosionRadius, enemyLayer);
            foreach (Collider hit in hits)
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.GetDamage(damage);
                }
            }
        }
        
        Destroy(gameObject);
    }
}