using UnityEngine;

public class ScreenWrapSoft : MonoBehaviour
{
    public Camera cam;
    private Transform ghost;
    private SpriteRenderer originalSR;
    private SpriteRenderer ghostSR;

    void Start()
    {
        originalSR = GetComponent<SpriteRenderer>();

        ghost = new GameObject("GhostWrap").transform;
        ghostSR = ghost.gameObject.AddComponent<SpriteRenderer>();
        ghostSR.sprite = originalSR.sprite;
        ghostSR.sortingLayerID = originalSR.sortingLayerID;
        ghostSR.sortingOrder = originalSR.sortingOrder;

        ghost.localScale = transform.localScale;
    }

    void Update()
    {
        Vector3 pos = cam.WorldToViewportPoint(transform.position);

        // Ghost는 기본적으로 비활성화
        ghostSR.enabled = false;

        // 왼쪽으로 나가면 오른쪽에 ghost 보이기
        if (pos.x < 0)
        {
            ghost.position = transform.position + Vector3.right * GetScreenWidth();
            ghostSR.enabled = true;
        }
        // 오른쪽으로 나가면 왼쪽에 ghost 보이기
        else if (pos.x > 1)
        {
            ghost.position = transform.position - Vector3.right * GetScreenWidth();
            ghostSR.enabled = true;
        }
    }

    float GetScreenWidth()
    {
        float height = 2f * cam.orthographicSize;
        return height * cam.aspect;
    }
}
