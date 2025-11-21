using UnityEngine;
using System;
using TMPro;

public class BattleRoundManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private EnemyDatabase enemyDatabase;
    
    [Header("Wave Data")]
    [SerializeField] private BattleWaveData[] waves; // 웨이브 배열
    
    private int currentWaveIndex = 0;
    private int remainingEnemiesInWave;
    private bool isRoundActive = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRoundActive)
        {
            StartWave(0);
        }
    }

    private void StartWave(int waveIndex)
    {
        if (waveIndex >= waves.Length)
        {
            CompleteRound();
            return;
        }

        currentWaveIndex = waveIndex;
        BattleWaveData wave = waves[waveIndex];
        isRoundActive = true;

        // 카메라 고정
        BattleCamera.Instance.PauseCameraFollow();

        // 총 몬스터 수 계산 및 소환
        remainingEnemiesInWave = wave.GetTotalEnemyCount();
        SpawnWaveEnemies(wave);
    }

    private void SpawnWaveEnemies(BattleWaveData wave)
    {
        int spawnIndex = 0;
        foreach (var spawnInfo in wave.enemySpawnInfos)
        {
            GameObject prefab = enemyDatabase.GetPrefab(spawnInfo.enemyType);
            for (int i = 0; i < spawnInfo.spawnCount; i++)
            {
                Enemy enemy = Instantiate(prefab, EnemySpawnManager.Instance.GetRandomSpawnPosition(), Quaternion.identity, transform).GetComponent<Enemy>();
                enemy.OnDead += HandleEnemyDied;
                spawnIndex++;
            }
        }
    }

    private void HandleEnemyDied()
    {
        Debug.Log("dfsf");
        if (!isRoundActive) return;

        remainingEnemiesInWave--;
        Debug.Log($"{remainingEnemiesInWave} left");

        if (remainingEnemiesInWave <= 0)
        {
            // 다음 웨이브 시작
            StartWave(currentWaveIndex + 1);
        }
    }

    private void CompleteRound()
    {
        Debug.Log("All waves cleared!");
        isRoundActive = false;
        GetComponent<Collider2D>().enabled = false;
        BattleCamera.Instance.ResumeCameraFollow();
    }
}
