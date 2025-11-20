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
        [field:SerializeField] public SharkDataSO SharkData { get; private set; }
        [SerializeField] private Transform target;
        [Header("상어 설정")]
        [SerializeField] private Vector2 attackRange;
        public SharkMovement SharkMovement { get; private set; }
        public SharkSkills SharkSkills { get; private set; }
        private HealthSystem _healthSystem;

        [field:SerializeField] public LayerMask LayerMask { get; private set; }

        public int ChargeStack { get; private set; }

        public float SkillCooltime { get; private set; }
        public float CurrentCooltime { get; private set; }
        
        public bool IsAttack { get; private set; } = false;
        
        public Animator Animator { get; private set; }

        private void Awake()
        {
            SharkMovement = GetComponent<SharkMovement>();
            _healthSystem = GetComponent<HealthSystem>();
            SharkSkills = GetComponent<SharkSkills>();
            Animator = GetComponentInChildren<Animator>();
                
            _healthSystem.SetHealth(SharkData.Hp);
            
            ResetCooltime();
        }

        private void FixedUpdate()
        {
            if (!IsAttack)
            {
                SharkMovement.SetSpeed(SharkData.Speed);
            }
            SharkMovement.RbMove();
            
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
            Collider2D[] arr = Physics2D.OverlapBoxAll(transform.position, attackRange, LayerMask);

            if (arr.Length > 1)
                return true;
            
            return false;
        }

        public void Charging()
        {
            ChargeStack++;
        }

        public void ResetCooltime()
        {
            CurrentCooltime = 0;
            SkillCooltime = Random.Range(SharkData.MinSkillCool, SharkData.MaxSkillCool);
        }

        public void SkillTick()
        {
            CurrentCooltime += Time.deltaTime;
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
                SharkMovement.SetSpeed(0);
            else
                SharkMovement.SetSpeed(SharkData.Speed);
        }

        public bool IsAttaking { get; }
        void IAgentable.CounterDamage(float damage)
        {
            _healthSystem.GetDamage(damage * 1.5f);
        }

        void IAgentable.DefaultDamage(float damage)
        {
            _healthSystem.GetDamage(damage);
        }
    }
}
