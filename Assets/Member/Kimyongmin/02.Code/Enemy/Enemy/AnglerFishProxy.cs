using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class AnglerFishProxy : MonoBehaviour
    {
        private AnglerFish _anglerFish;

        private void Awake()
        {
            _anglerFish = GetComponentInParent<AnglerFish>();
        }

        public void HitPan()
        {
            StartCoroutine(_anglerFish.HitPanJeong());
        }

        public void AttackEnd()
        {
            _anglerFish.PanjeongTime = 0;
            _anglerFish.IsAttack = false;
            _anglerFish.transform.rotation = Quaternion.Euler(_anglerFish.transform.rotation.x,_anglerFish.transform.rotation.y,0);
        }
    }
}
