using System;
using UnityEngine;

public class AgentMovemant : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private Vector2 _moveDir;
    private float _speed;
    private float _moveDelay;
    private float _currentMoveDelay;

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }

    public void SetMoveDir(Vector2 moveDir)
    {
        _moveDir = moveDir;
    }

    public void SetStat(float speed, float moveDelay)
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

        RbCompo.linearVelocity = newDir * _speed;
    }
}
