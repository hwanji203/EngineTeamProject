using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(DolphinBrain))]
    public class Dolphin : global::Enemy
    {
        private AttackHitbox _attackHitbox;
        
        [SerializeField] private Transform[] hitboxes = new Transform[3];
        
        [Header("돌핀 설정")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private float bulletSpeed = 20f;
        [SerializeField] private float shotAngle = 15f;
        
        private Vector3 _target = Vector3.forward;
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
            ResetCooltime();
            IsAttack = true;
            
            _target = GetTarget();
            
            _attackHitbox.ShowHitbox(_target,1f);
        }

        public void ShootProjectile()
        {
            for (int i = -1; i < 2; i++)
            {
                Projectile bullet = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,shotAngle * i))
                    .GetComponent<Projectile>();

                Vector2 dir = Quaternion.AngleAxis(shotAngle * i, Vector3.forward) * _target;
                
                bullet.Shoot(dir.normalized, bulletSpeed);
            }
        }
    }
}
