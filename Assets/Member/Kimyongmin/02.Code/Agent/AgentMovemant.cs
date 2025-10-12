using System;
using UnityEngine;

public class AgentMovemant : MonoBehaviour
{
    public Rigidbody2D RbCompo { get; private set; }

    private Vector2 _moveDir;
    private float _speed;

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }

    public void SetMoveDir(Vector2 moveDir)
    {
        _moveDir = moveDir;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    private void FixedUpdate()
    {
        RbCompo.linearVelocity = _moveDir * _speed;
    }
}
