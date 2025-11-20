using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkProxy : MonoBehaviour
    {
        [SerializeField] private Animator biteEffectAnimator;
        private readonly int _biteHash = Animator.StringToHash("Bite");

        private SharkBrain _sharkBrain;
        private Shark _shark;

        private void Awake()
        {
            _sharkBrain = GetComponentInParent<SharkBrain>();
            _shark = GetComponentInParent<Shark>();
        }

        public void NormalBiteAnd()
        {
            _sharkBrain.SharkStateMachine.ChangeState(SharkStateType.Chase);
            _shark.AttackBool(false);
        }

        public void OnBiteEffect()
        {
            biteEffectAnimator.gameObject.SetActive(true);
            biteEffectAnimator.SetTrigger(_biteHash);
        }
    }
}
