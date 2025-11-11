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
    [SerializeField] private float pullDuration = 0.5f;      // 끌려가는 시간
    
    [Header("이펙트")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip hitSound;
    
    private Rigidbody2D rb;
    private Vector2 targetPos;
    private bool isPulling = false;
    
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
                Debug.Log($"[{gameObject.name}] 끌어당기기 완료!");
            }
        }
    }
    
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"[{gameObject.name}] 데미지: {damage}, 남은 체력: {currentHealth}");
        
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
        
        Debug.Log($"[{gameObject.name}] 끌려감! 목표: {targetPosition}");
    }
    
    public void ApplySlow(float slowPercent, float duration)
    {
        StartCoroutine(SlowCoroutine(slowPercent, duration));
    }
    
    private IEnumerator SlowCoroutine(float slowPercent, float duration)
    {
        Debug.Log($"[{gameObject.name}] 슬로우!");
        yield return new WaitForSeconds(duration);
    }
    
    private void Die()
    {
        Debug.Log($"[{gameObject.name}] 사망!");
        Destroy(gameObject);
    }
}