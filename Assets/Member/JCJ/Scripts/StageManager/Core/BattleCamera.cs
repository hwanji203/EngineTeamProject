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
        
        positionComposer = mainFollowCamera.GetComponent<CinemachinePositionComposer>();
        if (positionComposer == null)
            Debug.LogWarning("mainFollowCamera에 CinemachinePositionComposer가 없습니다.");
    }
    
    public void PauseCameraFollow()
    {
        if (positionComposer == null)
        {
            Debug.LogError("Position Composer is null");
            return;
        }
        positionComposer.enabled = false;
    }

    public void ResumeCameraFollow()
    {
        if (positionComposer == null)
        {
            Debug.LogError("Position Composer is null");
            return;
        }
        positionComposer.enabled = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}