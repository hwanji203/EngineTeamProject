using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class StaminaGaugeUI : MonoBehaviour, IUI
{
    private float maxAmount;
    [SerializeField] private Image fillImage;
    [SerializeField] private float lerpTime;
    [field: SerializeField] public GameObject UIObject { get; private set; }

    public UIType UIType { get => UIType.GaugeUI; }

    private float lastValue;
    private Coroutine colorCoroutine;
    private Coroutine lerpCoroutine;
    public void Initialize() { }

    private void SetGauge(float value)
    {
        if (lerpCoroutine != null)
        {
            StopCoroutine(lerpCoroutine);
            lerpCoroutine = null;
        }
        lerpCoroutine = StartCoroutine(LerpHpBar(value));
    }

    private IEnumerator LerpHpBar(float value)
    {
        // lerpTime 동안 fillImage를 value만큼 바꾸기 (스르륵 올라가게)

        // 현재 게이지 값을 정규화합니다. (0 ~ 1)
        float currentFillAmount = lastValue / maxAmount;
        // 목표 게이지 값을 정규화합니다. (0 ~ 1)
        float targetFillAmount = value / maxAmount;

        // 경과 시간을 추적할 변수입니다.
        float elapsedTime = 0f;

        // lastValue를 value로 업데이트하기 전에 코루틴이 시작될 때의 값을 저장합니다.
        lastValue = value;

        // 경과 시간이 설정된 보간 시간(lerpTime)보다 작을 때까지 반복합니다.
        while (elapsedTime < lerpTime)
        {
            // 시간을 누적합니다.
            elapsedTime += Time.deltaTime;

            // 보간 비율을 계산합니다. elapsedTime / lerpTime은 0에서 1까지 부드럽게 증가합니다.
            float t = elapsedTime / lerpTime;

            // T값에 보간 곡선을 적용하여 시작과 끝에서 느려지는 효과를 줄 수 있습니다.
            // 예를 들어, 부드러운 In-Out 효과를 위해 t = t * t * (3f - 2f * t); 를 사용할 수 있습니다.
            // 여기서는 간단하게 선형 보간을 사용합니다.
            // 현재 fillAmount에서 목표 targetFillAmount까지 t 비율만큼 보간합니다.
            fillImage.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, t);

            // 다음 프레임까지 기다립니다.
            yield return null;
        }

        // 루프가 끝난 후, 정확히 목표 값으로 설정하여 오차를 제거합니다.
        fillImage.fillAmount = targetFillAmount;
        lerpCoroutine = null;
    }

    public void Open()
    {
        UIObject.SetActive(true);
    }

    public void Close()
    {
        UIObject.SetActive(false);
    }

    public void LateInitialize()
    {
        maxAmount = GameManager.Instance.Player.StatSO.MaxStamina;
        GameManager.Instance.Player.StaminaCompo.CurrentStamina.OnValueChange += SetGauge;
        lastValue = 0;
        SetGauge(maxAmount);
        fillImage.color = Color.cyan;
        GameManager.Instance.Player.OnDamage += StartWhite;
        GameManager.Instance.Player.OnHeal += StartGreen;
        Open();
    }

    private void StartWhite(float _, Vector2 __)
    {
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
        }
        colorCoroutine = StartCoroutine(WhiteBar());
    }

    private void StartGreen()
    {
        if (colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
        }
        colorCoroutine = StartCoroutine(GreenBar());
    }

    private IEnumerator WhiteBar()
    {
        fillImage.color = Color.white;
        yield return new WaitForSeconds(lerpTime);
        fillImage.color = Color.cyan;
        colorCoroutine = null;
    }

    private IEnumerator GreenBar()
    {
        fillImage.color = Color.green;
        yield return new WaitForSeconds(lerpTime);
        fillImage.color = Color.cyan;
        colorCoroutine = null;
    }
}
