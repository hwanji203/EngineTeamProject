using System;
using UnityEngine;

public class AgentMovemant : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private Vector2 _moveDir;
    private float _speed;
    private float _moveDelay;
    private float _currentMoveDelay;

    private float _drag = 5f;

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
        RbCompo.linearDamping = _drag;
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
    //         _currentMoveDelay
    //     }
    // }

    private void FixedUpdate()
    {
        
        RbCompo.linearVelocity = _moveDir * _speed;
    }
}
