using System;
using UnityEngine;

public abstract class Enemy : HealthSystem
{
    [Header("에너미 설1정")] 
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private LayerMask playerMask;
    [field:SerializeField] public EnemyData EnemyData { get; private set; }
    
    public AgentMovemant AgentMovemant { get; private set; }

    private Transform _target;

    private void Awake()
    {
        AgentMovemant = GetComponent<AgentMovemant>();
    }

    private void Start()
    { 
        Collider2D targetColl = Physics2D.OverlapCircle(transform.position, 519f, playerMask);
        if (targetColl != null)
            _target = targetColl.transform;
    }

    public void FilpX(float xDir)
    {
        if (xDir > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (xDir < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public Vector2 GetTarget()
    {
        return (_target.transform.position - transform.position).normalized;
    }

    public bool AttackInPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, attackRange, playerMask);
    }

    public bool ChaseInPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, chaseRange, playerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
