using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer : MonoBehaviour
{
    private Vector2 _moveDir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        _moveDir = value.Get<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir * 5;
    }
}
