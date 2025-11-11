using System;
using System.Collections;
using Member.Kimyongmin._02.Code.Interface;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(TurtleBrain))]
    public class Turtle : global::Enemy, IHitface
    {
        [Header("터틀 설정")]
        [SerializeField] private float dashPower = 10f;
        private AttackHitbox _attackHitbox;

        private Vector2 _dashDir = Vector2.right;

        [SerializeField] private Vector2 attackVec;

        protected override void Awake()
        {
            base.Awake();
            _attackHitbox = GetComponentInChildren<AttackHitbox>();
        }

        public override void Attack()
        {
            ResetCooltime();
            AgentMovement.IsDashing = true;
            IsAttack = true;
            _attackHitbox.ShowHitbox(GetTarget(),1f);
            _dashDir = (Target.position - transform.position).normalized;
        }

        public override void Death()
        {
            Destroy(gameObject);
        }

        public void Dash()
        {
            AgentMovement.RbCompo.linearVelocity = _dashDir * dashPower;
            StartCoroutine(HitPanJeong());
        }
        
        public void DashEnd()
        {
            AgentMovement.IsDashing = false;
            DisbleAttackRange();
            AgentMovement.RbCompo.linearVelocity = Vector2.zero;
        }

        private new void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, attackVec);
        }

        public float panjeongTime { get; set; } = 0;
        public float panjeongDuration { get; set; } = 0.5f;


        private Collider2D[] hitArr;
        public IEnumerator HitPanJeong()
        {
            while (panjeongTime < panjeongDuration)
            {
                panjeongTime += Time.deltaTime;
                hitArr = Physics2D.OverlapBoxAll(transform.position, attackVec, 0, layerMask);
                if (hitArr.Length > 0)
                {
                    foreach (var item in hitArr)
                    {
                        if (item.TryGetComponent<PlayerStamina>(out var playerStamina))
                        {
                            //playerStamina.LostStamina();
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
