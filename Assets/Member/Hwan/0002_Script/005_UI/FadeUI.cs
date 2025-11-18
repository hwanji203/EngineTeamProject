using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FadeUI : MonoBehaviour
{
    public event Action OnFadeEnd;

    private float defaultAlpha = 1f;
    private Coroutine fadeCoroutine;
    private Image fadeImage;

    public void Initialize()
    {
        fadeImage = GetComponent<Image>();
        defaultAlpha = fadeImage.color.a;
    }

    public void SetAlpha(float a)
    {
        Color c = fadeImage.color;
        c.a = a;
        fadeImage.color = c;
    }

    public void FadeOut(float time)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutRoutine(time));
    }

    private IEnumerator FadeOutRoutine(float time)
    {
        float t = 0f;

        SetAlpha(0f);

        while (t < time)
        {
            t += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(0f, defaultAlpha, t / time);
            SetAlpha(alpha);

            yield return null;
        }
        SetAlpha(defaultAlpha);
        fadeCoroutine = null;
        OnFadeEnd?.Invoke();
    }
}
