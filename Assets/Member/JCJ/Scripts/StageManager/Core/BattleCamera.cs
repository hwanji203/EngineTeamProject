using UnityEngine;
using Unity.Cinemachine;

public class BattleCamera : MonoBehaviour
{
    public static BattleCamera Instance { get; private set; }

    [SerializeField] private CinemachineCamera mainFollowCamera;
    [SerializeField] private CinemachineCamera battleCamera;

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
    }
    //배틀 카메라 전환
    public void SwitchToBattleCamera(Transform battleCameraTransform)
    {
        if (battleCameraTransform == null)
        {
            Debug.LogError("Battle camera transform is null");
            return;
        }

        // 배틀 카메라 위치 설정
        battleCamera.transform.position = battleCameraTransform.position;
        
        // 우선순위 변경
        battleCamera.Priority = 10;
        mainFollowCamera.Priority = 0;
        
        Debug.Log($"<color=cyan>Battle Camera activated at {battleCameraTransform.position}</color>");
    }

    // 메인 카메라로 복귀
    public void SwitchToMainCamera()
    {
        // 우선순위 원래대로
        mainFollowCamera.Priority = 10;
        battleCamera.Priority = 0;
        
        Debug.Log("<color=cyan>Main Camera restored</color>");
    }
}