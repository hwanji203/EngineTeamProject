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
        [SerializeField] private float attackRange;
        public SharkMovement SharkMovement { get; private set; }
        public SharkSkills SharkSkills { get; private set; }
        private HealthSystem _healthSystem;

        public LayerMask LayerMask { get; private set; }

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
            
            LayerMask = LayerMask.GetMask("Player");
            
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

        private void Update()
        {
            if (!IsAttack)
                FilpX(GetTargetDir().x);
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
            return Physics2D.OverlapCircle(transform.position, attackRange, LayerMask);
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
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        
        public void AttackBool(bool value)
        {
            IsAttack = value;
            if (value)
                SharkMovement.SetSpeed(0);
        }
        
    }
}
