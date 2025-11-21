using UnityEngine;

[System.Serializable]
public class BattleWaveData
{
    [Header("Enemy Spawn Infos")]
    public EnemySpawnInfo[] enemySpawnInfos;

    public int GetTotalEnemyCount()
    {
        int total = 0;
        foreach (var spawnInfo in enemySpawnInfos)
            total += spawnInfo.spawnCount;
        return total;
    }
}