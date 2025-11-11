using System.Collections.Generic;
using UnityEngine;
 
namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class Jellyfish : global::Enemy
    {
        private static readonly int HitHash = UnityEngine.Animator.StringToHash("Hit");
        
        [SerializeField] private Transform head;
        
        private Vector3 _angle;
        private LineRenderer _lineRenderer;

        private Vector2 _lineEndPos;
        
        private List<Transform> _cognation = new List<Transform>();

        private LayerMask _layerMask;

        private AttackHitbox _attackHitbox;

        protected override void Awake()
        {
            base.Awake();
            _angle = transform.rotation.eulerAngles;
            _lineRenderer = GetComponentInChildren<LineRenderer>();

            _attackHitbox = GetComponentInChildren<AttackHitbox>();
            
            _layerMask = LayerMask.GetMask("Enemy");
        }

        public override void Attack()
        {
            
        }

        public override void Death()
        {
            Animator.SetBool(HitHash, true);
            _attackHitbox.ShowHitbox(Vector2.zero, 1.2f);
        }

        void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(_angle);
            
            SettingLine();
            Collider2D[] arr = Physics2D.OverlapCircleAll(head.position, 500f, LayerMask.GetMask("Enemy"));

            if (_cognation.Count == 0)
                _lineRenderer.enabled = false;
            else
                _lineRenderer.enabled = true;

            foreach (var item in arr)
            {
                if (item.CompareTag("Jellyfish") && item.transform != this.transform)
                {
                    if (!_cognation.Contains(item.transform))
                    {
                        _cognation.Add(item.transform);
                    }
                }
            }
        }

        private void SettingLine()
        {
            _cognation.RemoveAll(item => item == null);
            for (int i = 0; i < _cognation.Count; i++)
            {
                _lineRenderer.SetPosition(0, head.position);
                _lineRenderer.SetPosition(1, _cognation[i].position + new Vector3(0,0.5f,0));
            }
        }
    }
}
