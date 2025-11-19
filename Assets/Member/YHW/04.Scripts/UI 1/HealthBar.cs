using Member.Kimyongmin._02.Code.Agent;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBarImage;

    HealthSystem healthSystem;
    Coroutine barAnimationCoroutine;

    float maxHealth = 100f;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
        UpdateBar();
        healthSystem.OnHealthChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        StartCoroutine(FirstCoroutine());
    }

    private IEnumerator FirstCoroutine()
    {
        yield return null;

        if (barAnimationCoroutine != null)
        {
            StopCoroutine(barAnimationCoroutine);
        }

        barAnimationCoroutine = StartCoroutine(BarAnimation());
    }

    private IEnumerator BarAnimation()
    {
        float targetFill = healthSystem.Health / maxHealth;

        float initialFill = healthBarImage.fillAmount;
        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float newFill = Mathf.Lerp(initialFill, targetFill, elapsedTime / duration);

            healthBarImage.fillAmount = newFill;

            yield return null;
        }

        healthBarImage.fillAmount = targetFill;
        barAnimationCoroutine = null;
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateBar;
        }
    }
}