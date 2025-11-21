using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerStatSO StatSO { get; private set; }
    [field: SerializeField] public PlayerInputSO InputSO { get; private set; }
    public PlayerStamina StaminaCompo { get; private set; }
    public PlayerMovement MovementCompo { get; private set; }
    public PlayerAttack AttackCompo { get; private set; }
    public PositionChecker PositionCheckerCompo { get; private set; }
    private PlayerMoveController moveController = new PlayerMoveController();
    private PlayerAnimation AnimationCompo;
    private PlayerEyeAnimation eyeAnimation;
    private PlayerBlackEyeMove blackEyeMove;

    private Vector2 MouseScreenPos => InputSO.MousePos;
    public event Action<float, Vector2> OnDamage;

    private void Awake()
    {
        GetComponents();

        ComponentInitialize();

        ActionInitialize();
    }
    private void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(MouseScreenPos);
        MovementCompo.MousePos = mouseWorldPos;
        moveController.UpdateState();
        blackEyeMove.Move(mouseWorldPos);
    }

    private void GetComponents()
    {
        AnimationCompo = GetComponentInChildren<PlayerAnimation>();
        MovementCompo = GetComponent<PlayerMovement>();
        AttackCompo = GetComponent<PlayerAttack>();
        StaminaCompo = GetComponent<PlayerStamina>();
        eyeAnimation = GetComponentInChildren<PlayerEyeAnimation>();
        blackEyeMove = GetComponentInChildren<PlayerBlackEyeMove>();
        PositionCheckerCompo = GetComponent<PositionChecker>();
    }

    private void ComponentInitialize()
    {
        moveController.Initialize(AttackCompo, StaminaCompo, AnimationCompo, MovementCompo);
        AttackCompo.Initialize(StatSO);
        StaminaCompo.Initialize(StatSO);
        eyeAnimation.Initialize();
    }

    private void ActionInitialize()
    {
        InputSO.OnSpaceBtnChanged += moveController.SetDoMove;
        InputSO.OnMouseClickChanged += moveController.SetState;

        AnimationCompo.OnAttackStart += (type) => MovementCompo.Move(type switch {
            PlayerAttackType.Dash => PlayerMovementType.Dash,
            _ => PlayerMovementType.Flip
            }
        );
        AnimationCompo.OnAttackEnd += MovementCompo.EndAttack;
        AnimationCompo.OnAttackStart += AttackCompo.Attack;

        StaminaCompo.CurrentStamina.OnValueChange += MovementCompo.GetStaminaIsZero;

        OnDamage += (_, _) => AnimationCompo.ChangeAnimation(PlayerState.Hit);
        OnDamage += (damage, _) => StaminaCompo.LostStamina(damage);
        OnDamage += (_, enemyPos) => MovementCompo.Damaged(enemyPos);
        OnDamage += (_, _) => MovementCompo.ChangeState(PlayerState.Hit);

        MovementCompo.OnStateChange += eyeAnimation.ChangeAnimation;
    }

    public void GetDamage(float value, Vector2 enemyPos)
    {
        OnDamage?.Invoke(value, enemyPos);
    }

    public void RecoveryStamina(float value)
    {
        StaminaCompo.RecoveryStamina(value);
    }
}