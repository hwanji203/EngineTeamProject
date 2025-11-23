using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void Play()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        UIManager.Instance.OpenUI(UIType.SettingUI);

    }
    

    [SerializeField] private float hoverScaleFactor = 1.1f;

    [SerializeField] private float scaleDuration = 0.2f;

    [SerializeField] private Ease scaleEase = Ease.OutQuad; 

    private Vector3 originalScale; 
    private Tween currentTween;    

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }
        currentTween = transform.DOScale(originalScale * hoverScaleFactor, scaleDuration)
                              .SetEase(scaleEase)
                              .SetUpdate(true); 
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        currentTween = transform.DOScale(originalScale, scaleDuration)
                              .SetEase(scaleEase)
                              .SetUpdate(true).UI();
    }

    private void OnDisable()
    {
        if (currentTween != null)
        {
            currentTween.Kill();
            transform.localScale = originalScale;
        }
    }
}
