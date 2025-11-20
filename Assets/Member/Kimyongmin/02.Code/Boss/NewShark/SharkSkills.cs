using System;
using System.Collections;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Boss.SO;
using Member.Kimyongmin._02.Code.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkSkills : MonoBehaviour
    {
        public AttackHitbox AttackHitbox { get; private set; }
        private SharkLaser _laser;

        private void Awake()
        {
            AttackHitbox = GetComponentInChildren<AttackHitbox>();
            _laser = GetComponentInChildren<SharkLaser>();
        }

        public void LaserFocusOn(Vector3 dir)
        {
            StartCoroutine(_laser.FocusOn(dir));
        }
        public void Bite(float delay, LayerMask layerMask, SharkDataSO sharkData, Action<Vector3,float> callback, Vector3 dir, float power)
        {
            StartCoroutine(BiteCor(delay, layerMask, sharkData, callback, dir,power));
        }

        private IEnumerator BiteCor(float delay, LayerMask layerMask, SharkDataSO sharkData, Action<Vector3,float> callback,  Vector3 dir, float power)
        {
            transform.DOKill(true);
            
            AttackHitbox.ShowHitbox(transform.right, delay);
            yield return new WaitForSeconds(delay);
            callback(dir,power);
            StartCoroutine(HitPanJeong(layerMask, sharkData));
        }

        private float _panjeongTime = 0;
        private float _panjeongDuration = 0.05f;
        private IEnumerator HitPanJeong(LayerMask layerMask, SharkDataSO sharkData)
        {
            while (_panjeongTime < _panjeongDuration)
            {
                _panjeongTime += Time.deltaTime;
                Collider2D[] hitArr = Physics2D.OverlapBoxAll(AttackHitbox.transform.position, new Vector2(5.5f,2.5f),0, layerMask);
                if (hitArr.Length > 0)
                {
                    foreach (var item in hitArr)
                    {
                        if (item.TryGetcomponentInParent(out Player player))
                        {
                            DealStamina(player, sharkData.NormalAttackDamage);
                            _panjeongTime = _panjeongDuration;
                            break;
                        }
                    }
                }   
                yield return null;
            }

            _panjeongTime = 0;
        }

        public void DealStamina(Player player, float damage)
        {
            player.GetDamage(damage, transform.position);
        }
    }
}
