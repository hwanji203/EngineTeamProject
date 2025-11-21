using UnityEngine;
using Unity.Cinemachine;

public class BattleCamera : MonoBehaviour
{
    public static BattleCamera Instance { get; private set; }

    [SerializeField] private CinemachineCamera mainFollowCamera;

    private CinemachinePositionComposer positionComposer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Position Composer 캐싱 (메인카메라에 붙어있어야 함)
        positionComposer = mainFollowCamera.GetComponent<CinemachinePositionComposer>();
        if (positionComposer == null)
            Debug.LogWarning("mainFollowCamera에 CinemachinePositionComposer가 없습니다.");
    }

    // 배틀 상황에서 카메라 멈춤
    public void PauseCameraFollow()
    {
        if (positionComposer == null)
        {
            Debug.LogError("Position Composer is null");
            return;
        }
        positionComposer.enabled = false;
        Debug.Log("<color=cyan>Main Camera Position Composer OFF (Follow Pause)</color>");
    }

    // 배틀 끝나면 다시 자동 추적 켜기
    public void ResumeCameraFollow()
    {
        if (positionComposer == null)
        {
            Debug.LogError("Position Composer is null");
            return;
        }
        positionComposer.enabled = true;
        Debug.Log("<color=cyan>Main Camera Position Composer ON (Follow Resume)</color>");
    }
}