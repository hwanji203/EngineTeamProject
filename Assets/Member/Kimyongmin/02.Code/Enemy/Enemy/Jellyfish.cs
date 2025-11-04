using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class Jellyfish : global::Enemy
    {
        [SerializeField] private Transform head;
        
        private Vector3 _angle;
        private LineRenderer _lineRenderer;

        private Vector2 _lineEndPos;

        protected override void Awake()
        {
            base.Awake();
            _angle = transform.rotation.eulerAngles;
            _lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        void LateUpdate()
        {
            
        }

        public override void Attack()
        {
            _lineRenderer.SetPosition(0, head.position);
            _lineRenderer.SetPosition(1, _lineEndPos);
        }

        public void LineSetting(Vector2 pos)
        {
            _lineEndPos = pos;
        }
    }
}
