using Member.Kimyongmin._02.Code.Agent;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBarImage;

    HealthSystem healthSystem;

    Coroutine barAnimationCoroutine;

    private void Awake()
    {
        healthBarImage = GetComponent<Image>();
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
    }

    private void Start()
    {
       

        healthSystem.OnHealthChanged += UpdateBar;
    }

    private void UpdateBar()
    {
        StartCoroutine(UpdateAndAnimateBar());
    }

    private IEnumerator UpdateAndAnimateBar()
    {
        yield return null;

        float targetFill = healthSystem.Health / 100f;

        if (barAnimationCoroutine != null)
        {
            StopCoroutine(barAnimationCoroutine);
        }
        barAnimationCoroutine = StartCoroutine(AnimateHealthBar(targetFill));
    }
    private IEnumerator AnimateHealthBar(float targetFill)
    {
        float startFill = healthBarImage.fillAmount; 
        float elapsedTime = 0f;

        while (elapsedTime < 0.1f)
        {
            float t = elapsedTime / 0.1f;

            healthBarImage.fillAmount = Mathf.Lerp(startFill, targetFill, t);

            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        Debug.Log(healthBarImage.fillAmount);

        healthBarImage.fillAmount = targetFill;
    }
    private void OnDestroy()
    {
        if (healthSystem != null)
        {
            healthSystem.OnHealthChanged -= UpdateBar;
        }
    }
}
