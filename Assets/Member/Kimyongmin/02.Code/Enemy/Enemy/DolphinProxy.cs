using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class DolphinProxy : MonoBehaviour
    {
        private Dolphin _dolphin;
        [SerializeField] private Animator effectAnimator;

        private void Start()
        {
            _dolphin = GetComponentInParent<Dolphin>();

            _dolphin.HealthSystem.OnHealthChanged += AttackEnd;
        }

        public void ProjectileAttack()
        {
            _dolphin.ShootProjectile();
            effectAnimator.gameObject.SetActive(true);
        }

        public void AttackEnd()
        {
            _dolphin.DisbleAttackRange();
            effectAnimator.gameObject.SetActive(false);
            _dolphin.IsAttack = false;
        }

        private void OnDestroy()
        {
            _dolphin.HealthSystem.OnHealthChanged -= AttackEnd;
        }
    }
}
