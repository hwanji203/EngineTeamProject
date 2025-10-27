using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        [SerializeField] public float MaxHealth { get; private set; }

        private float _health;
        
        public event Action OnHealthChanged;

        public float Health
        {
            get => _health;
            set
            {
                float before = _health;
                if (value != before)
                    OnHealthChanged?.Invoke();
                
                _health = Mathf.Clamp(value, 0, MaxHealth);
                
            }
                
            
        }


        public void GetDamage(float damage)
        {
            Health -= damage;
        }
        
        
    }
}
