using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(TurtleBrain))]
    public class Turtle : global::Enemy
    {
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
            AgentMovemant.IsDashing = true;
            StartCoroutine(_attackHitbox.ShowHitbox(Target.position));
            Debug.Log("dd");
            
            _dashDir = (Target.position - transform.position).normalized;
        }

        public void Dash()
        {
            
            AgentMovemant.RbCompo.linearVelocity = _dashDir * dashPower;
            ResetCooltime();
        }
        
        public void DashEnd()
        {
            AgentMovemant.IsDashing = false;
            DisbleAttackRange();
            AgentMovemant.RbCompo.linearVelocity = Vector2.zero;
        }
    }
}
