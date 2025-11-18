using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controlls;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "HwanSO/PlayerInput")]
public class PlayerInputSO : ScriptableObject, IPlayerActions
{
    private Controlls controlls;

    public event Action<bool, PlayerAttackType> OnMouseClickChanged;
    public event Action<bool> OnSpaceBtnChanged;
    private Dictionary<InputType, bool> lookInputDictionary = new();
    public Vector2 MousePos { get; private set; }

    private void OnEnable()
    {
        lookInputDictionary.Clear();
        for (int i = 0; i < 4; i++)
        {
            lookInputDictionary.Add((InputType)i, true);
        }

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

    public void LookInput(InputType inputType, bool value)
    {
        lookInputDictionary[inputType] = value;
    }

    public void LookInput(bool value)
    {
        for (int i = 0; i < 4; i++)
        {
            lookInputDictionary[(InputType)i] = value;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (lookInputDictionary[InputType.Move] == true)
        {
            OnSpaceBtnChanged?.Invoke(context.performed);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (lookInputDictionary[InputType.Aim] == true)
        {
            MousePos = context.ReadValue<Vector2>();
        }
    }

    public void OnFlipAttack(InputAction.CallbackContext context)
    {
        if (lookInputDictionary[InputType.Flip] == true)
        {
            OnMouseClickChanged?.Invoke(context.performed, PlayerAttackType.Flip);
        }
    }

    public void OnDashAttack(InputAction.CallbackContext context)
    {
        if (lookInputDictionary[InputType.Dash] == true)
        {
            OnMouseClickChanged?.Invoke(context.performed, PlayerAttackType.Dash);
        }
    }
}