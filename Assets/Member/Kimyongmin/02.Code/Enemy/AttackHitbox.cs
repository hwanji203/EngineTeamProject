using System;
using System.Collections.Generic;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy
{
    public class AttackHitbox : MonoBehaviour
    {
        [SerializeField] private Transform[] hitbox;
        private List<Sequence> _sequenceList = new List<Sequence>();

        private HealthSystem _healthSystem;

        private float _hitboxY = 1;
        private void Awake()
        {
            _hitboxY = hitbox[0].localScale.y;
            
            _healthSystem = GetComponentInParent<HealthSystem>();
            
            _healthSystem.OnHealthChanged += ResetHitbox;
        }

        private void TalmoBeam(int index, Vector2 dir, float duration)
        {
            Sequence s = DOTween.Sequence().SetTarget(hitbox[index]);
            _sequenceList.Add(s);
         
            gameObject.SetActive(true);
            transform.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            hitbox[index].localScale = new Vector3(0, _hitboxY, 1);
            s.Append(hitbox[index].DOScaleX(1, duration - 0.1f).SetEase(Ease.OutQuad));
            s.AppendCallback(() =>
            {
                gameObject.SetActive(false);
                hitbox[index].localScale = new Vector3(0, hitbox[index].localScale.y, 1);
            });
        }
        
        private void TalmoAng(int index, float range, float duration)
        {
            Sequence s = DOTween.Sequence().SetTarget(hitbox[index]);
            _sequenceList.Add(s);
        
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
            if (_sequenceList.Count > 0)
            {
                for (int i = 0; i < hitbox.Length; i++)
                {
                    _sequenceList.ForEach(s => s.Kill());
                    hitbox[i].localScale = new Vector3(0, 0 ,1);
                }
                _sequenceList.Clear();
            }
        }

        private void OnDestroy()
        {
            _healthSystem.OnHealthChanged -= ResetHitbox;
        }
    }
}
