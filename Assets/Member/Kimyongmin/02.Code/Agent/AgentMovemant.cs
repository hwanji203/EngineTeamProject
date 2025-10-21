using UnityEngine;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class AgentMovemant : MonoBehaviour
    {
        public Rigidbody2D RbCompo { get; private set; }

        private Vector2 _moveDir;
        private float _speed;
        private float _moveDelay;
        private float _currentMoveDelay;
        
        public bool IsDashing { get; set; } = false;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
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

        // private void Update()
        // {
        //     _currentMoveDelay += Time.deltaTime;
        //     if (_moveDelay < _currentMoveDelay)
        //     {
        //         _currentMoveDelay;
        //     }
        // }

        private float _smooth = 4;
        private Vector2 _targetVel;
    
        private void FixedUpdate()
        {
            Vector2 currentDir = RbCompo.linearVelocity.normalized;
        
            if (RbCompo.linearVelocity.sqrMagnitude < 0.01f)
                currentDir = _moveDir;
            
            Vector2 newDir = Vector2.Lerp(currentDir, _moveDir, _smooth * Time.fixedDeltaTime).normalized;

            if (!IsDashing)
                RbCompo.linearVelocity = newDir * _speed;
        }
    }
}
