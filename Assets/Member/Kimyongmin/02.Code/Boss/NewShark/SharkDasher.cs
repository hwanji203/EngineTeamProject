using System;
using System.Collections;
using Member.Kimyongmin._02.Code.Enemy;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkDasher : MonoBehaviour
    {
        private Shark _shark;
        private AttackHitbox _hitbox;

        private void Awake()
        {
            _shark = GetComponentInParent<Shark>();
            _hitbox = GetComponentInChildren<AttackHitbox>();
        }

        public IEnumerator ChargeReady(Vector3 target)
        {
            float duration = 2.5f;
            float t = 0;
            
            _hitbox.ShowHitbox(target,duration);

            while (t < duration)
            {
                t += Time.deltaTime;
                
                _shark.SharkMovement.SetSpeed(-1);
                _shark.SharkMovement.LongDash(target);
                yield return new WaitForFixedUpdate();
            }
        }
        
        public IEnumerator ChargeAttack(Vector3 target)
        {
            float duration = 5;
            float t = 0;

            while (t < duration)
            {
                t += Time.deltaTime;
                
                _shark.SharkMovement.SetSpeed(20);
                _shark.SharkMovement.LongDash(target);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
