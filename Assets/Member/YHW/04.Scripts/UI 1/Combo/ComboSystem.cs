using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GameManager.Instance.Player.AttackCompo;
        playerAttack.OnAttack += DoCombo;
    }

    protected override void OnDestroy()
    {
        playerAttack.OnAttack -= DoCombo;
    }

    // Unity 오브젝트가 진짜로 살아있는지 체크하는 헬퍼
    private bool IsValid(UnityEngine.Object obj)
    {
        return obj != null && !obj.Equals(null);
    }

    public void DoCombo(bool isCounter)
    {
        Debug.Log(this);
        Debug.Log(currentCombo);
        // 1) 콤보 오브젝트가 없거나, 이미 Destroy 된 상태면 새로 생성
        if (!IsValid(currentComboObj))
        {
            Transform parent = IsValid(spawnParent) ? spawnParent : transform;
            currentComboObj = Instantiate(comboPrefab, parent);

            originalScale = currentComboObj.transform.localScale;

            comboRectTransform = currentComboObj.GetComponentInChildren<RectTransform>();
            comboImage = currentComboObj.GetComponentInChildren<Image>();

            TextMeshProUGUI[] texts = currentComboObj.GetComponentsInChildren<TextMeshProUGUI>(true);
            if (texts.Length >= 2)
            {
                comboText = texts[0];
                counterText = texts[1];
            }
            else
            {
                comboText = null;
                counterText = null;
            }
        }

        currentCombo++;

        if (comboText != null && !comboText.Equals(null))
        {
            comboText.text = "x" + currentCombo.ToString();
        }

        // 2) 이전 트윈들 모두 정리
        if (IsValid(currentComboObj))
        {
            currentComboObj.transform.DOKill();
        }

        // 3) 스케일 업/다운 연출
        if (IsValid(comboRectTransform))
        {
            comboRectTransform
                .DOScale(originalScale * scaleUpSize, scaleDuration)
                .SetEase(Ease.OutBack).UI()
                .OnComplete(() =>
                {
                    if (IsValid(comboRectTransform))
                    {
                        comboRectTransform
                            .DOScale(originalScale, scaleDuration)
                            .SetEase(Ease.InBack).UI();
                    }
                });
        }

        // 4) 기존 코루틴 정지 후 새로 시작
        if (comboCoroutine != null)
            StopCoroutine(comboCoroutine);
        comboCoroutine = StartCoroutine(FillRoutine());

        // 5) 카운터 텍스트 on/off
        if (IsValid(counterText))
        {
            if (isCounter)
            {
                counterText.gameObject.SetActive(true);
            }
            else
            {
                counterText.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator FillRoutine()
    {
        float timer = 0f;

        if (!IsValid(comboImage))
            yield break;

        comboImage.fillAmount = 1f;

        while (timer < comboDuration)
        {
            // 도중에 오브젝트/이미지가 파괴되면 바로 종료
            if (!IsValid(comboImage))
                yield break;

            timer += Time.unscaledDeltaTime;
            comboImage.fillAmount = 1f - (timer / comboDuration);
            yield return null;
        }

        EndCombo();
    }

    private void EndCombo()
    {
        currentCombo = 0;

        if (IsValid(currentComboObj))
        {
            currentComboObj.transform.DOKill();
            Destroy(currentComboObj);
        }

        currentComboObj = null;
        comboRectTransform = null;
        comboImage = null;
        comboText = null;
        counterText = null;
        comboCoroutine = null;
    }
}
