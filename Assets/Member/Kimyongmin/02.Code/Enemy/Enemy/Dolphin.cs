using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class Dolphin : global::Enemy
    {
        private AttackHitbox _attackHitbox;
        
        [SerializeField] private Transform[] hitboxes = new Transform[3];
        
        [Header("돌핀 설정")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private float bulletSpeed = 20f;
        [SerializeField] private float shotAngle = 15f;
        
        private Vector2 _target = Vector2.right;
        protected override void Awake()
        {
            base.Awake();
            _attackHitbox = GetComponentInChildren<AttackHitbox>();

            hitboxes[0].eulerAngles = new Vector3(0, 0, 0);
            hitboxes[1].eulerAngles = new Vector3(0, 0, shotAngle);
            hitboxes[2].eulerAngles = new Vector3(0, 0, -shotAngle);
        }

        public override void Attack()
        {
            _target = GetTarget();
            
            _attackHitbox.ShowHitbox(_target,1f);
            ExpantionAttackRange();
        }

        public void ShootProjectile()
        {
            for (int i = -1; i < 2; i++)
            {
                Projectile bullet = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,shotAngle * i))
                    .GetComponent<Projectile>();
                
                bullet.Shoot(Quaternion.AngleAxis(shotAngle * i, transform.position) * _target, bulletSpeed);
            }
        }
    }
}
