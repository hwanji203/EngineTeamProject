using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SharkGuid : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    private void Start()
    {
        text.DOFade(0, 0f);
        StartCoroutine(A());
    }

    private IEnumerator A()
    {
        yield return new WaitForSeconds(0.25f);
        text.DOFade(1, 1f);
        yield return new WaitForSeconds(6);
        text.DOFade(0, 3f);
    }
}
