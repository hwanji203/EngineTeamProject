using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameClearUI : MonoBehaviour
{
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
            StarManager.Instance.Clear();
        }
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            StarManager.Instance.gameObject.SetActive(true);
        }
    }
}
