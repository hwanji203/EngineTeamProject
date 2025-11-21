using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingUI : MonoBehaviour,IUI
{
    [SerializeField]private AudioMixer mixer;
    [SerializeField]private GameObject InStageUI;

    [field:SerializeField]public GameObject UIObject { get; set; }

    public UIType UIType => UIType.SettingUI;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && UIObject.activeSelf == true)
        {
            TurnOff();
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && UIObject.activeSelf == false)
        {
            Open();
        }

        if(SceneManager.GetActiveScene().name == "HWTestScene")
        {
            InStageUI.SetActive(false);
        }
        else
        {
            InStageUI.SetActive(true);
        }
    }

    public void SetMasterLevel(float sliderVal)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderVal) * 20);
    }
    public void SetSFXLevel(float sliderVal)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderVal) * 20);
    }
    public void SetBGMLevel(float sliderVal)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderVal) * 20);
    }
    public void TurnOff()
    {
        Close();
        Time.timeScale = 1f;
    }
    public void ExitStage()
    {

    }
    public void Continue()
    {
        Time.timeScale = 1f;
    }

    public void Initialize()
    {
    }

    public void LateInitialize()
    {
    }

    public void Open()
    {
        UIObject.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Close()
    {
        UIObject.SetActive(false);
    }
    
}
