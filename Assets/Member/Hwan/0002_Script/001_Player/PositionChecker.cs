using System;
using Unity.Cinemachine;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] private float nearGroundYValuePercent;
    private float camHalfSize;
    private float nearGroundYValue;

    [SerializeField] private float startClearYOffset;
    private float startClearY;
    private StageInfoSO stageSO;

    [SerializeField] private CinemachineCamera camTrn;

    public event Action<float> OnNearGround;
    public event Action<float> OnNearClear;
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    public void Awake()
    {
        stageSO = GameManager.Instance.StageSO;
        startClearY = stageSO.EndY - startClearYOffset;

        camHalfSize = Camera.main.orthographicSize;
        nearGroundYValue = 2 * camHalfSize * nearGroundYValuePercent - camHalfSize;
    }

    private void Start()
    {
        PlayerYPos.OnValueChange += CheckGround;
        PlayerYPos.OnValueChange += CheckClear;
    }

    private void Update()
    {
        PlayerYPos.Value = transform.position.y;
    }

    private void CheckGround(float playerYPos)
    {
        float currentNearGroundYValue = nearGroundYValue + camTrn.State.RawPosition.y;
        float currentGroundYValue = -1 * camHalfSize + camTrn.State.RawPosition.y;

        if (playerYPos <= currentNearGroundYValue)
        {
            float currentPercent = 1 - (playerYPos - currentGroundYValue) / (currentNearGroundYValue - currentGroundYValue);
            if (currentPercent < 0.05f) currentPercent = 0;
            else if (currentPercent > 0.95f) currentPercent = 1;

            OnNearGround?.Invoke(currentPercent);
        }
    }

    private void CheckClear(float playerYPos)
    {
        if (playerYPos >= startClearY)
        {
            float currentPercent = (playerYPos - startClearY) / (stageSO.EndY - startClearY);
            if (currentPercent < 0.05f) currentPercent = 0;
            else if (currentPercent > 0.95f) currentPercent = 1;

            OnNearClear?.Invoke(currentPercent);
        }
    }
    
    public void SubNearGround(Action<float> method)
    {
        OnNearGround += method;
    }

    public void SubNearClear(Action<float> method)
    {
        OnNearClear += method;
    }
}
