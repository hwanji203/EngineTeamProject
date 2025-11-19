using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class TurtleProxy : MonoBehaviour
    {
        private Turtle _turtle;
        [SerializeField] private GameObject animators;

        private void Awake()
        {
            _turtle = GetComponentInParent<Turtle>();
        }

        public void ProxyDash()
        {
            _turtle.Dash();
            animators.SetActive(true);
        }

        public void AttackEnd()
        {
            _turtle.DashEnd();
            animators.SetActive(false);
            _turtle.IsAttack = false;
        }
    }
}
