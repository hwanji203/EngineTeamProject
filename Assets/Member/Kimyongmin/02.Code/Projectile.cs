using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code
{
    public enum BulletType
    {
        Linear,
        Rotate
    }
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private BulletType bulletType;

        [SerializeField] private float spinSpeed = 3f;
        [SerializeField] private float lifetime = 3f;
        private float _currentTime = 0;
        private Rigidbody2D _rb;

        private float _damage = 0;

        private bool _rotate = false;

        private void Awake()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector2 dir, float speed)
        {
            transform.right = dir;
            
            _rb.linearVelocity = transform.right.normalized * speed;
            if (bulletType == BulletType.Rotate)
            {
                _rotate = true;
            }
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (lifetime < _currentTime)
            {
                Destroy(gameObject);
            }
            if (_rotate)
                transform.Rotate(new Vector3(0,0,spinSpeed));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetcomponentInParent(out Player player))
            {
                player.GetDamage(_damage, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
