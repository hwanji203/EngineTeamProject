using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;

        public void Shoot(Vector2 dir, float speed)
        {
            rb.linearVelocity = dir.normalized * speed;
        }
    }
}
