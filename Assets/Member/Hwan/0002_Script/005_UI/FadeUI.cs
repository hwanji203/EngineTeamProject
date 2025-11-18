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
        var img = GetComponent<Image>();
        if (img != null)
        {
            var c = img.color;
            c.a = a;
            img.color = c;
        }
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

        // 시작은 항상 0
        SetAlpha(0f);

        while (t < time)
        {
            // ★ 타임스케일 0에서도 동작하도록 변경
            t += Time.unscaledDeltaTime;

            float alpha = Mathf.Lerp(0f, defaultAlpha, t / time);
            SetAlpha(alpha);

            yield return null;
        }

        // 마지막 값 고정
        SetAlpha(defaultAlpha);
        fadeCoroutine = null;
        OnFadeEnd?.Invoke();
    }

}
