using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class PuffleFishProxy : MonoBehaviour
    {
        private PuffleFish _puffleFish;

        private void Awake()
        {
            _puffleFish = GetComponentInParent<PuffleFish>();
        }

        public void Explosion()
        {
            _puffleFish.Explo();
        }

        public void AttackEnd()
        {
            _puffleFish.IsAttack = false;
            _puffleFish.AgentMovemant.IsDashing = false;
            _puffleFish.DisbleAttackRange();
        }
    }
}
