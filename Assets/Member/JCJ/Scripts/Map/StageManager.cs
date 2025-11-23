using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    
    private bool[] levelClear = new bool[10];
    public event Action<int> OnStageCleared;
    public event Action<int> OnStageUnlocked;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public bool CanOpenLevel(int level)
    {
        if (level == 1) return true;
        return levelClear[level - 1];
    }
    
    public void ClearLevel(int level)
    {
        if (!levelClear[level])
        {
            levelClear[level] = true;
            SaveProgress();
            
            OnStageCleared?.Invoke(level);
            
            if (level < 9)
            {
                OnStageUnlocked?.Invoke(level + 1);
            }
            
            Debug.Log($"{level} 레벨 클리어");
        }
    }
    
    public bool IsLevelCleared(int level)
    {
        return levelClear[level];
    }
    
    private void SaveProgress()
    {
        for (int i = 1; i <= 9; i++)
        {
            PlayerPrefs.SetInt($"Level_{i}_Clear", levelClear[i] ? 1 : 0);
        }
        PlayerPrefs.Save();
    }
    
    private void LoadProgress()
    {
        for (int i = 1; i <= 9; i++)
        {
            levelClear[i] = PlayerPrefs.GetInt($"Level_{i}_Clear", 0) == 1;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}