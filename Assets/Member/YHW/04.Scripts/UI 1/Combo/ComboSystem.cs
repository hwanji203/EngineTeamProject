using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ComboSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float comboDuration = 1.5f;  
    [SerializeField] private float scaleUpSize = 1.3f;  
    [SerializeField] private float scaleDuration = 0.2f; 

    [Header("References")]
    [SerializeField] private GameObject comboPrefab;  
    [SerializeField] private Transform spawnParent;

    private string comboAnimStateName;

    private GameObject currentComboObj; 
    private Animator comboAnimator;
    private TextMeshPro comboText;
    private Vector3 originalScale;
    private float baseAnimLength = 1f;
    private int currentCombo = 0;
    private Coroutine comboCoroutine;

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
            comboAnimator = currentComboObj.GetComponent<Animator>();
            comboText = currentComboObj.GetComponentInChildren<TextMeshPro>(true);
            originalScale = currentComboObj.transform.localScale;

            GetBaseAnimationLength();
        }

        currentCombo++;
        comboText.text = "x" + currentCombo.ToString();

        currentComboObj.transform.DOKill();

        currentComboObj.transform
            .DOScale(originalScale * scaleUpSize, scaleDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                currentComboObj.transform.DOScale(originalScale, scaleDuration).SetEase(Ease.InBack);
            });

        if (comboAnimator != null)
        {
            comboAnimator.speed = baseAnimLength / comboDuration;
            if (!string.IsNullOrEmpty(comboAnimStateName))
            {
                comboAnimator.Play(comboAnimStateName, 0, 0f);
            }
        }

        if (comboCoroutine != null)
            StopCoroutine(comboCoroutine);
        comboCoroutine = StartCoroutine(ComboTimeout());
    }

    private IEnumerator ComboTimeout()
    {
        yield return new WaitForSeconds(comboDuration);
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

    private void GetBaseAnimationLength()
    {
        if (comboAnimator.runtimeAnimatorController != null)
        {
            var clips = comboAnimator.runtimeAnimatorController.animationClips;
            if (clips != null && clips.Length > 0)
            {
                baseAnimLength = clips[0].length;
                comboAnimStateName = clips[0].name;
            }
        }
    }
}
