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
            acMoneyT.SetText("AcquiredGold : " + StarManager.Instance.acquiredMoney.ToString());
        }
    }
}
