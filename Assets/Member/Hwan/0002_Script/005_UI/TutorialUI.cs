using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour, IUI
{
    [field: SerializeField] public GameObject UIObject { get; private set; }
    [SerializeField] private FadeUI[] fadeUIs;
    [SerializeField] private RectTransform fadeRectTrn;
    [SerializeField] private TutorialInfoSO tutoInfoSO;
    [SerializeField] private RectTransform messageTrn;

    private TutorialMove tutoMove;

    public UIType UIType => UIType.TutorialUI;

    private TalkManager talkManager;
    private Button checkButton;
    private int currentTutorialNumber;
    private PlayerInputSO inputSO;

    public void Close()
    {
        SettingUIs();
        Time.timeScale = 1;

        talkManager.ActiveFalse();

        UIObject.SetActive(false);
    }

    private void SettingUIs()
    {
        checkButton.interactable = false;
        messageTrn.gameObject.SetActive(false);

        for (int i = 0; i < fadeUIs.Length; i++)
        {
            fadeUIs[i].SetAlpha(0);
        }

        switch (currentTutorialNumber)
        {
            case 1:
                //처음꺼 끝났을 때
                inputSO.LookInput(InputType.Aim, true);
                tutoMove.Enemy = TutorialManager.Instance.SpawnTutorialEnemy();
                break;
            case 2:
                inputSO.LookInput(InputType.Move, true);
                break;
            case 3:
                inputSO.LookInput(InputType.Flip, true);
                break;
            case 4:
                inputSO.LookInput(InputType.Dash, true);
                break;
            case 5:
                GameManager.Instance.CinemachineCam.GetComponent<CinemachinePositionComposer>().enabled = true;
                SaveManager.Instance.SaveValue("Tutorial", 1);
                TutorialManager.Instance.EndTutorial();
                StarManager.Instance.AddGold(500);
                break;
        }
    }

    public void Initialize()
    {
        inputSO = GameManager.Instance.Player.InputSO;

        talkManager = TalkManager.Instance;
        checkButton = GetComponentInChildren<Button>(true);
        try
        {
            checkButton.onClick.AddListener(Skip);
            checkButton.interactable = false;
        }
        catch (NullReferenceException)
        {
            Debug.Log("버튼이 없습니다.");
        }

        tutoMove = GetComponent<TutorialMove>();
  
        currentTutorialNumber = 0;

        fadeUIs[0].Initialize();
        fadeUIs[1].Initialize();

        SettingUIs();
    }

    public void Open()
    {
        StartTutorial();
    }

    private void StartTutorial()
    {
        if (tutoInfoSO.TutorialInfos.Length - 1 < currentTutorialNumber) return;

        Time.timeScale = 0;
        checkButton.interactable = false;

        TutorialInfo currentInfo = tutoInfoSO.TutorialInfos[currentTutorialNumber];

        tutoMove.Move2Target(currentInfo);

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
        StartCoroutine(WaitSkip());
    }

    private IEnumerator WaitSkip()
    {
        checkButton.interactable = false;
        yield return new WaitForSecondsRealtime(0.5f);
        checkButton.interactable = true;
    }

    private void Skip()
    {
        currentTutorialNumber++;
        Close();
    }

    public void LateInitialize() { }
}