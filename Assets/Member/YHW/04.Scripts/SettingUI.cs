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
    public void ExitStage()
    {
        Debug.Log("메인 화면");
    }
    public void Continue()
    {
        Close();
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
        Time.timeScale = 1;
        UIObject.SetActive(false);
    }
    
}
