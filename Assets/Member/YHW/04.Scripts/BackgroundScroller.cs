using UnityEngine;

public class BackgroundTextureScroller : MonoBehaviour
{
    [Tooltip("스크롤 속도 (x, y)")]
    public Vector2 scrollSpeed = new Vector2(0.1f, 0f); // X축으로 초당 0.1씩 이동

    // 스크롤할 머티리얼 (Inspector에서 할당해도 되고, Start에서 찾아도 됨)
    public Material scrollMaterial;

    // 쉐이더 프로퍼티 ID (성능을 위해 캐시)
    private int offsetPropertyID;

    // 현재 누적된 오프셋 값
    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        // 만약 Inspector에서 머티리얼을 할당하지 않았다면,
        // 이 오브젝트의 렌더러에서 머티리얼을 직접 가져옵니다.
        if (scrollMaterial == null)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                // material을 사용해야 이 오브젝트만의 머티리얼 인스턴스가 생성됩니다.
                scrollMaterial = renderer.material;
            }
        }

        if (scrollMaterial != null)
        {
            // 쉐이더 그래프에서 만든 프로퍼티 이름("_Offset")으로 ID를 찾습니다.
            offsetPropertyID = Shader.PropertyToID("_Offset");
        }
        else
        {
            Debug.LogError("스크롤할 머티리얼이 없습니다!", this);
            this.enabled = false; // 스크립트 비활성화
        }
    }

    void Update()
    {
        // 매 프레임마다 속도와 시간에 맞춰 오프셋 값을 누적시킵니다.
        // Time.deltaTime을 곱해줘서 프레임 속도와 관계없이 일정하게 움직입니다.
        currentOffset += scrollSpeed * Time.deltaTime;

        // 쉐이더의 "_Offset" 프로퍼티에 계산된 값을 전달합니다.
        // Frac 노드가 1.0이 넘는 값을 알아서 0~1 사이로 "래핑"해줍니다.
        scrollMaterial.SetVector(offsetPropertyID, currentOffset);
    }
}