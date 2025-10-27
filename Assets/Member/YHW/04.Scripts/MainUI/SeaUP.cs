using UnityEngine;

public class SeaUP : MonoBehaviour
{
    [SerializeField] private float defaultSpeed = 0.5f;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashDuration = 0.2f;

    [SerializeField]private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isDashing = false;
    private float dashTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0)
                isDashing = false;
            return;
        }

        rb.linearVelocity = Vector2.up * defaultSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = playerTransform.position;
    }
    public void Dash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        rb.AddForce(Vector2.up * dashPower, ForceMode2D.Impulse);
    }
}
