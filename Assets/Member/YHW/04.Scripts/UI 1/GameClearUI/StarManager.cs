using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StarManager : MonoBehaviour
{
    static StarManager instance;

    [SerializeField]private List<GameObject> starList = new List<GameObject>();

    [SerializeField]private Image fadePanel;

    private int clearHash = Animator.StringToHash("Clear");

    private int starCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        fadePanel.DOFade(0, 1f);
    }

    private void OnDisable()
    {
        starCount = 0;
    }

    public void Clear()
    {
        starCount++;
    }



    
}
