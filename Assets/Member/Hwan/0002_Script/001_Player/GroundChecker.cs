using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float nearGroundYValuePercent;
    private float camHalfSize;
    private float nearGroundYValue;

    private Transform camTrn;
    private Transform playerTrn;

    public event Action<float> OnNearGround;
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    public void Awake()
    {
        playerTrn = transform;
        camTrn = Camera.main.transform;

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
        float currentNearGroundYValue = nearGroundYValue + camTrn.position.y;
        float currentGroundYValue = -1 * camHalfSize + camTrn.position.y;

        if (value <= currentNearGroundYValue)
        {
            float currentPercent = 1 - (value - currentGroundYValue) / (currentNearGroundYValue - currentGroundYValue);
            if (currentPercent < 0.01f) currentPercent = 0;
            else if (currentPercent > 0.99f) currentPercent = 1;

            OnNearGround?.Invoke(currentPercent);
            
        }
    }
    
    public void SubOnNoAir(Action<float> method)
    {
        OnNearGround += method;
    }
}
