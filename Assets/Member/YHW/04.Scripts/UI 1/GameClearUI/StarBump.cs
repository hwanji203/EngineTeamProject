using DG.Tweening;
using System.Collections;
using UnityEngine;

public class StarBump : MonoBehaviour
{
    [SerializeField] private float bumpScale = 1.2f;
    [SerializeField] private float bumpDuration = 0.2f;
    [SerializeField] private float delayDuration = 0.1f;

    private void OnEnable()
    {
        TimeManager.Instance.StartUICoroutine(Bump());
    }
    private IEnumerator Bump()
    {
        yield return new WaitForSeconds(delayDuration);

        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.DOScale(bumpScale + 0.3f, bumpDuration / 2)
                                   .SetEase(Ease.OutQuad).UI());

        mySequence.Append(transform.DOScale(bumpScale, bumpDuration / 2)
                                   .SetEase(Ease.InQuad).UI());

        mySequence.Play();


    }
}
