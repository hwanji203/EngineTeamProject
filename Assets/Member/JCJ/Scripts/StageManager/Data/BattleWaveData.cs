using UnityEngine;

[System.Serializable]
public class BattleWaveData
{
    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Enemy Spawn Infos")]
    public EnemySpawnInfo[] enemySpawnInfos;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    public int GetTotalEnemyCount()
    {
        int total = 0;
        foreach (var spawnInfo in enemySpawnInfos)
            total += spawnInfo.spawnCount;
        return total;
    }
}