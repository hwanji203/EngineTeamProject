using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D가 없습니다!");
        }
    }
    
    void Update()
    {
        // 입력 받기
        moveInput.x = Input.GetAxisRaw("Horizontal"); // A/D 또는 ←/→
        moveInput.y = Input.GetAxisRaw("Vertical");   // W/S 또는 ↑/↓
        
        // 대각선 이동 시 속도 보정
        moveInput.Normalize();
    }
    
    void FixedUpdate()
    {
        // 이동 적용
        rb.linearVelocity = moveInput * moveSpeed;
    }
}