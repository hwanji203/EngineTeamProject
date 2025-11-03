using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class TurtleProxy : MonoBehaviour
    {
        private Turtle _turtle;

        private void Awake()
        {
            _turtle = GetComponentInParent<Turtle>();
        }

        public void ProxyDash()
        {
            _turtle.Dash();
        }

        public void AttackEnd()
        {
            _turtle.DashEnd();
            _turtle.IsAttack = false;
        }
    }
}
