using UnityEngine;

public enum EnemyName
{
    Fish,
    Anglerfish,
    Turtle,
    Pufflefish,
    Dolphin,
    JellyFish,
    Crab
}

[CreateAssetMenu(fileName = "StageInfoSO", menuName = "HwanSO/StageInfoSO")]
public class StageInfoSO : ScriptableObject
{
    [field: SerializeField] public float StartY { get; set; }
    [field: SerializeField] public float EndY { get; set; }

    public Enemys[] EnemyList;
    
    [System.Serializable]
    public struct Enemys
    {
        public EnemyName name;
        public GameObject enemyPrefab;
    }
}
