using System.Collections;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [field: SerializeField] public bool DoTuto { get; private set; }
    [SerializeField] private TutorialEnemy tutorialEnemy;
    [SerializeField] private bool inGame;

    public bool IsTutorialing { get; private set; }

    private void Start()
    {
        if (inGame == false)
        {
            if (DoTuto) SaveManager.Instance.SaveValue("Tutorial", 0);
            else SaveManager.Instance.SaveValue("Tutorial", 1);
        }

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            StartCoroutine(StartTutorial());
        }
    }

    private IEnumerator StartTutorial()
    {
        IsTutorialing = true;
        GameManager.Instance.Player.InputSO.LookInput(false);
        GameManager.Instance.Player.InputSO.LookInput(InputType.Aim, true);
        yield return new WaitForSeconds(0.35f);
        TutorialTriggerOn();
    }

    public void TutorialTriggerOn()
    {
        UIManager.Instance.OpenUI(UIType.TutorialUI);
    }

    public Transform SpawnTutorialEnemy()
    {
        return Instantiate(tutorialEnemy, transform).transform;
    }

    public void EndTutorial()
    {
        IsTutorialing = false;
    }
}
