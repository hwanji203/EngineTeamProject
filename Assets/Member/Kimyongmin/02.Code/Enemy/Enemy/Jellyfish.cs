using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class Jellyfish : global::Enemy
    {
        private Vector3 _angle;

        protected override void Awake()
        {
            base.Awake();
            _angle = transform.rotation.eulerAngles;
        }

        void LateUpdate()
        {
            
        }

        public override void Attack()
        {
            
        }
    }
}
