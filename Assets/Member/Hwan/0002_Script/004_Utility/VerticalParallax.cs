using UnityEngine;

public class VerticalParallax : MonoBehaviour
{
    [SerializeField] private bool isBackground = false;   // 배경 레이어인지 여부
    [SerializeField] private bool doTeleport = false;     // 워프(순환) 사용할지 여부

    [SerializeField] private float speed = 1f;            // 패럴럭스 속도 계수
    [SerializeField] private float parallaxFactor = 0.1f; // 카메라 이동에 곱해줄 계수

    [Header("Teleport Settings")]
    [SerializeField] private float oneBlock = 10f;        // 한 번 순환할 높이(한 블록 크기)
    [SerializeField] private float maxRange = 100f;       // 비상용 최대 범위(너무 멀리 가면 리셋)
    [SerializeField] private float errorRange = 0.1f;     // 이전 코드 호환용(현재는 사용 안 함)

    private Transform camTrn;             // 메인 카메라 Transform
    private Vector3 offset;               // 기준 위치
    private float lastFrameCamYValue;     // 지난 프레임의 카메라 Y값
    private float movedCamYValue;         // 누적된 카메라 이동량(여기에 기반해서 패럴럭스)

    private void Start()
    {
        camTrn = Camera.main.transform;

        // 배경일 경우, 시작 시점에 스테이지 시작 위치만큼 내려놓기
        if (isBackground)
        {
            transform.localPosition -= new Vector3(0f, GameManager.Instance.StageSO.StartY);
        }

        // 이때의 위치를 기준(offset)으로 잡음
        offset = transform.localPosition;
        Debug.Log(offset);
        // 첫 프레임 비교용으로 카메라 현재 Y 저장
        lastFrameCamYValue = GameManager.Instance.StageSO.StartY;

        // 초기값
        movedCamYValue = 0f;
    }

    private void Update()
    {
        movedCamYValue += (camTrn.position.y - lastFrameCamYValue) * 0.1f * speed;
        lastFrameCamYValue = camTrn.position.y;
        if (doTeleport == false)
        {
            transform.localPosition = new Vector3(0, -movedCamYValue) + offset;
            return;
        }

        if (Mathf.Abs(movedCamYValue) >= oneBlock - errorRange && movedCamYValue % 1 < errorRange)
        {
            transform.localPosition = offset;
            movedCamYValue = 0;
        }
        else if (movedCamYValue > maxRange)
        {
            transform.localPosition = offset;
        }
        else
        {
            transform.localPosition = new Vector3(0, -movedCamYValue) + offset;
        }
    }

    // 필요하면 외부에서 전투 시작/끝 시점에 호출해서 기준 재설정 가능
    public void ResetOffset()
    {
        offset = transform.localPosition;
        movedCamYValue = 0f;
        lastFrameCamYValue = camTrn.position.y;
    }
}
