using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class DolphinProxy : MonoBehaviour
    {
        private Dolphin _dolphin;

        private void Awake()
        {
            _dolphin = GetComponentInParent<Dolphin>();
        }

        public void ProjectileAttack()
        {
            _dolphin.ShootProjectile();
        }

        public void AttackEnd()
        {
            _dolphin.DisbleAttackRange();
            _dolphin.IsAttack = false;
        }
    }
}
