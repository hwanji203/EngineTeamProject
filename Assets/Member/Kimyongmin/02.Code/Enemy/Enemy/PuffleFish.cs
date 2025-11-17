using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(PuffleBrain))]
    public class PuffleFish : global::Enemy
    {
        [Header("복어 설정")] 
        [SerializeField] private float exploDelay = 0.5f;
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
            _hitboxCompo.ShowHitbox(exploDelay,exploRadius);
            AgentMovement.RbCompo.linearVelocity = Vector2.zero;
        }

        public override void Death()
        {
            
        }

        public void Explo()
        {
            Sequence s = DOTween.Sequence();
            
            s.Append(transform.DOScale(exploRadius, 0.125f).SetEase(Ease.InOutElastic));
            s.Append(transform.DOScale(1,0.75f).SetEase(Ease.InOutQuad));
        }
    }
}
