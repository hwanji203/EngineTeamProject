using System.Collections;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class AgentMovement : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }
        
        private HealthSystem _healthSystem;

        private Vector2 _moveDir;
        private float _speed;
        private float _moveDelay;
        private float _currentMoveDelay;

        public bool IsDashing { get; set; } = false;
        public bool IsHit { get; set; } = false;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
            _healthSystem = GetComponent<HealthSystem>();

            _healthSystem.OnHealthChanged += Knockback;
            _healthSystem.OnDeath += KnockbackTwo;
        }

        public void SetMoveDir(Vector2 moveDir)
        {
            _moveDir = moveDir;
        }

        public void SetSpeed(float speed, float moveDelay)
        {
            _speed = speed;
            _moveDelay = moveDelay;
        }

        private float _smooth = 4;
        private Vector2 _targetVel;
        private float _fallSpeed = 1;
    
        private void FixedUpdate()
        {
            Vector2 currentDir = RbCompo.linearVelocity.normalized;
        
            if (RbCompo.linearVelocity.sqrMagnitude < 0.01f)
                currentDir = _moveDir;
            
            Vector2 newDir = Vector2.Lerp(currentDir, _moveDir, _smooth * Time.fixedDeltaTime).normalized;

            if (!IsDashing && !IsHit && !_healthSystem.IsDead)
            {
                RbCompo.linearVelocity = newDir * _speed;
            }

            if (_healthSystem.IsDead)
            {
                _fallSpeed += Time.deltaTime;
                RbCompo.gravityScale = _fallSpeed;
            }
        }

        private Vector2 _knockDir = Vector2.left;
        public void GetKnockbackDir(Vector2 knockDir)
        {
            _knockDir = knockDir;
        }

        private float _currentTime = 0;
        private float _knockPower = 5;

        private void Knockback()
        {
            StartCoroutine(KnockbackCor(5));
        }
        
        private void KnockbackTwo()
        {
            StartCoroutine(KnockbackCor(25));
        }
        private IEnumerator KnockbackCor(float a)
        {
            RbCompo.linearVelocity = Vector2.zero;
            _currentTime = 0;
            _knockPower = 0;
            
            while (_currentTime < 1)
            {
                _currentTime += Time.deltaTime;
                
                _knockPower = Mathf.Lerp(a, 0, _currentTime);
                RbCompo.linearVelocity = _knockDir * _knockPower;
                yield return null;
            }
        }

        private void OnDestroy()
        {
            _healthSystem.OnHealthChanged -= Knockback;
            _healthSystem.OnDeath -= KnockbackTwo;
        }
    }
}
