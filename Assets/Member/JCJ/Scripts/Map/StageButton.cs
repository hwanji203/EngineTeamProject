using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int stageLevel;
    
    [Header("Icons")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI levelText;
    
    [Header("Visual")]
    [SerializeField] private Color lockedColor = Color.gray;
    [SerializeField] private Color unlockedColor = Color.white;
    [SerializeField] private Color clearedColor = Color.yellow;
    
    private Image buttonImage;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
        UpdateVisual();
    }
    
    void OnEnable()
    {
        if (StageManager.Instance != null)
        {
            StageManager.Instance.OnStageCleared += OnAnyStageCleared;
            StageManager.Instance.OnStageUnlocked += OnAnyStageUnlocked;
        }
    }
    
    void OnDisable()
    {
        if (StageManager.Instance != null)
        {
            StageManager.Instance.OnStageCleared -= OnAnyStageCleared;
            StageManager.Instance.OnStageUnlocked -= OnAnyStageUnlocked;
        }
    }
    
    void OnClick()
    {
        if (!StageManager.Instance.CanOpenLevel(stageLevel))
        {
            Debug.Log("이전 레벨 클리어 필요");
            return;
        }
        
        Debug.Log($"레벨 {stageLevel}");
        SceneTransitionManager.Instance.LoadScene($"Stage{stageLevel}");
    }
    
    void UpdateVisual()
    {
        if (StageManager.Instance == null) return;
        
        bool canOpen = StageManager.Instance.CanOpenLevel(stageLevel);
        bool isCleared = StageManager.Instance.IsLevelCleared(stageLevel);
        
        if (levelText != null)
            levelText.text = stageLevel.ToString();
        
        if (isCleared)
        {
            button.interactable = true;
            if (buttonImage != null) buttonImage.color = clearedColor;
        }
        else if (canOpen)
        {
            button.interactable = true;
            if (buttonImage != null) buttonImage.color = unlockedColor;
        }
        else
        {
            button.interactable = false;
            if (buttonImage != null) buttonImage.color = lockedColor;
        }
    }
    
    void OnAnyStageCleared(int level)
    {
        UpdateVisual();
    }
    
    void OnAnyStageUnlocked(int level)
    {
        if (level == stageLevel)
        {
            UpdateVisual();
        }
    }
}
