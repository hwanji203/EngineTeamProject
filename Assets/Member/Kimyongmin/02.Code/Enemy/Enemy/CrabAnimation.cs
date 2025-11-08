using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class CrabAnimation : MonoBehaviour
    {
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        
        private Animator _animator;

        [SerializeField] private Transform stone;
        [SerializeField] private Transform[] stonePos;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayerAttackAnim()
        {
            _animator.SetTrigger(AttackHash);
        }

        public void StoneMove(int i)
        {
            stone.position = stonePos[i].position;
        }

        public void StoneSetActiveOn()
        {
            stone.gameObject.SetActive(true);
        }
        public void StoneSetActiveOff()
        {
            stone.gameObject.SetActive(false);
        }
    }
}
