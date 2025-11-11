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

        private float _dashAnlge;

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
            _dashAnlge = Mathf.Atan2(_dashDir.y, _dashDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, _dashAnlge);
            StartCoroutine(HitPanJeong());
        }
        
        public void DashEnd()
        {
            AgentMovement.IsDashing = false;
            DisbleAttackRange();
            AgentMovement.RbCompo.linearVelocity = Vector2.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private new void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, attackVec);
        }

        public float PanjeongTime { get; set; }
        public float PanjeongDuration { get; set; } = 0.5f;


        private Collider2D[] _hitArr;
        public IEnumerator HitPanJeong()
        {
            while (PanjeongTime <= PanjeongDuration && IsAttack)
            {
                PanjeongTime += Time.deltaTime;
                _hitArr = Physics2D.OverlapBoxAll(transform.position, attackVec, _dashAnlge, layerMask);
                if (_hitArr.Length > 0)
                {
                    foreach (var item in _hitArr)
                    {
                        if (item.TryGetcomponentInParent(out Player player))
                        {
                            DealStamina(player, EnemyDataSo.damage);
                            IsAttack = false;
                            PanjeongTime = 0;
                            Array.Clear(_hitArr, 0, _hitArr.Length);
                            break;
                        }
                    }
                }
                yield return null;
            }
        }

        public void DealStamina(Player player, float damage)
        {
            player.GetDamage(damage);
        }
    }
}
