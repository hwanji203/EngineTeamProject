using Member.Kimyongmin._02.Code.Enemy.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField] private float defaultSpawnCool;
    [SerializeField] private float errorSpawnCool;
    [SerializeField] private float spawnOffset;

    private StageInfoSO stageInfoSO;
    private CinemachineCamera cinemachine;

    private bool isPlayerOnFightField;

    private Dictionary<EnemyName, GameObject> enemyDicionary = new();
    private EnemyName[] spawnableEnemies;

    protected override void Awake()
    {
        stageInfoSO = GameManager.Instance.StageSO;
        cinemachine = GameManager.Instance.CinemachineCam;

        spawnableEnemies = new EnemyName[stageInfoSO.EnemyList.Length];

        for (int i = 0; i < stageInfoSO.EnemyList.Length; i++)
        {
            var enemy = stageInfoSO.EnemyList[i];
            enemyDicionary[enemy.name] = enemy.enemyPrefab;
            spawnableEnemies[i] = enemy.name;
        }

        BattleRoundManager.OnBattle += (value) => isPlayerOnFightField = value;
    }

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        while (true)
        {
            float currentTime = UnityEngine.Random.Range(defaultSpawnCool - errorSpawnCool, defaultSpawnCool + errorSpawnCool);

            while (currentTime > 0)
            {
                if (isPlayerOnFightField == true)
                {
                    yield return null;
                    continue;
                }

                currentTime -= Time.deltaTime;
                yield return null;
            }

            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int index = UnityEngine.Random.Range(0, spawnableEnemies.Length);
        EnemyName selectedEnemy = spawnableEnemies[index];

        Vector2 spawnPos = GetRandomSpawnPosition();
        Instantiate(enemyDicionary[selectedEnemy], spawnPos, Quaternion.identity, transform);

        SoundManager.Instance.Play(SFXSoundType.GenEnemy);
    }

    public Vector2 GetRandomSpawnPosition()
    {
        int spawnSide = UnityEngine.Random.Range(0, 2);
        if (spawnSide == 0) spawnSide = -1;

        float spawnXPos = spawnSide * (cinemachine.Lens.Aspect * cinemachine.Lens.OrthographicSize + spawnOffset);
        float spawnYPos = cinemachine.State.RawPosition.y + UnityEngine.Random.Range(0f, cinemachine.Lens.OrthographicSize);

        return new Vector2(spawnXPos, spawnYPos);
    }
}
