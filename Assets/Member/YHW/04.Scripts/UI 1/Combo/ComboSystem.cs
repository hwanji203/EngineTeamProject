using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ComboSystem : MonoSingleton<ComboSystem>
{
    [SerializeField] private float comboDuration = 1.5f;  
    [SerializeField] private float scaleUpSize = 1.3f;  
    [SerializeField] private float scaleDuration = 0.2f; 

    [SerializeField] private GameObject comboPrefab;  
    [SerializeField] private Transform spawnParent;

    private GameObject currentComboObj; 
    private TextMeshProUGUI comboText;
    private TextMeshProUGUI counterText;
    private Vector3 originalScale;
    private int currentCombo = 0;
    private Coroutine comboCoroutine;
    private RectTransform comboRectTransform;
    private Image comboImage;

    private void Start()
    {
        GameManager.Instance.Player.AttackCompo.OnAttack += DoCombo;
    }

    private void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            DoCombo(true);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            //Counter();
        }
    }

    public void DoCombo(bool isCounter)
    {
        if (currentComboObj == null)
        {
            currentComboObj = Instantiate(comboPrefab, spawnParent);
            originalScale = currentComboObj.transform.localScale;
            comboRectTransform = currentComboObj.GetComponentInChildren<RectTransform>();
            comboImage = currentComboObj.GetComponentInChildren<Image>();
            TextMeshProUGUI[] texts = currentComboObj.GetComponentsInChildren<TextMeshProUGUI>(true);
            comboText = texts[0];
            counterText = texts[1];
        }

        currentCombo++;
        comboText.text = "x" + currentCombo.ToString();

        currentComboObj.transform.DOKill();

        comboRectTransform
            .DOScale(originalScale * scaleUpSize, scaleDuration)
            .SetEase(Ease.OutBack).UI()
            .OnComplete(() =>
            {
                currentComboObj.transform.DOScale(originalScale, scaleDuration).SetEase(Ease.InBack).UI();
            });

        if (comboCoroutine != null)
            StopCoroutine(comboCoroutine);
        comboCoroutine = TimeManager.Instance.StartUICoroutine(FillRoutine());

        if (isCounter)
        {
            counterText.gameObject.SetActive(true);
            isCounter = false;
        }
        else
        {
            counterText.gameObject.SetActive(false);
        }
    }

    IEnumerator FillRoutine()
    {
        float timer = 0f;
        comboImage.fillAmount = 1f;

        while (timer < comboDuration)
        {
            timer += Time.deltaTime;
            comboImage.fillAmount = 1f - (timer / comboDuration);
            yield return null;
        }


        EndCombo();
    }
    private void EndCombo()
    {
        currentCombo = 0;

        if (currentComboObj != null)
        {
            currentComboObj.transform.DOKill();

            Destroy(currentComboObj);
            currentComboObj = null;
        }
    }

    
}
