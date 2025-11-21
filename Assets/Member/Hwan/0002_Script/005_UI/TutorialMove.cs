using UnityEngine;

public class TutorialMove : MonoBehaviour
{
    [SerializeField] private RectTransform fadeRectTrn;
    [SerializeField] private RectTransform messageRectTrn;
    [SerializeField] private RectTransform sliderRect;
    [SerializeField] private RectTransform stamina;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _canvasRect;
    [SerializeField] private Vector2 leftTopEndPosition = new Vector2(530, 445);
    [SerializeField] private Vector2 rightBottomPosition = new Vector2(300, 200);
    public Transform Enemy { get; set; }
    private Transform player;

    private void Awake()
    {
        player = GameManager.Instance.Player.transform;
    }

    private void Start()
    {
        messageRectTrn.anchoredPosition = Vector2.zero;

    }
    public void Move2Target(TutorialInfo currentInfo)
    {
        Vector2 targetPos = new();

        switch (currentInfo.TargetType)
        {
            case TutorialTarget.None:
                Debug.Log("None인데요");
                break;
            case TutorialTarget.Player:
                targetPos = WorldToCanvasPos(player.position);
                break;
            case TutorialTarget.Slider:
                targetPos = sliderRect.anchoredPosition;
                break;
            case TutorialTarget.Enemy:
                targetPos = WorldToCanvasPos(Enemy.position);
                break;
            case TutorialTarget.Stamina:
                targetPos = stamina.anchoredPosition;
                break;
        }

        SetRectValue(targetPos, currentInfo);
    }

    private void SetRectValue(Vector3 pos, TutorialInfo info)
    {
        // FadeRect는 원래대로
        fadeRectTrn.anchoredPosition = pos + (Vector3)info.FadePosOffset;
        fadeRectTrn.sizeDelta = info.FadeScale;

        // MessageRect 위치 계산
        Vector3 messagePos = pos + (Vector3)info.MessagePosOffset;

        // Clamp 범위 계산
        float minX = -leftTopEndPosition.x;
        float maxX = rightBottomPosition.x;

        float maxY = leftTopEndPosition.y;
        float minY = -rightBottomPosition.y;

        // Clamp 적용
        messagePos.x = Mathf.Clamp(messagePos.x, minX, maxX);
        messagePos.y = Mathf.Clamp(messagePos.y, minY, maxY);

        messageRectTrn.anchoredPosition = messagePos;
    }


    private Vector2 WorldToCanvasPos(Vector3 worldPos)
    {
        Camera cam = null;

        if (_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            cam = null;
        }
        else
        {
            cam = _canvas.worldCamera != null ? _canvas.worldCamera : Camera.main;
        }

        // 1) 월드 → 스크린
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // 2) 스크린 → 캔버스 로컬(anchored 기준)
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRect,
            screenPos,
            cam,
            out localPoint
        );

        return localPoint; // 이게 anchoredPosition으로 쓸 좌표
    }
}
