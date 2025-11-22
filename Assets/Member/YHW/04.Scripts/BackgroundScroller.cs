using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Tooltip("추적할 메인 카메라 (자동으로 찾지만, 수동 할당 권장)")]
    public Camera mainCamera;

    [Tooltip("시차 스크롤 속도 (0.1 = 배경이 10% 속도로 느리게 움직여서 멀어 보임)")]
    public float scrollSpeedX = 0.1f;

    [Tooltip("Y축 스크롤 속도 (좌우로만 움직이면 0)")]
    public float scrollSpeedY = 0f; // Y축은 필요 없으면 0으로 둡니다.

    // 쉐이더에 값을 전달할 머티리얼
    private Material backgroundMaterial;

    // 쉐이더 프로퍼티 ID (성능 향상)
    private int offsetPropertyID;

    void Start()
    {
        // 카메라를 자동으로 찾기
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // 이 오브젝트의 렌더러에서 머티리얼 인스턴스를 가져옵니다.
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            backgroundMaterial = renderer.material; // .material을 써야 인스턴스가 복제됩니다.
        }

        if (backgroundMaterial != null)
        {
            // 쉐이더 그래프에서 만든 프로퍼티 이름("_Offset")으로 ID를 찾습니다.
            offsetPropertyID = Shader.PropertyToID("_Offset");
        }
        else
        {
            Debug.LogError("배경 머티리얼을 찾을 수 없습니다!", this);
            enabled = false;
        }
    }

    void LateUpdate()
    {
        if (backgroundMaterial == null || mainCamera == null) return;

        // --- 1. 쉐이더 오프셋 계산 (원래 하던 일) ---
        float cameraPosX = mainCamera.transform.position.x;
        float cameraPosY = mainCamera.transform.position.y;
        Vector2 offset = new Vector2(cameraPosX * scrollSpeedX, cameraPosY * scrollSpeedY);
        backgroundMaterial.SetVector(offsetPropertyID, offset);

        // --- 2. Quad 오브젝트 위치를 카메라에 고정 (추가된 부분) ---
        // 이 스크립트가 붙은 Quad의 위치를 카메라의 XY 위치와 강제로 동기화시킵니다.
        // Z 값은 Quad가 원래 있던 Z값(예: 10)을 유지합니다.
        float quadZ = transform.position.z;
        transform.position = new Vector3(cameraPosX, cameraPosY, quadZ);
    }
}