using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float lifetime = 3f;
        private float _currentTime = 0;

        public void Shoot(Vector2 dir, float speed)
        {
            transform.right = dir;
            
            rb.linearVelocity = transform.right.normalized * speed;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (lifetime < _currentTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
