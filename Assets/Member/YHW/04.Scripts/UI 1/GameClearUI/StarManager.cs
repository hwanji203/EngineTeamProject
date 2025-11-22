using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StarManager : MonoSingleton<StarManager>,IUI
{
    [SerializeField]private Image fadePanel;
    [SerializeField]private Sprite filledStarSprite;

    private int clearHash = Animator.StringToHash("Clear");

    private int acquiredMoney;

    protected override void Awake()
    {
        base.Awake();
    }
    private void OnEnable()
    {

    }

    
    private void FadeIn()
    {   
        fadePanel.DOFade(0.5f, 1f).UI();
    }

    public void AddGold(int howmuch)
    {
        acquiredMoney += howmuch;
        CurrencyManager.Instance.AddGold(howmuch);
    }
    
    [SerializeField] private TextMeshProUGUI acMoneyT;

    [field:SerializeField]public GameObject UIObject { get; set; }

    public UIType UIType => UIType.ClearUI;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {

    }

    public void NextStage()
    {
    }


    

    public void ShowScore(int finalScore, float duration = 1f)
    {
        int current = 0;

        DOTween.To(() => current, x =>
        {
            current = x;
            acMoneyT.text = "AcquiredGold : " + current.ToString();
        },
        finalScore,
        duration).SetEase(Ease.OutCubic).UI();
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void Open()
    {
        Time.timeScale = 0;
        UIObject.SetActive(true); 
        FadeIn();
        ShowScore(acquiredMoney, 2f);
    }

    public void Close()
    {
        Time.timeScale = 1;
        UIObject.SetActive(false);
    }
}
