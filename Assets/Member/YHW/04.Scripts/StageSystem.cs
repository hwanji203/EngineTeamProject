using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageSystem : MonoSingleton<StageSystem>
{

    int enemyCount = 50;

    [SerializeField]private GameObject gameClearUI;

    private void Update()
    {
        if (enemyCount <= 0)
        {
            GameClear();
        }
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            KilEnemy();
        }
    }

    private void GameClear()
    {
        gameClearUI.SetActive(true);
    }

    public void KilEnemy()
    {
        enemyCount-=10;
    }
}
