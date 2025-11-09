using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerStatSO StatSO { get; private set; }
    [SerializeField] private PlayerInputSO inputSO;
    public PlayerStamina StaminaCompo { get; private set; }
    public PlayerMovement MovementCompo { get; private set; }

    private PlayerMoveController moveController = new PlayerMoveController();
    private PlayerAttack AttackCompo;
    private PlayerAnimation AnimationCompo;

    private Vector2 MouseScreenPos => inputSO.MousePos;

    private void Awake()
    {
        GetComponents();

        ComponentInitialize();

        ActionInitialize();
    }
    private void Update()
    {
        MovementCompo.MousePos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        moveController.UpdateState();
        MovementCompo.UpdateRotYValue();
    }

    private void GetComponents()
    {
        AnimationCompo = GetComponentInChildren<PlayerAnimation>();
        MovementCompo = GetComponent<PlayerMovement>();
        AttackCompo = GetComponent<PlayerAttack>();
        StaminaCompo = GetComponent<PlayerStamina>();
    }

    private void ComponentInitialize()
    {
        moveController.Initialize(AttackCompo, StaminaCompo, AnimationCompo, MovementCompo);
        AttackCompo.Initialize(StatSO);
        StaminaCompo.Initialize(StatSO);
    }

    private void ActionInitialize()
    {
        inputSO.OnSpaceBtnChanged += moveController.SetDoMove;
        inputSO.OnMouseClickChanged += moveController.SetState;

        AnimationCompo.OnAttackStart += MovementCompo.AttackMove;
        AnimationCompo.OnAttackEnd += MovementCompo.EndAttack;
        AnimationCompo.OnAttackStart += AttackCompo.Attack;

        StaminaCompo.CurrentStamina.OnValueChange += MovementCompo.StaminaZeroMove;
    }

    public void GetDamage(float value)
    {
        StaminaCompo.LostStamina(value);
        CameraShaker.Instance.RandomShake(value);
    }
}