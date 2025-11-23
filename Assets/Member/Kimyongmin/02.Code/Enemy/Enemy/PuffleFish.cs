using System;
using System.Collections;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Interface;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(PuffleBrain))]
    public class PuffleFish : global::Enemy, IHitface
    {
        [Header("복어 설정")] [SerializeField] private float exploDelay = 0.5f;
        [SerializeField] private float exploRadius = 5f;

        private Vector3 _dashDir = Vector3.right;
        private AttackHitbox _hitboxCompo;

        protected override void Awake()
        {
            base.Awake();
            _hitboxCompo = GetComponentInChildren<AttackHitbox>();
        }

        public override void Attack()
        {
            ResetCooltime();
            IsAttack = true;
            AgentMovement.IsDashing = true;
            _dashDir = Target.position;
            _hitboxCompo.ShowHitbox(exploDelay, exploRadius);
            AgentMovement.RbCompo.linearVelocity = Vector2.zero;
        }

        public override void Death()
        {
            VFXManager.Instance.Play(VFXType.EnemyDead,transform.position,Quaternion.identity, transform.parent);
        }

        public void Explo()
        {
            Sequence s = DOTween.Sequence();

            StartCoroutine(HitPanJeong());
            s.Append(transform.DOScale(exploRadius, 0.125f).SetEase(Ease.InOutElastic));
            s.Append(transform.DOScale(1, 0.75f).SetEase(Ease.InOutQuad));
        }

        public float PanjeongTime { get; set; }
        public float PanjeongDuration { get; set; } = 0.1f;

        private Collider2D[] _hitArr;

        public IEnumerator HitPanJeong()
        {
            while (PanjeongTime <= PanjeongDuration && IsAttack)
            {
                PanjeongTime += Time.deltaTime;
                _hitArr = Physics2D.OverlapCircleAll(transform.position, exploRadius , layerMask);
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
        }

        public void DealStamina(Player player, float damage)
        {
            player.GetDamage(damage, transform.position);
            IsAttack = false;
            PanjeongTime = 0;
            Array.Clear(_hitArr, 0, _hitArr.Length);
        }
    }
}