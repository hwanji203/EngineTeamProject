using UnityEngine;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Agent;
using Member.Kimyongmin._02.Code.Enemy.SO;

public abstract class Enemy : MonoBehaviour
{
    [Header("에너미 설1정")] 
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private LayerMask playerMask;

    public Animator Animator { get;private set; }
    [field:SerializeField] public EnemyDataSo EnemyDataSo { get; private set; }
    private float _currentAttackTime;
    
    public AgentMovemant AgentMovemant { get; private set; }

    protected Transform Target;

    private float _normalAttackRange;

    protected virtual void  Awake()
    {
        AgentMovemant = GetComponent<AgentMovemant>();
        Animator = GetComponentInChildren<Animator>();

        _currentAttackTime = EnemyDataSo.attackDelay;

        _normalAttackRange = attackRange;
    }

    protected virtual void Start()
    { 
        Collider2D targetColl = Physics2D.OverlapCircle(transform.position, 519f, playerMask);
        if (targetColl != null)
            Target = targetColl.transform;
        
        AgentMovemant.SetSpeed(EnemyDataSo.moveSpeed, 0);
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
        return (Target.transform.position - transform.position).normalized;
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

    public bool CanAttack { get; private set; } = true;
    private void Update()
    {
        _currentAttackTime += Time.deltaTime;
        
        Debug.Log(CanAttack);
        
        if (_currentAttackTime > EnemyDataSo.attackDelay)
        {
            CanAttack = true;
            EnableAttackRange();
        }
    }

    public void ResetCooltime()
    {
        CanAttack = false;
        _currentAttackTime = 0;
    }

    public abstract void Attack();

    public void DisbleAttackRange()
    {
        attackRange = 0;
    }
    public void EnableAttackRange()
    {
        attackRange = _normalAttackRange;
    }
    
    public void ExpantionAttackRange()
    {
        attackRange = 999;
    }
}
