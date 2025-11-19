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
    
    [SerializeField] private TextMeshProUGUI enemyLeftText;
    [SerializeField] private TextMeshProUGUI roundLeftText;
    [SerializeField] private GameObject leftCanvas;
    

    void Start()
    {
        TestEnemy.OnEnemyDied += HandleEnemyDied;
    }

    void OnDestroy()
    {
        TestEnemy.OnEnemyDied -= HandleEnemyDied;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isRoundActive)
        {
            StartWave(0);
            leftCanvas.SetActive(true);
            roundLeftText.text = $"Round : {currentWaveIndex}";
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
        BattleCamera.Instance.SwitchToBattleCamera(wave.cameraTransform);

        // 총 몬스터 수 계산 및 소환
        remainingEnemiesInWave = wave.GetTotalEnemyCount();
        if (waveIndex == 0)
        {
            enemyLeftText.text = $"Enemy : {remainingEnemiesInWave}";
        }
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
                if (spawnIndex >= wave.spawnPoints.Length)
                    spawnIndex = 0;

                Instantiate(prefab, wave.spawnPoints[spawnIndex].position, Quaternion.identity, transform);
                spawnIndex++;
            }
        }
    }

    private void HandleEnemyDied()
    {
        if (!isRoundActive) return;

        remainingEnemiesInWave--;
        enemyLeftText.text = $"Enemy : {remainingEnemiesInWave}";
        Debug.Log($"{remainingEnemiesInWave} left");

        if (remainingEnemiesInWave <= 0)
        {
            // 다음 웨이브 시작
            StartWave(currentWaveIndex + 1);
            roundLeftText.text = $"Round : {currentWaveIndex}";
        }
    }

    private void CompleteRound()
    {
        Debug.Log("All waves cleared!");
        leftCanvas.SetActive(false);
        isRoundActive = false;
        GetComponent<Collider2D>().enabled = false;
        BattleCamera.Instance.SwitchToMainCamera();
    }
}
