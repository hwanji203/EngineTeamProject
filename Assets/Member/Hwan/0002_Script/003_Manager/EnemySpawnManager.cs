using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private StageInfoSO stageInfoSO;
    private Transform camTrn;

    private bool isPlayerOnFightField;

    private void Awake()
    {
        stageInfoSO = GameManager.Instance.StageSO;
        camTrn = GameManager.Instance.CinemachineCam.transform;
    }


}
