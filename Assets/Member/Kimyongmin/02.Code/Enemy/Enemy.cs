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
        
        
    }

    public void FilpX(float xDir)
    {
        float angle = Mathf.Atan2(GetTarget().y,GetTarget().x) * Mathf.Rad2Deg;
        
        if (xDir > 0)
            transform.DORotate(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z), 0.2f);
        else if (xDir < 0)
            transform.DORotate(new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z), 0.2f);
        
        transform.eulerAngles = new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 
            angle);
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
