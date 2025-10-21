using UnityEngine;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] public float MaxHealth { get; private set; }

        private float health;

        public float Health
        {
            get => health;
            set => Mathf.Clamp(value, 0, MaxHealth);
        }


        public void GetDamage(float damage)
        {
            Health -= damage;
        }
    }
}
