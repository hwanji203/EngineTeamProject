using UnityEngine;
using UnityEngine.InputSystem;

public class TestMoneyGettor : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            CurrencyManager.Instance.AddGold(10);
        }
    }
}
