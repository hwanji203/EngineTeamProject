using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour, IUI
{

    [SerializeField] private Image fadePanel;

    [field: SerializeField] public GameObject UIObject { get; set; }

    public UIType UIType => UIType.GameOverUI;

    public void GoLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LobbyUI");
    }
    public void RePlay()
    {
        Time.timeScale = 1f;
        Close();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Close()
    {
        UIObject.SetActive(false);
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void Open()
    {
        Time.timeScale = 0f;
        UIObject.SetActive(true);
        fadePanel.DOFade(0.5f, 1f).UI();
    }

   
}
