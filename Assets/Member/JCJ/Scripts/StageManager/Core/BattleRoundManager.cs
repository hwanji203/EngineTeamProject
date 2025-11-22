using UnityEngine;
using Member.Kimyongmin._02.Code.Agent;
using System.Collections.Generic;
using System;

public class BattleRoundManager : MonoSingleton<BattleRoundManager>
{
    [Header("Database")]
    [SerializeField] private EnemyDatabase enemyDatabase;
    
    [Header("Wave Data")]
    [SerializeField] private BattleWaveData[] waves;
    
    private int currentWaveIndex = 0;
    private int remainingEnemiesInWave;
    private bool isRoundActive = false;
    
    private List<HealthSystem> currentWaveEnemies = new List<HealthSystem>();

    public event Action<bool> OnBattle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRoundActive)
        {
            StartWave(0);
            OnBattle?.Invoke(true);
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

        BattleCamera.Instance.PauseCameraFollow();

        remainingEnemiesInWave = wave.GetTotalEnemyCount();
        SpawnWaveEnemies(wave);
        
        Debug.Log($"웨이브 {waveIndex + 1} 시작! 총 적: {remainingEnemiesInWave}");
    }

    private void SpawnWaveEnemies(BattleWaveData wave)
    {
        // 이전 웨이브 적들 이벤트 구독 해제
        UnsubscribeAllEnemies();
        currentWaveEnemies.Clear();
        
        int spawnIndex = 0;
        foreach (var spawnInfo in wave.enemySpawnInfos)
        {
            GameObject prefab = enemyDatabase.GetPrefab(spawnInfo.enemyType);
            for (int i = 0; i < spawnInfo.spawnCount; i++)
            {
                if (spawnIndex >= wave.spawnPoints.Length)
                    spawnIndex = 0;

                GameObject enemy = Instantiate(prefab, wave.spawnPoints[spawnIndex].position, Quaternion.identity, transform);
                
                // 각 적의 HealthSystem에 이벤트 구독
                HealthSystem healthSystem = enemy.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.OnDeath += HandleEnemyDied;
                    currentWaveEnemies.Add(healthSystem);
                }
                
                spawnIndex++;
            }
        }
    }

    private void HandleEnemyDied()
    {
        if (!isRoundActive) return;

        remainingEnemiesInWave--;
        Debug.Log($"적 처치! 남은 적: {remainingEnemiesInWave}");

        if (remainingEnemiesInWave <= 0)
        {
            Debug.Log("웨이브 클리어!");
            StartWave(currentWaveIndex + 1);
        }
    }

    private void CompleteRound()
    {
        isRoundActive = false;
        GetComponent<Collider2D>().enabled = false;
        BattleCamera.Instance.ResumeCameraFollow();

        // 모든 이벤트 구독 해제
        OnBattle?.Invoke(true);
        UnsubscribeAllEnemies();
        currentWaveEnemies.Clear();
    }
    
    private void UnsubscribeAllEnemies()
    {
        foreach (var enemy in currentWaveEnemies)
        {
            if (enemy != null)
            {
                enemy.OnDeath -= HandleEnemyDied;
            }
        }
    }
    
    protected override void OnDestroy()
    {
        UnsubscribeAllEnemies();
        base.OnDestroy();
    }
}
