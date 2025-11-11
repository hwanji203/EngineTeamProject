using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;


public class Combo : MonoBehaviour
{

    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            DoCombo();
        }
    }

    private void DoCombo()
    {
        transform.DOScale(1.5f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }
}
