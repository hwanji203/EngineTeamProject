using UnityEngine;

[CreateAssetMenu(fileName = "StageInfoSO", menuName = "HwanSO/Info/StageInfoSO")]
public class StageInfoSO : ScriptableObject
{
    [field: SerializeField] public float StartY;
    [field: SerializeField] public float EndY;

    [field: SerializeField] public float enemyLevel;
}
