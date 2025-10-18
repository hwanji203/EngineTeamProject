using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float dashDamping;
    [SerializeField] private float mousePosLimitsRadius;

    private Rigidbody2D rb;
    public PlayerStatSO StatSO { get; set; }
    public Vector2 MoveDir { get; private set; }
    public bool CanMove { get; private set; }
    public Vector2 MousePos { get; set; }

    private bool doMove;
    private Coroutine waitMoveCoroutine;
    private float defaultDamping;

    public event Action<PlayerState> OnMoveChange;

    public bool DoMove 
    {
        get
        {
            return doMove;
        }
        set
        {
            doMove = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        CanMove = true;
        DoMove = false;
    }
    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        UpdateZValue();
        if ((MousePos - (Vector2)transform.position).magnitude > mousePosLimitsRadius)
            RotateMove();
    }

    private void Move()
    {
        if (CanMove == false) return;

        Debug.Log(DoMove);

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
        rb.AddForce(MoveDir * Time.deltaTime * StatSO.acceleration, ForceMode2D.Force);
        rb.linearVelocity = rb.linearVelocity.normalized * Mathf.Clamp(rb.linearVelocity.magnitude, 0, StatSO.maxSpeed);
    }
    
    private void RotateMove()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(MoveDir.y, MoveDir.x)) * Mathf.Rad2Deg);
    }

    public void DashMove()
    {
        if (waitMoveCoroutine == null)
        {
            OnMoveChange?.Invoke(PlayerState.Dash);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(MoveDir * StatSO.dashPower, ForceMode2D.Force);
            rb.linearDamping = dashDamping;
            waitMoveCoroutine = StartCoroutine(DashCoroutine(StatSO.dashTime));
        }
    }

    private IEnumerator DashCoroutine(float waitTime)
    {
        CanMove = false;
        yield return new WaitForSeconds(waitTime);
        CanMove = true;
        rb.linearDamping = defaultDamping;
        waitMoveCoroutine = null;
    }

    private void UpdateZValue()
    {
        float targetRad = Mathf.Atan2(MousePos.y - transform.position.y, MousePos.x - transform.position.x);
        float mouseDeg = targetRad * Mathf.Rad2Deg;

        float targetDeg = Mathf.MoveTowardsAngle(transform.eulerAngles.z, mouseDeg, StatSO.rotateSpeed * Time.deltaTime);

        MoveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));
    }
}
