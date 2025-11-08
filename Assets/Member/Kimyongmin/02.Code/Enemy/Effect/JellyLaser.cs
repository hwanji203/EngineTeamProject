using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

namespace Member.Kimyongmin._02.Code.Enemy.Effect
{
    public class JellyLaser : MonoBehaviour
    {

        public void Shoot(float range)
        {
            Sequence s = DOTween.Sequence();
            
            s.Append(transform.DOScaleY(range, 0.25f).SetEase(Ease.InOutElastic));
            s.Append(transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutQuad));
            s.AppendCallback(() => Destroy(gameObject));
        }
    }
}
