using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class StarManager : MonoSingleton<StarManager>
{

    [SerializeField]private List<GameObject> starList = new List<GameObject>();

    [SerializeField]private Image fadePanel;
    [SerializeField]private Sprite filledStarSprite;

    private int clearHash = Animator.StringToHash("Clear");

    private int starCount = 0;

    public int acquiredMoney { get; set; }


    private void OnEnable()
    {
        for (int i = 0; i < starCount; i++)
        {
            starList[i].GetComponent<Animator>().SetTrigger(clearHash);
        }
        FadeIn();
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
    }

    public void ChangeState()
    {
        for (int i = 0; i <starCount ; i++)
        {
            starList[i].GetComponent<Animator>().enabled = false;
            starList[i].GetComponent<SpriteRenderer>().sprite = filledStarSprite;
        }
    }
    


}
