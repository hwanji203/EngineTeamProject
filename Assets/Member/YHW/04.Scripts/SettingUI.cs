using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour,IUI
{
    [SerializeField]private AudioMixer mixer;
    [SerializeField]private GameObject InStageUI;

    [field:SerializeField]public GameObject UIObject { get; set; }

    public UIType UIType => UIType.SettingUI;

    [SerializeField] private GameObject[] allSlider;

    private void Awake()
    {
        SetSliderUI();
    }

    public void SetSliderUI()
    {
        float value;
        
        if (mixer.GetFloat("Master", out value))
            allSlider[0].GetComponent<Slider>().value = Mathf.Pow(10, value / 20f);
        
        if (mixer.GetFloat("SFX", out value))
            allSlider[1].GetComponent<Slider>().value = Mathf.Pow(10, value / 20f);

        if (mixer.GetFloat("BGM", out value))
            allSlider[2].GetComponent<Slider>().value = Mathf.Pow(10, value / 20f);
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
        SceneManager.LoadScene("Lobby");
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
