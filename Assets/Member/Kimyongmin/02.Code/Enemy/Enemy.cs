using System;
using UnityEngine;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Agent;
using Member.Kimyongmin._02.Code.Enemy;
using Member.Kimyongmin._02.Code.Enemy.SO;

public abstract class Enemy : MonoBehaviour, IAgentable
{
    [Header("에너미 설1정")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Vector2 attackVec;

    public Animator Animator { get; private set; }
    [field: SerializeField] public EnemyDataSo EnemyDataSo { get; private set; }
    private float _currentAttackTime;

    public HealthSystem HealthSystem { get; private set; }

    public AgentMovement AgentMovement { get; private set; }

    protected Transform Target;

    private float _normalAttackRange;
    public bool IsInvincibility { get; set; }

    public bool IsAttack { get; set; } = false;
    public event Action OnDead;

    private LayerMask _wallMask;
    private LayerMask _wallMaskTwo;

    protected virtual void Awake()
    {
        try
        {
            _currentAttackTime = EnemyDataSo.attackDelay - 1;
        }
        catch (NullReferenceException)
        {
            Debug.LogError($"{gameObject.name}이 친구 에너미 데이터 안 넣음!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        
        AgentMovement = GetComponent<AgentMovement>();
        Animator = GetComponentInChildren<Animator>();
        HealthSystem = GetComponent<HealthSystem>();
        HealthSystem.SetHealth(EnemyDataSo.hp);

        _normalAttackRange = attackRange;

        Collider2D targetColl = Physics2D.OverlapCircle(transform.position, 999f, layerMask);
        Debug.Log(targetColl);
        if (targetColl != null)
            Target = targetColl.transform;

        AgentMovement.SetSpeed(EnemyDataSo.moveSpeed, EnemyDataSo.detectDelay);

        HealthSystem.OnDeath += () => OnDead?.Invoke();
        
        _wallMask= LayerMask.NameToLayer("Wall");
        _wallMaskTwo = LayerMask.NameToLayer("EnemyWall");
    }

    protected virtual void Start()
    {
        HealthSystem.OnDeath += Death;
    }

    public void FilpX(float xDir)
    {
        float duration = 0;


        if (xDir > 0)
        {
            transform.DORotate(
                new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z), duration);
        }
        else if (xDir < 0)
        {
            transform.DORotate(
                new Vector3(transform.localRotation.eulerAngles.x, 180, transform.localRotation.eulerAngles.z),
                duration);
        }
    }

    public Vector2 GetTarget()
    {
        return (Target.transform.position - transform.position).normalized;
    }

    public bool AttackInPlayer()
    {
        if (EnemyDataSo.enemyType != EnemyType.NotAggressive)
        {
            return Physics2D.OverlapCircle(transform.position, attackRange, layerMask);
        }

        return false;
    }

    public bool ChaseInPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, chaseRange, layerMask);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public bool CanAttack { get; private set; } = true;
    public bool IsAttaking => IsAttack;

    protected void Update()
    {
        _currentAttackTime += Time.deltaTime;

        AgentMovement.GetKnockbackDir(-GetTarget());

        if (_currentAttackTime > EnemyDataSo.attackDelay)
        {
            CanAttack = true;
            EnableAttackRange();
        }

        if (HealthSystem.IsDead && !d)
        {
            d = true;
            GameManager.Instance.Player.RecoveryStamina(EnemyDataSo.deadStamina);
        }
    }

    private bool d = false;

    public void ResetCooltime()
    {
        CanAttack = false;
        _currentAttackTime = 0;
    }

    public abstract void Attack();

    public abstract void Death();

    public void DisbleAttackRange()
    {
        attackRange = 0;
    }

    public void EnableAttackRange()
    {
        attackRange = _normalAttackRange;
    }

    private void OnDestroy()
    {
        HealthSystem.OnDeath -= Death;
    }

    public void DeadGameobject()
    {
        Destroy(gameObject);
    }

    void IAgentable.CounterDamage(float damage)
    {
        HealthSystem.GetDamage(damage * 1.5f);
    }

    void IAgentable.DefaultDamage(float damage)
    {
        if (IsInvincibility == true)
        {
            return;
        }

        HealthSystem.GetDamage(damage);
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == _wallMask || other.gameObject.layer == _wallMaskTwo)
        {
            AgentMovement.SetMoveDir(Target.position - transform.position);
            FilpX((Target.position - transform.position).x);
        }
    }
}
