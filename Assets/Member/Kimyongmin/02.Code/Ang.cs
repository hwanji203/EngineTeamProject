using UnityEngine;
using DG.Tweening;

public class Ang : MonoBehaviour
{
    private void Start()
    {
        transform.DOMoveX(5, 1f).SetRelative(true).SetEase(Ease.Linear);
    }
}
