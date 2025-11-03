using System;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy
{
    public class AttackHitbox : MonoBehaviour
    {
        [SerializeField] private Transform[] hitbox;

        private HealthSystem _healthSystem;
        private void Awake()
        {
            _healthSystem = GetComponentInParent<HealthSystem>();
            
            _healthSystem.OnHealthChanged += ResetHitbox;
        }

        private void TalmoBeam(int index, Vector2 dir, float duration)
        {
            Sequence s = DOTween.Sequence();
        
            gameObject.SetActive(true);
            transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            s.Join(hitbox[index].DOScaleX(1, duration).SetEase(Ease.OutQuad));
            s.AppendCallback(() =>
            {
                gameObject.SetActive(false);
                hitbox[index].localScale = new Vector3(0, hitbox[index].localScale.y, 1);
            });
        }
        
        private void TalmoAng(int index, float range, float duration)
        {
            Sequence s = DOTween.Sequence();
        
            gameObject.SetActive(true);
            s.Append(hitbox[index].DOScale(range , duration - 0.1f).SetEase(Ease.OutQuad));
            s.AppendCallback(() =>
            {
                gameObject.SetActive(false);
                hitbox[index].localScale = new Vector3(0, 0, 1);
            });
        }
    
        public void ShowHitbox(Vector2 dir, float duration)
        {
            for (int i = 0; i < hitbox.Length; i++)
            {
                TalmoBeam(i,dir,duration);
            }
        }
        public void ShowHitbox(float duration, float range)
        {
            for (int i = 0; i < hitbox.Length; i++)
            {
                TalmoAng(i, range, duration);
            }
        }

        private void ResetHitbox()
        {
            for (int i = 0; i < hitbox.Length; i++)
            {
                transform.DOKill();
                hitbox[i].localScale = new Vector3(0, 0, 1);
            }
        }

        private void OnDestroy()
        {
            _healthSystem.OnHealthChanged -= ResetHitbox;
        }
    }
}
