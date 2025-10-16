using UnityEngine;
using DG.Tweening;

public abstract class Enemy : HealthSystem
{
    [Header("에너미 설1정")] 
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private LayerMask playerMask;
    [field:SerializeField] public EnemyDataSO EnemyDataSo { get; private set; }
    
    public AgentMovemant AgentMovemant { get; private set; }

    private Transform _target;

    protected virtual void  Awake()
    {
        AgentMovemant = GetComponent<AgentMovemant>();
    }

    private void Start()
    { 
        Collider2D targetColl = Physics2D.OverlapCircle(transform.position, 519f, playerMask);
        if (targetColl != null)
            _target = targetColl.transform;
        
        AgentMovemant.SetStat(EnemyDataSo.moveSpeed, 0);
    }
    
    public void FilpX(float xDir)
    {
        float duration = 1f / EnemyDataSo.moveSpeed;
        

        if (xDir > 0)
        {
            transform.DORotate(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z), duration);
        }
        else if (xDir < 0)
        {
            transform.DORotate(new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z), duration);
        }
    }

    public Vector2 GetTarget()
    {
        return (_target.transform.position - transform.position).normalized;
    }

    public bool AttackInPlayer()
    {
        if (EnemyDataSo.EnemyType != EnemyType.NotAggressive)
            return Physics2D.OverlapCircle(transform.position, attackRange, playerMask);

        return false;
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
