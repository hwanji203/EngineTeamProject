using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSO movementSO;

    private Rigidbody2D rb;
    public Vector2 MousePos { get; set; }
    public bool DoMove { get; set; }
    public bool CanMove { get; set; }
    public Vector2 MoveDir { get; private set; }

    private Coroutine waitMoveCoroutine;

    public event Action<PlayerState> OnMoveChange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.linearDamping = movementSO.decreaseValue;
        rb.gravityScale = movementSO.gravityScale;

        CanMove = true;
        DoMove = false;
    }
    private void Update()
    {
        if (CanMove == false) return;

        Rotate();
        Move();
    }

    private void Rotate()
    {
        UpdateZValue();
        RotateMove();
    }

    private void Move()
    {
        if (DoMove == true)
        {
            //스테미나 없으면 canMove false때리고 return
            OnMoveChange?.Invoke(PlayerState.Move);
            GoDirectionMove();
            return;
        }

        OnMoveChange?.Invoke(PlayerState.Idle);
    }

    private void GoDirectionMove()
    {
        rb.AddForce(MoveDir * Time.deltaTime * movementSO.acceleration, ForceMode2D.Force);
        rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Clamp(rb.linearVelocity.magnitude, 0, movementSO.maxSpeed);
    }
    
    private void RotateMove()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(MoveDir.y, MoveDir.x)) * Mathf.Rad2Deg);
    }

    public void AttackMove(PlayerAttackType type)
    {
        switch (type)
        {
            case PlayerAttackType.Dash:
                if (waitMoveCoroutine == null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.AddForce(MoveDir * movementSO.dashPower, ForceMode2D.Force);
                    rb.linearDamping = movementSO.dashDamping;
                    waitMoveCoroutine = StartCoroutine(MoveCoroutine(movementSO.dashTime));
                }
                break;
            case PlayerAttackType.Default:
                if (waitMoveCoroutine == null)
                {
                    transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 360), movementSO.attackTime, RotateMode.FastBeyond360).SetEase(Ease.OutCirc);
                    waitMoveCoroutine = StartCoroutine(MoveCoroutine(movementSO.attackTime));
                }
                break;
        }
    }

    private IEnumerator MoveCoroutine(float waitTime)
    {
        rb.gravityScale = 0;
        yield return new WaitForSeconds(waitTime);
        rb.gravityScale = movementSO.gravityScale;
        CanMove = true;
        rb.linearDamping = movementSO.dashDamping;
        waitMoveCoroutine = null;
    }

    private void UpdateZValue()
    {
        if ((MousePos - (Vector2)transform.position).magnitude < movementSO.limitPos) return;

        float targetRad = Mathf.Atan2(MousePos.y - transform.position.y, MousePos.x - transform.position.x);
        float mouseDeg = targetRad * Mathf.Rad2Deg;

        float targetDeg = Mathf.MoveTowardsAngle(transform.eulerAngles.z, mouseDeg, movementSO.rotateSpeed * Time.deltaTime);

        MoveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));
    }
}
