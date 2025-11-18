using System.Collections;
using UnityEngine;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    [SerializeField] private bool doTuto;

    private void Start()
    {
        if (doTuto) SaveManager.Instance.SaveValue("Tutorial", 0);
        else SaveManager.Instance.SaveValue("Tutorial", 1);

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            StartCoroutine(StartTutorial());
        }
    }

    private IEnumerator StartTutorial()
    {
        GameManager.Instance.Player.InputSO.LookInput(false);
        GameManager.Instance.Player.InputSO.LookInput(InputType.Aim, true);
        yield return new WaitForSeconds(2);
        UIManager.Instance.OpenUI(UIType.TutorialUI);
    }
}
