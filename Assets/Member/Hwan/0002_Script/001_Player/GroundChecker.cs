using System;
using Unity.Cinemachine;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float nearGroundYValuePercent;
    private float camHalfSize;
    private float nearGroundYValue;

    [SerializeField] private CinemachineCamera camTrn;
    private Transform playerTrn;

    public event Action<float> OnNearGround;
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    public void Awake()
    {
        playerTrn = transform;

        camHalfSize = Camera.main.orthographicSize;
        nearGroundYValue = 2 * camHalfSize * nearGroundYValuePercent - camHalfSize;
        
    }

    private void Start()
    {
        PlayerYPos.OnValueChange += CheckGround;
    }

    private void Update()
    {
        PlayerYPos.Value = playerTrn.position.y;
    }

    private void CheckGround(float value)
    {
        float currentNearGroundYValue = nearGroundYValue + camTrn.State.RawPosition.y;
        float currentGroundYValue = -1 * camHalfSize + camTrn.State.RawPosition.y;

        if (value <= currentNearGroundYValue)
        {
            float currentPercent = 1 - (value - currentGroundYValue) / (currentNearGroundYValue - currentGroundYValue);
            if (currentPercent < 0.05f) currentPercent = 0;
            else if (currentPercent > 0.95f) currentPercent = 1;

            OnNearGround?.Invoke(currentPercent);
        }
    }
    
    public void SubOnNoAir(Action<float> method)
    {
        OnNearGround += method;
    }
}
