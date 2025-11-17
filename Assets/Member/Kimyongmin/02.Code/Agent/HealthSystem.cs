using System;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        private float _maxHealth;

        private float _health;
        
        public event Action OnHealthChanged;
        public event Action OnDeath;

        public bool Hit { get; private set; } = false;

        public float Health
        {
            get => _health;
            set
            {
                float before = _health;
                if (value != before)
                {
                    OnHealthChanged?.Invoke();
                    Hit = true;
                }
                
                _health = Mathf.Clamp(value, 0, _maxHealth);
            }
                
            
        }

        public void GetDamage(float damage)
        {
            Debug.Log("sdfd");
            Health -= damage;
            if (Health <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void SetHealth(float health)
        {
            _maxHealth = health;
            _health = _maxHealth;
        }
        
    }
}
