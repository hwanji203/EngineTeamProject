using System;
using Unity.Cinemachine;
using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [SerializeField] private float outOfCamYValuePercent;
    private float camHalfSize;
    private float nearOutOfCamYValue;

    [SerializeField] private float startClearYOffset;
    private float startClearY;
    private StageInfoSO stageSO;

    private CinemachineCamera camTrn;

    public event Action<float> OnNearOutOfCam;
    public event Action<float> OnNearClear;
    public NotifyValue<float> PlayerYPos = new NotifyValue<float>();

    public void Awake()
    {
        stageSO = GameManager.Instance.StageSO;
        startClearY = stageSO.EndY - startClearYOffset;

        camHalfSize = Camera.main.orthographicSize;
        nearOutOfCamYValue = Mathf.Abs(2 * camHalfSize * outOfCamYValuePercent - camHalfSize);

        camTrn = GameManager.Instance.CinemachineCam;
    }

    private void Start()
    {
        PlayerYPos.OnValueChange += CheckOutOfCam;
        PlayerYPos.OnValueChange += CheckClear;
    }

    private void Update()
    {
        PlayerYPos.Value = transform.position.y;
    }

    private void CheckOutOfCam(float value)
    {
        float currentNearOutOfCamYValue = nearOutOfCamYValue + camTrn.State.RawPosition.y;
        float currentOutOfCamYValue = camHalfSize + camTrn.State.RawPosition.y;
        float playerYPos = Mathf.Abs(camTrn.State.RawPosition.y - value);

        if (playerYPos >= currentNearOutOfCamYValue)
        {
            float currentPercent = 1 - (playerYPos - currentOutOfCamYValue) / (currentNearOutOfCamYValue - currentOutOfCamYValue);
            if (currentPercent < 0.1f) currentPercent = 0;
            else if (currentPercent > 0.9f) currentPercent = 1;

            OnNearOutOfCam?.Invoke(currentPercent);
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
        OnNearOutOfCam += method;
    }

    public void SubNearClear(Action<float> method)
    {
        OnNearClear += method;
    }
}
