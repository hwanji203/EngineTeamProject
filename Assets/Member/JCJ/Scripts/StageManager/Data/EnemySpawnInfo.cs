using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    [Tooltip("소환할 몬스터 타입")]
    public BattleEnemyType enemyType;
    
    [Tooltip("이 타입을 몇 마리 소환할지")]
    [Min(1)]
    public int spawnCount = 1;
}