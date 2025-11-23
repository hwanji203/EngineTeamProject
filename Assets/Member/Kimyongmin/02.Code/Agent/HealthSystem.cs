using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Member.Kimyongmin._02.Code.Agent
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        private float _maxHealth;

        private float _health;

        private bool _isiIvincibility = false;
        
        public event Action OnHealthChanged;
        public event Action OnDeath;

        public bool Hit { get; private set; } = false;
        public bool IsDead { get; private set; } = false;

        public float Health
        {
            get => _health;
            set
            {
                float before = _health;
                if (value != before && !IsDead && !_isiIvincibility)
                {
                    OnHealthChanged?.Invoke();
                    CameraShaker.Instance.RandomShake(_health - value);
                    SoundManager.Instance.Play(SFXSoundType.EnemyGetDamage);
                    Hit = true;
                }
                
                if (!_isiIvincibility)
                    _health = Mathf.Clamp(value, 0, _maxHealth);
                
                Debug.Log(_health);
            }
        }
        public void GetDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0 && !IsDead)
            {
                OnDeath?.Invoke();
                IsDead = true;
            }
        }

        public void SetHealth(float health)
        {
            _maxHealth = health;
            _health = _maxHealth;
        }
        
        public float GetHealthPercent()
        {
            return Health / _maxHealth;
        }

        public void SetInvincibility(bool value)
        {
            _isiIvincibility = value;
        }

        public float HealthBarValue()
        {
            return Health / _maxHealth;
        }
    }
}
