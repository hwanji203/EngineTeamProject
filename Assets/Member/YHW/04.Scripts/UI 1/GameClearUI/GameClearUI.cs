using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameClearUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI acMoneyT;

    public void Retry()
    {

    }

    public void Quit()
    {
        
    }

    public void NextStage()
    {
    }
    
    private void Start()
    {
        StarManager.Instance.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            StarManager.Instance.AddStar();
            StarManager.Instance.AddGold(100);
        }
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            StarManager.Instance.gameObject.SetActive(true);
            ShowScore(StarManager.Instance.acquiredMoney, 2f);
        }
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
}
