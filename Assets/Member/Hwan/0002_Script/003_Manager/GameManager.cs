using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public StageInfoSO StageSO { get; private set; }
}
