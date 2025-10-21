using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(TurtleBrain))]
    public class Turtle : global::Enemy
    {
        [SerializeField] private float dashPower = 10f;
        

        public override void Attack()
        {
            AgentMovemant.IsDashing = true;
        }

        public void Dash()
        {
            AgentMovemant.RbCompo.linearVelocity = GetTarget() * dashPower;
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
