using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class AnglerFishProxy : MonoBehaviour
    {
        private AnglerFish _anglerFish;
        [SerializeField] private Animator biteAnimator;
        
        private readonly int _biteHash =  Animator.StringToHash("Bite");

        private void Start()
        {
            _anglerFish = GetComponentInParent<AnglerFish>();

            _anglerFish.HealthSystem.OnHealthChanged += AttackEnd;
        }

        public void HitPan()
        {
            StartCoroutine(_anglerFish.HitPanJeong());
            biteAnimator.gameObject.SetActive(true);
            biteAnimator.SetTrigger(_biteHash);
        }

        public void AttackEnd()
        {
            _anglerFish.PanjeongTime = 0;
            _anglerFish.IsAttack = false;
            _anglerFish.transform.rotation = Quaternion.Euler(_anglerFish.transform.rotation.x,_anglerFish.transform.rotation.y,0);
        }

        private void OnDestroy()
        {
            _anglerFish.HealthSystem.OnHealthChanged -= AttackEnd;
        }
    }
}
