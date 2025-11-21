using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkMovement : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        private Vector3 _moveDir;
        private float _smooth = 4f;

        private float _speed;

        private bool _isDashing = false;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }

        public void RbMove()
        {
            Vector2 currentDir = RbCompo.linearVelocity.normalized;
        
            if (RbCompo.linearVelocity.sqrMagnitude < 0.01f)
                currentDir = _moveDir;
            
            Vector2 newDir = Vector2.Lerp(currentDir, _moveDir, _smooth * Time.fixedDeltaTime).normalized;
            
            if (!_isDashing)
                RbCompo.linearVelocity = newDir * _speed;
        }

        public void SetMoveDir(Vector3 moveDir)
        {
            _moveDir = moveDir;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void ShortDash(Vector3 dir , float power)
        {
            RbCompo.linearVelocity = dir * power;
            SetDashing(true);
        }

        public void SetDashing(bool isDashing)
        {
            _isDashing = isDashing;
        }
    }
}
