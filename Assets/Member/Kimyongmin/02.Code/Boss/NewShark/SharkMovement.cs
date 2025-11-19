using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkMovement : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        private Vector3 _moveDir;
        private float _smooth = 4f;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }

        public void RbMove(float speed)
        {
            Vector2 currentDir = RbCompo.linearVelocity.normalized;
        
            if (RbCompo.linearVelocity.sqrMagnitude < 0.01f)
                currentDir = _moveDir;
            
            Vector2 newDir = Vector2.Lerp(currentDir, _moveDir, _smooth * Time.fixedDeltaTime).normalized;
            
            RbCompo.linearVelocity = newDir * speed;
        }

        public void SetMoveDir(Vector3 moveDir)
        {
            _moveDir = moveDir;
        }
    
    }
}
