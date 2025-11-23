using System;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Agent;
using Member.Kimyongmin._02.Code.Boss.SO;
using Member.Kimyongmin._02.Code.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class Shark : MonoBehaviour, IAgentable
    {
        [field:SerializeField] public SharkDataSo SharkData { get; private set; }
        [SerializeField] private Transform target;
        [Header("상어 설정")]
        [SerializeField] private Vector2 attackRange;
        [SerializeField] private SharkLaserD sharkLaserD;
        
        public SharkMovement SharkMovement { get; private set; }
        public SharkAttacks SharkAttacks { get; private set; }
        public HealthSystem Healthsystem { get; private set; }

        [field:SerializeField] public LayerMask LayerMask { get; private set; }

        public int ChargeStack { get; private set; }

        public float SkillCooltime { get; private set; }
        public float CurrentCooltime { get; private set; }
        public float CurrentAttackCool {get; private set; }

        public bool IsAttack { get; private set; } = false;
        
        public Animator Animator { get; private set; }
        
        private readonly int _hitHash = Animator.StringToHash("Hit");

        private LayerMask _wallMask;

        private void Awake()
        {
            SharkMovement = GetComponent<SharkMovement>();
            Healthsystem = GetComponent<HealthSystem>();
            SharkAttacks = GetComponent<SharkAttacks>();
            Animator = GetComponentInChildren<Animator>();
            
            _wallMask = LayerMask.NameToLayer("Wall");
        }

        private void Start()
        {
            Healthsystem.SetHealth(SharkData.Hp);
            sharkLaserD.LaserSetting(SharkData.LaserTickDamage, transform.position);

            Healthsystem.OnHealthChanged += HitAnim;
            Healthsystem.OnHealthChanged += SharkMovement.ZeroVelocity;
            ResetCooltime();
            
            Healthsystem.SetInvincibility(true);
        }

        private void FixedUpdate()
        {
            if (!IsAttack)
            {
                SharkMovement.SetSpeed(SharkData.Speed);
                SharkMovement.RbMove();
            }
            
        }

        public Vector3 GetTargetDir()
        {
            return (target.position - transform.position).normalized;
        }
        
        public void FilpX(float xDir)
        {
            float duration = 1f / SharkData.Speed;


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

        public bool AttackInPlayer()
        {
            Collider2D[] arr = Physics2D.OverlapBoxAll(transform.position, attackRange,0, LayerMask);

            if (arr.Length > 0)
                return true;
            
            return false;
        }

        public void Charging()
        {
            ChargeStack++;
        }
        
        public void ResetCharging()
        {
            ChargeStack = 0;
        }


        public void ResetCooltime()
        {
            CurrentCooltime = 0;
            SkillCooltime = Random.Range(SharkData.MinSkillCool, SharkData.MaxSkillCool);
        }

        public void ResetAttackCooltime()
        {
            CurrentAttackCool = 0;
        }

        public void SkillCoolPlus(float value)
        {
            CurrentCooltime += value;
        }
        public void CooltimeTick()
        {
            CurrentCooltime += Time.deltaTime;
            CurrentAttackCool += Time.deltaTime;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, attackRange);
        }
        
        public void AttackBool(bool value)
        {
            IsAttack = value;
            if (value)
            {
                SharkMovement.SetSpeed(0);
                SharkMovement.RbMove();
            }
                
            else
                SharkMovement.SetSpeed(SharkData.Speed);
        }

        public bool IsAttaking { get; }

        public bool IsInvincibility { get; private set; }

        void IAgentable.CounterDamage(float damage)
        {
            Healthsystem.GetDamage(damage * 1.5f);
        }

        void IAgentable.DefaultDamage(float damage)
        {
            Healthsystem.GetDamage(damage);
        }

        public Action OnWallBurt;

        public bool Charge {get; private set;}
        public void ChargeBool(bool value)
        {
             Charge = value;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Charge)
            {
                if (other.TryGetcomponentInParent(out Player player))
                {
                    player.GetDamage(SharkData.ChargeDamage, transform.position);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (Charge)
            {
                if (other.gameObject.layer == _wallMask)
                {
                    OnWallBurt?.Invoke();
                    CameraShaker.Instance.RandomShake(100);
                }
            }
        }
        
        public float RoarDir { get; set; }

        private void HitAnim()
        {
            Animator.SetTrigger(_hitHash);
        }

        private void OnDestroy()
        {
            Healthsystem.OnHealthChanged -= HitAnim;
            Healthsystem.OnHealthChanged -= SharkMovement.ZeroVelocity;
        }
    }
}
