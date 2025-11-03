
using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;

public class UrchinSpine : MonoBehaviour
{
    private float damage;
    private float speed;
    private float lifetime;
    private Vector3 direction;
    private LayerMask enemyLayer;
    private Rigidbody2D rb;
    private bool hasHit = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    
    public void Initialize(float dmg, float spd, float lifeTime, Vector3 dir, LayerMask layer)
    {
        damage = dmg;
        speed = spd;
        lifetime = lifeTime;
        direction = dir.normalized;
        enemyLayer = layer;
        
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        
        Destroy(gameObject, lifetime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;
        
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            hasHit = true;
            
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.GetDamage(damage);
                Debug.Log($"가시가 [{other.name}]에 {damage} 데미지!");
            }
            
            Destroy(gameObject);
        }
    }
}