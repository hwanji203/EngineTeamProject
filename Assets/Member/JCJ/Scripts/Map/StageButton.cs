using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int stageLevel; // 현재 버튼에 해당 하는 레벨
    
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
        StageManager.OnStageCleared += OnAnyStageCleared;
        StageManager.OnStageUnlocked += OnAnyStageUnlocked;
    }
    
    void OnDisable()
    {
        StageManager.OnStageCleared -= OnAnyStageCleared;
        StageManager.OnStageUnlocked -= OnAnyStageUnlocked;
    }
    void OnClick()
    {
        if (!StageManager.Instance.CanOpenLevel(stageLevel))
        {
            Debug.Log("이전 레벨 클리어 필요");
            return;
        }
        
        Debug.Log($"레벨 {stageLevel}");
    }
    
    void UpdateVisual()
    {
        if (StageManager.Instance == null) return;
        
        bool canOpen = StageManager.Instance.CanOpenLevel(stageLevel);
        bool isCleared = StageManager.Instance.IsLevelCleared(stageLevel);
        
        
        if (levelText != null)// 레벨 텍스트
            levelText.text = stageLevel.ToString();
        
        if (isCleared)// 클리어 상태
        {
            button.interactable = true;
            // if (lockIcon != null) lockIcon.SetActive(false);
            // if (clearIcon != null) clearIcon.SetActive(true);
            if (buttonImage != null) buttonImage.color = clearedColor;
        }
        else if (canOpen) // 언락 상태
        {
            button.interactable = true;
            // if (lockIcon != null) lockIcon.SetActive(false);
            // if (clearIcon != null) clearIcon.SetActive(false);
            if (buttonImage != null) buttonImage.color = unlockedColor;
        }
        
        else// 잠김 상태
        {
            button.interactable = false;
            // if (lockIcon != null) lockIcon.SetActive(true);
            // if (clearIcon != null) clearIcon.SetActive(false);
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
