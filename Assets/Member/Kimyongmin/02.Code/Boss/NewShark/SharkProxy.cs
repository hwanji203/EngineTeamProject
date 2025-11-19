using UnityEngine;

namespace Member.Kimyongmin._02.Code.Boss.NewShark
{
    public class SharkProxy : MonoBehaviour
    {
        [SerializeField] private Animator biteEffectAnimator;
        private readonly int _biteHash = Animator.StringToHash("Bite");

        public void OnBiteEffect()
        {
            biteEffectAnimator.gameObject.SetActive(true);
            biteEffectAnimator.SetTrigger(_biteHash);
        }
    }
}
