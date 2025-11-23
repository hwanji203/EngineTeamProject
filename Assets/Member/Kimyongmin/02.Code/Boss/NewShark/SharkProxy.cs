using System;
using UnityEngine;
using UnityEngine.Events;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkProxy : MonoBehaviour
    {
        [SerializeField] private Animator biteEffectAnimator;
        [SerializeField] private PointEffector2D roarEffect;
        private readonly int _biteHash = Animator.StringToHash("Bite");

        private SharkBrain _sharkBrain;
        private Shark _shark;
        [SerializeField] private SharkDasher sharkDasher;

        public UnityEvent OnSharkFightEnd;
        public UnityEvent<UIType> OnSharkFightEndUI;

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
            SoundManager.Instance.Play(SFXSoundType.SharkBite);
            biteEffectAnimator.gameObject.SetActive(true);
            biteEffectAnimator.SetTrigger(_biteHash);
        }

        public void OnRoarEffect()
        {
            if (_shark.RoarDir > 0)
                roarEffect.forceMagnitude = -200;
            else
                roarEffect.forceMagnitude = 200;
            
            roarEffect.gameObject.SetActive(true);
        }
        
        public void OffRoarEffect()
        {
            roarEffect.gameObject.SetActive(false);
        }

        public void ChargeReady()
        {
            _dir = _shark.GetTargetDir();
            StartCoroutine(sharkDasher.ChargeReady(_dir));
        }
        
        public void ChargeAttack()
        {
            StartCoroutine(sharkDasher.ChargeAttack(_dir));
        }

        public void BossFightEnd()
        {
            OnSharkFightEnd?.Invoke();
            OnSharkFightEndUI?.Invoke(UIType.ClearUI);
        }

        public void LaserPointSound()
        {
            SoundManager.Instance.Play(SFXSoundType.SharkLaserPointer);
        }
        
        public void LaserSound()
        {
            SoundManager.Instance.Play(SFXSoundType.SharkLaser);
        }

        public void ShortsRoarSound()
        {
            SoundManager.Instance.Play(SFXSoundType.SharkRoarShorts);
        }
        
        public void LongRoarSound()
        {
            SoundManager.Instance.Play(SFXSoundType.SharkRoar);
        }
    }
}
