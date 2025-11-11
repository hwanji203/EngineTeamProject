using UnityEngine;
using System.Collections;
using Member.Kimyongmin._02.Code.Agent;

/// 테스트용 적 (기존 IDamageable 사용)
public class TestEnemy : MonoBehaviour, IDamageable, ISlowable
{
    [Header("체력")]
    [SerializeField] private float maxHealth = 30f;
    private float currentHealth;
    
    [Header("상태")]
    private float currentSpeedMultiplier = 1f;
    
    [Header("이펙트")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private AudioClip hitSound;
    
    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[{gameObject.name}] TestEnemy 생성됨 - 체력: {maxHealth}");
    }
    
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"[{gameObject.name}] 데미지: {damage}, 남은 체력: {currentHealth}/{maxHealth}");
        
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
    
    public void ApplySlow(float slowPercent, float duration)
    {
        StartCoroutine(SlowCoroutine(slowPercent, duration));
    }
    
    private IEnumerator SlowCoroutine(float slowPercent, float duration)
    {
        Debug.Log($"[{gameObject.name}] 슬로우 적용! {slowPercent * 100}%");
        currentSpeedMultiplier = slowPercent;
        yield return new WaitForSeconds(duration);
        currentSpeedMultiplier = 1f;
        Debug.Log($"[{gameObject.name}] 슬로우 해제!");
    }
    
    private void Die()
    {
        Debug.Log($"[{gameObject.name}] 테스트 적 사망!");
        Destroy(gameObject);
    }
    
    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetSpeedMultiplier() => currentSpeedMultiplier;
}
