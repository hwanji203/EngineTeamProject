using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Controlls;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "HwanSO/PlayerInput")]
public class PlayerInputSO : ScriptableObject, IPlayerActions
{
    private Controlls controlls;

    public event Action OnMouseClick;
    public event Action<bool> OnSpaceBtnChanged;
    public event Action<Vector2> OnMouseMove;

    private void OnEnable()
    {
        if (controlls == null)
        {
            controlls = new();
        }

        controlls.Player.SetCallbacks(this);
        controlls.Player.Enable();
    }

    private void OnDisable()
    {
        controlls.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnMouseClick?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnSpaceBtnChanged?.Invoke(context.performed);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        OnMouseMove?.Invoke(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }
}

//public class AgentMovement : MonoBehaviour
//{
//    [SerializeField] private AgentMovementSO movementSO;
//    [SerializeField] private Vector2 currentVelocity;
//    private Vector2 moveDir;

//    private Rigidbody2D rb;

//    public UnityEvent<float> OnVelocityChanged;

//    public Vector2 lastVelocity;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void FixedUpdate()
//    {
//        OnVelocityChanged?.Invoke(currentVelocity.magnitude);
//        Move();
//    }

//    public void CalculateSpeed(Vector2 value)
//    {
//        float targetX = CalculateAxisSpeed(MathF.Sign(value.x), currentVelocity.x) + currentVelocity.x;
//        float targetY = CalculateAxisSpeed(MathF.Sign(value.y), currentVelocity.y) + currentVelocity.y;

//        currentVelocity = new Vector2(targetX, targetY);
//    }

//    private float CalculateAxisSpeed(float value, float currentSpeed)
//    {
//        if (Mathf.Abs(value) == 1)
//        {
//            // value �������� ���ӵ� ��ȯ
//            // �ٵ� currentSpeed���� ���ӵ��� ������ �� maxSpeed �̻��̸� maxSpeed - currentSpeed ��ȯ

//            float acceleration = value * movementSO.acceleration * Time.deltaTime; // ���ӵ�
//            return acceleration + currentSpeed >= movementSO.maxSpeed ? movementSO.maxSpeed - currentSpeed : acceleration;
//        }
//        else
//        {
//            // value �ݴ� ������ ���ӵ� ��ȯ
//            // �ٵ� currentSpeed���� ���ӵ��� ������ �� 0 ���ϸ� -currentSpeed ��ȯ

//            float deacceleration = -MathF.Sign(currentSpeed) * movementSO.deacceleration * Time.deltaTime;
//            return Mathf.Abs(deacceleration + currentSpeed) <= 0 ? -currentSpeed : deacceleration;
//        }
//    }

//    private void Move()
//    {
//        if (rb.linearVelocity != lastVelocity)
//        {
//            currentVelocity = rb.linearVelocity;
//        }
//        else
//        {
//            rb.linearVelocity = currentVelocity;
//        }
//        lastVelocity = currentVelocity;
//    }
//}

