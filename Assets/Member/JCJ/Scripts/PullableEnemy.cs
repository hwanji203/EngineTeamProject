using System.Collections;
using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;

public class PullableEnemy : MonoBehaviour, IDamageable, ISlowable
{
    [Header("체력")]
    [SerializeField] private float maxHealth = 30f;
    private float currentHealth;
    
    [Header("끌려갈 때")]
    [SerializeField] private float pullStopDistance = 1.5f;  // 이 거리 내로 오면 멈춤
    
    [Header("이펙트")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip hitSound;
    
    private Rigidbody2D rb;
    private Vector2 targetPos;
    private bool isPulling;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        currentHealth = maxHealth;
    }
    
    private void FixedUpdate()
    {
        // 끌려가는 중이면 거리 체크
        if (isPulling && rb != null)
        {
            float distance = Vector2.Distance(rb.position, targetPos);
            
            if (distance < pullStopDistance)
            {
                // 가까워지면 멈춤
                rb.linearVelocity = Vector2.zero;
                isPulling = false;
                Debug.Log($"[{gameObject.name}] pull");
            }
        }
    }
    
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"[{gameObject.name}] damage : {damage}, current Health : {currentHealth}");
        
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }
        
        if (hitSound != null)
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    // 끌려가기
    public void PullTowards(Vector2 targetPosition, float speed)
    {
        if (rb == null || rb.bodyType != RigidbodyType2D.Dynamic) return;
        
        targetPos = targetPosition;
        isPulling = true;
        
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.linearVelocity = direction * speed;
        
        Debug.Log($"[{gameObject.name}] pull, target : {targetPosition}");
    }
    
    public void ApplySlow(float slowPercent, float duration)
    {
        StartCoroutine(SlowCoroutine(duration));
    }
    
    private IEnumerator SlowCoroutine(float duration)
    {
        Debug.Log($"[{gameObject.name}] slowed");
        yield return new WaitForSeconds(duration);
    }
    
    private void Die()
    {
        Debug.Log($"[{gameObject.name}] Dead");
        Destroy(gameObject);
    }
}