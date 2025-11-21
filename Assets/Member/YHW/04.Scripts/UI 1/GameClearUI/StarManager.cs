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
    [SerializeField]private List<GameObject> starList = new List<GameObject>();

    [SerializeField]private Image fadePanel;
    [SerializeField]private Sprite filledStarSprite;

    private int clearHash = Animator.StringToHash("Clear");

    private int starCount = 0;

    private int acquiredMoney;

    protected override void Awake()
    {
        base.Awake();
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    private void OnEnable()
    {
        FadeIn();
        ShowScore(acquiredMoney, 2f);
        StartCoroutine(Clear());
    }

    
    private void FadeIn()
    {   
        fadePanel.DOFade(0.5f, 1f);
    }

    private void OnDisable()
    {
        starCount = 0;
    }

    public void AddStar()
    {
        starCount++;
    }

    public void AddGold(int howmuch)
    {
        acquiredMoney += howmuch;
        CurrencyManager.Instance.AddGold(howmuch);
    }

    public void ChangeState()
    {
        for (int i = 0; i <starCount ; i++)
        {
            starList[i].GetComponent<Animator>().enabled = false;
            starList[i].GetComponent<SpriteRenderer>().sprite = filledStarSprite;
        }
    }
    

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < starCount; i++)
        {
            starList[i].GetComponent<Animator>().SetTrigger(clearHash);
        }
    }
    [SerializeField] private TextMeshProUGUI acMoneyT;

    [field:SerializeField]public GameObject UIObject { get; set; }

    public UIType UIType => UIType.GameClearUI;

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
        duration).SetEase(Ease.OutCubic);
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void Open()
    {
        UIObject.SetActive(true);
    }

    public void Close()
    {
        UIObject.SetActive(false);
    }
}
