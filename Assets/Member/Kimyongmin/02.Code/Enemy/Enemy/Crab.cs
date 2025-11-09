using System.Collections;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class Crab : global::Enemy
    {
        [SerializeField] private GameObject stone;
        
        private CrabAnimation _crabAnimation;
        private AttackHitbox _attackHitbox;

        private readonly float _attackDelay = 1f;
        private float _currentDelay = 0f;

        [SerializeField] private Transform shotPos;

        [SerializeField] private float shotSpeed = 9f;

        protected override void Awake()
        {
            base.Awake();
            
            _crabAnimation = GetComponentInChildren<CrabAnimation>();
            _attackHitbox = GetComponentInChildren<AttackHitbox>();
            
        }

        private new void Update()
        {
            base.Update();
            
            if (CanAttack)
            {
                _currentDelay += Time.deltaTime;
                _crabAnimation.StoneSetActiveOn();
                _crabAnimation.StoneMove(0);
                if (_attackDelay < _currentDelay)
                {
                    Attack();
                    ResetCooltime();
                    _currentDelay = 0f;
                }
            }
        }
        
        private Vector2 targerDir = Vector2.zero;

        public override void Attack()
        {
            targerDir = GetTarget();
            _attackHitbox.ShowHitbox(GetTarget(),1.5f);
            _crabAnimation.PlayerAttackAnim();
            StartCoroutine(ShootStoneCor(1.5f));
        }

        public override void Death()
        {
            Destroy(gameObject);
        }

        private IEnumerator ShootStoneCor(float delay)
        {
            yield return new WaitForSeconds(delay);
            GameObject bullet = Instantiate(stone, shotPos.position, Quaternion.identity);
            bullet.GetComponent<Projectile>().Shoot(targerDir,shotSpeed);
        }

        
    }
}
