using UnityEngine;

[System.Serializable]
public class BattleRoundData
{
    [Header("Camera")]
    [Tooltip("이 라운드의 배틀 카메라 위치")]
    public Transform cameraTransform;
    
    [Header("Enemy Settings")]
    [Tooltip("소환할 몬스터 종류와 마리수")]
    public EnemySpawnInfo[] enemySpawnInfos;
    
    [Header("Spawn Settings")]
    [Tooltip("몬스터 소환 위치들")]
    public Transform[] spawnPoints;
    
    public int GetTotalEnemyCount()
    {
        int total = 0;
        foreach (var info in enemySpawnInfos)
        {
            total += info.spawnCount;
        }
        return total;
    }
}