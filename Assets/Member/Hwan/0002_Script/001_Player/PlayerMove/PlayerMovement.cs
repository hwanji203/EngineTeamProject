using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public PlayerStatSO StatSO { get; set; }

    private Vector2 moveDir;
    private float mouseDeg;
    public bool CanMove { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        UpdateZValue();

        RotateMove();
        if (CanMove == true) { GoDirectionMove(); }
    }

    private void GoDirectionMove()
    {
        rb.AddForce(moveDir * Time.fixedDeltaTime * StatSO.moveSpeed, ForceMode2D.Force);
    }
    private void RotateMove()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg));
    }

    private void UpdateZValue()
    {
        //rotation.z 값 구하기
        float targetDeg = Mathf.MoveTowardsAngle(transform.eulerAngles.z, mouseDeg, StatSO.rotateSpeed * Time.deltaTime);

        //지금의 방향을 moveDir의 방향으로 정하기
        moveDir = new Vector2(Mathf.Cos(targetDeg * Mathf.Deg2Rad), Mathf.Sin(targetDeg * Mathf.Deg2Rad));
    }

    public void SetMouseDeg(Vector2 mousePos)
    {
        float targetRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        mouseDeg = targetRad * Mathf.Rad2Deg;
    }
}
