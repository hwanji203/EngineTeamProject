using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }
    [SerializeField] private FadeUI[] fadeUIs;
    [SerializeField] private RectTransform fadeRectTrn;
    [SerializeField] private TutorialInfoSO tutoInfoSO;
    [SerializeField] private RectTransform messageTrn;
    [SerializeField] private float waitTime;
    public UIType UIType => UIType.TutorialUI;

    private TalkManager talkManager;
    private Button checkButton;
    private int currentTutorialNumber;

    public void Close()
    {
        SettingUIs();

        talkManager.ActiveFalse();

        UIObject.SetActive(false);
    }

    private void SettingUIs()
    {
        checkButton.interactable = false;
        checkButton.gameObject.SetActive(false);

        for (int i = 0; i < fadeUIs.Length; i++)
        {
            fadeUIs[i].SetAlpha(0);
        }
    }

    public void Initialize()
    {
        talkManager = TalkManager.Instance;
        checkButton = GetComponentInChildren<Button>(true);
        checkButton.onClick.AddListener(Skip);

        currentTutorialNumber = 0;

        fadeUIs[0].Initialize();
        fadeUIs[1].Initialize();

        SettingUIs();
        UIObject.SetActive(false);

    }

    public void Open()
    {
        StartTutorial(currentTutorialNumber);
    }

    private void StartTutorial(int currentTutoNumber)
    {
        Time.timeScale = 0;

        if (tutoInfoSO.StageInfos.Length < currentTutoNumber)
        {
            Close();
        }
        StageInfo currentInfo = tutoInfoSO.StageInfos[currentTutoNumber];

        //¹ØÀÛ¾÷
        fadeRectTrn.anchoredPosition = currentInfo.FadePosOffset;
        fadeRectTrn.sizeDelta = currentInfo.FadeScale;
        messageTrn.anchoredPosition = currentInfo.MessagePosOffset;

        //´«¿¡ º¸ÀÌ°Ô ÇÏ±â
        UIObject.SetActive(true);
        fadeUIs[0].FadeOut(1);
        fadeUIs[1].FadeOut(1);
        fadeUIs[0].OnFadeEnd += () =>
        {
            talkManager.StartTyping(currentInfo.TutorialMessage);
            talkManager.OnTypingEnd += CanSkip;
        };
    }

    private void CanSkip()
    {
        talkManager.OnTypingEnd -= CanSkip;
        checkButton.gameObject.SetActive(true);
        checkButton.interactable = true;
    }

    private void Skip()
    {
        Time.timeScale = 1;
        currentTutorialNumber++;
        Close();
        StartCoroutine(WaitForNextTuto());
    }

    private IEnumerator WaitForNextTuto()
    {
        yield return new WaitForSeconds(waitTime);
        Open();
    }
}
