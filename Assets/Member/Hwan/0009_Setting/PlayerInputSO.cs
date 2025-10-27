using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Controlls;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "HwanSO/PlayerInput")]
public class PlayerInputSO : ScriptableObject, IPlayerActions
{
    private Controlls controlls;

    public event Action<bool, PlayerAttackType> OnMouseClickChanged;
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

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnSpaceBtnChanged?.Invoke(context.performed);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        OnMouseMove?.Invoke(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }

    public void OnFlipAttack(InputAction.CallbackContext context)
    {
        OnMouseClickChanged?.Invoke(context.performed, PlayerAttackType.Flip);
    }

    public void OnDashAttack(InputAction.CallbackContext context)
    {
        OnMouseClickChanged?.Invoke(context.performed, PlayerAttackType.Dash);
    }
}