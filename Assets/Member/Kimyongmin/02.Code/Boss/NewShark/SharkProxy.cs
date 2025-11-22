using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkProxy : MonoBehaviour
    {
        [SerializeField] private Animator biteEffectAnimator;
        [SerializeField] private GameObject roarEffect;
        private readonly int _biteHash = Animator.StringToHash("Bite");

        private SharkBrain _sharkBrain;
        private Shark _shark;
        [SerializeField] private SharkDasher sharkDasher;

        private Vector3 _dir;
        
        private void Awake()
        {
            _sharkBrain = GetComponentInParent<SharkBrain>();
            _shark = GetComponentInParent<Shark>();
        }

        public void AttackAnd()
        {
            _sharkBrain.SharkStateMachine.ChangeState(SharkStateType.Chase);
            _shark.AttackBool(false);
            _shark.SharkMovement.SetDashing(false);
        }

        public void ChaseEnter()
        {
            _sharkBrain.SharkStateMachine.ChangeState(SharkStateType.Chase);
        }

        public void OnBiteEffect()
        {
            biteEffectAnimator.gameObject.SetActive(true);
            biteEffectAnimator.SetTrigger(_biteHash);
        }

        public void OnRoarEffect()
        {
            roarEffect.SetActive(true);
        }

        public void ChargeReady()
        {
            _dir = _shark.GetTargetDir();
            StartCoroutine(sharkDasher.ChargeReady(_dir, _shark.SharkMovement));
        }
        
        public void ChargeAttack()
        {
            StartCoroutine(sharkDasher.ChargeAttack(_dir, _shark.SharkMovement));
        }
    }
}
