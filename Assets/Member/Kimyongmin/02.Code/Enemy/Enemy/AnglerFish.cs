using System;
using System.Collections;
using Member.Kimyongmin._02.Code.Interface;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class AnglerFish : global::Enemy, IHitface
    {
        private float _dashAngle;
        [SerializeField] private Vector3 offset;
        
        public override void Attack()
        {
            IsAttack = true;
            AgentMovement.IsDashing = true;
            ResetCooltime();
            AgentMovement.RbCompo.linearVelocity = GetTarget() * 1.25f;
            _dashAngle = Mathf.Atan2(GetTarget().y, GetTarget().x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,_dashAngle);
            StartCoroutine(HitPanJeong());
            DisbleAttackRange();
        }

        public override void Death()
        {
            Destroy(gameObject);
        }
        
        public float PanjeongTime { get; set; }
        public float PanjeongDuration { get; set; } = 0.1f;

        private Collider2D[] _hitArr;

        public IEnumerator HitPanJeong()
        {
            yield return new WaitForSeconds(0.5f);
            while (PanjeongTime < PanjeongDuration && IsAttack)
            {
                PanjeongTime += Time.deltaTime;
                _hitArr = Physics2D.OverlapBoxAll(transform.position + offset, attackVec, _dashAngle, layerMask);
                if (_hitArr.Length > 0)
                {
                    foreach (var item in _hitArr)
                    {
                        if (item.TryGetcomponentInParent(out Player player))
                        {
                            DealStamina(player, EnemyDataSo.damage);
                            break;
                        }
                    }
                }   
                yield return null;
            }
            EnableAttackRange();
        }

        public void DealStamina(Player player, float damage)
        {
            player.GetDamage(damage, transform.position);
            IsAttack = false;
            AgentMovement.IsDashing = false;
            transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y,0);
            PanjeongTime = 0;
            Array.Clear(_hitArr, 0, _hitArr.Length);
        }
        
        private new void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position + offset, attackVec);
        }
    }
}
