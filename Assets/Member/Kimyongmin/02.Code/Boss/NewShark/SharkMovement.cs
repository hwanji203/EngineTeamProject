using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkMovement : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }

        public void RbMove(Vector3 moveDir, float speed)
        {
            RbCompo.linearVelocity = moveDir * speed;
        }
    
    }
}
