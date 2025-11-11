using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerStatSO StatSO { get; private set; }
    [SerializeField] private PlayerInputSO inputSO;
    public PlayerStamina StaminaCompo { get; private set; }
    public PlayerMovement MovementCompo { get; private set; }
    public event Action<float, Vector2> OnDamage;

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
        MovementCompo.CurrentValue.MousePos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        moveController.UpdateState();
        if (Keyboard.current.gKey.wasReleasedThisFrame)
        {
            GetDamage(UnityEngine.Random.Range(0, 10), UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(0, 10));
        }
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

        AnimationCompo.OnAttackStart += (type) => MovementCompo.Move(type switch {
            PlayerAttackType.Dash => PlayerMovementType.Dash,
            _ => PlayerMovementType.Flip
            }
        );
        AnimationCompo.OnAttackEnd += MovementCompo.EndAttack;
        AnimationCompo.OnAttackStart += AttackCompo.StartAttack;

        StaminaCompo.CurrentStamina.OnValueChange += MovementCompo.GetStaminaIsZero;

        OnDamage += (damage, vector) => AnimationCompo.ChangeAnimation(PlayerState.Hit);
        OnDamage += (damage, vector) => StaminaCompo.LostStamina(damage);
        OnDamage += (damage, vector) => CameraShaker.Instance.RandomShake(damage);
        OnDamage += (damage, vector) => MovementCompo.Damaged(vector);
    }

    public void GetDamage(float value, Vector2 enemyPos)
    {
        OnDamage?.Invoke(value, enemyPos);
    }
}