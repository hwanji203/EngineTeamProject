using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FuctionBtn : MonoBehaviour
{
    [SerializeField]private float moveDuration = 0.5f;
    [SerializeField]private Ease moveEase = Ease.OutQuad;
    [SerializeField]private float moveDistance = 100f;

    void Start()
    {
        
    }

    private void Update()
    {
        if(Keyboard.current.aKey.wasPressedThisFrame)
        {
            MoveIn();

        }
    }

    private void MoveIn()
    {
        transform.DOMoveX(transform.position.x + moveDistance, moveDuration)
            .SetEase(moveEase)
            .SetUpdate(true);
    }
}
