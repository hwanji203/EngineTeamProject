using System.Collections;
using DG.Tweening;
using Member.Kimyongmin._02.Code.Boss.SO;
using Member.Kimyongmin._02.Code.Enemy;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkSkills : MonoBehaviour
    {
        public AttackHitbox AttackHitbox { get; private set; }
        
        private Collider2D[] _hitArr;

        private void Awake()
        {
            AttackHitbox = GetComponentInChildren<AttackHitbox>();
        }

        public void Bite(Vector2 dir, float delay, LayerMask layerMask, SharkDataSO sharkData)
        {
            StartCoroutine(BiteCor(dir, delay, layerMask, sharkData));
        }

        private IEnumerator BiteCor(Vector2 dir, float delay, LayerMask layerMask, SharkDataSO sharkData)
        {
            transform.DOKill(true);
            
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            AttackHitbox.ShowHitbox(transform.right, delay);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(HitPanJeong(angle, layerMask, sharkData));
        }

        private float _panjeongTime = 0;
        private float _panjeongDuration = 0.1f;
        private IEnumerator HitPanJeong(float angle, LayerMask layerMask, SharkDataSO sharkData)
        {
            while (_panjeongTime < _panjeongDuration)
            {
                _panjeongTime += Time.deltaTime;
                _hitArr = Physics2D.OverlapBoxAll(AttackHitbox.transform.position, new Vector2(3.5f,2.5f), angle, layerMask);
                if (_hitArr.Length > 0)
                {
                    foreach (var item in _hitArr)
                    {
                        if (item.TryGetcomponentInParent(out Player player))
                        {
                            DealStamina(player, sharkData.NormalAttackDamage);
                            break;
                        }
                    }
                }   
                yield return null;
            }
        }

        public void DealStamina(Player player, float damage)
        {
            player.GetDamage(damage, transform.position);
        }
    }
}
