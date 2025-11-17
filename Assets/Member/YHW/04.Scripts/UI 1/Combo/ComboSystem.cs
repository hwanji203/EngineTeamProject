using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private float comboDuration = 1.5f;  
    [SerializeField] private float scaleUpSize = 1.3f;  
    [SerializeField] private float scaleDuration = 0.2f; 

    [SerializeField] private GameObject comboPrefab;  
    [SerializeField] private Transform spawnParent;


    private GameObject currentComboObj; 
    private TextMeshProUGUI comboText;
    private Vector3 originalScale;
    private int currentCombo = 0;
    private Coroutine comboCoroutine;
    private RectTransform comboRectTransform;
    private Image comboImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCombo();
        }
    }

    public void AddCombo()
    {

        if (currentComboObj == null)
        {
            currentComboObj = Instantiate(comboPrefab, spawnParent);
            comboText = currentComboObj.GetComponentInChildren<TextMeshProUGUI>(true);
            originalScale = currentComboObj.transform.localScale;
            comboRectTransform = currentComboObj.GetComponentInChildren<RectTransform>();
            comboImage = currentComboObj.GetComponentInChildren<Image>();
        }

        currentCombo++;
        comboText.text = "x" + currentCombo.ToString();

        currentComboObj.transform.DOKill();

        comboRectTransform
            .DOScale(originalScale * scaleUpSize, scaleDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                currentComboObj.transform.DOScale(originalScale, scaleDuration).SetEase(Ease.InBack);
            });

        if (comboCoroutine != null)
            StopCoroutine(comboCoroutine);
        comboCoroutine = StartCoroutine(FillRoutine());

       
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
