using System;
using UnityEngine;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Enemy.SO;
using UnityEngine.InputSystem;

namespace Member.Kimyongmin._02.Code.Enemy.Effect
{
    public class JellyLaser : MonoBehaviour
    {
        [SerializeField] private EnemyDataSo enemyData;
        private static string _tagName = "Player";
        public void Shoot(float range)
        {
            Sequence s = DOTween.Sequence();
            
            
            s.Append(transform.DOScaleY(range, 0.25f).SetEase(Ease.InOutElastic));
            s.Append(transform.DOScaleY(1f, 0.15f).SetEase(Ease.OutQuad));
            s.AppendCallback(() => Destroy(gameObject));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_tagName))
            {
                if (other.TryGetcomponentInParent(out Player player))
                {
                    player.GetDamage(enemyData.damage, Vector2.zero);
                }
            }
        }
    }
}
