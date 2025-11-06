using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(TurtleBrain))]
    public class Turtle : global::Enemy
    {
        [Header("터틀 설정")]
        [SerializeField] private float dashPower = 10f;
        private AttackHitbox _attackHitbox;

        private Vector2 _dashDir = Vector2.right;

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
        }
        
        public void DashEnd()
        {
            AgentMovement.IsDashing = false;
            DisbleAttackRange();
            AgentMovement.RbCompo.linearVelocity = Vector2.zero;
        }
    }
}
