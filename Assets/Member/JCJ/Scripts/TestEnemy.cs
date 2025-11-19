using System;
using UnityEngine;
using System.Collections;
using Member.Kimyongmin._02.Code.Agent;

public class TestEnemy : MonoBehaviour, IDamageable, ISlowable
{
    public static event Action OnEnemyDied;
    [Header("체력")]
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float currentHealth;
    
    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"[{gameObject.name}] TestEnemy Spawned - Hp : {maxHealth}");
    }
    
    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        GetDamage(0f);
    }

    public void ApplySlow(float slowPercent, float duration)
    {
        StartCoroutine(SlowCoroutine(slowPercent, duration));
    }
    
    private IEnumerator SlowCoroutine(float slowPercent, float duration)
    {
        Debug.Log($"[{gameObject.name}] Slowed {slowPercent * 100}%");
        yield return new WaitForSeconds(duration);
        Debug.Log($"[{gameObject.name}] Unslowed");
    }
    
    private void Die()
    {
        Debug.Log($"[{gameObject.name}] Enemy Is Dead");
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}
