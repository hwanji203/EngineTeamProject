using System;
using Member.Kimyongmin._02.Code.Agent;
using UnityEngine;
using UnityEngine.UI;

namespace Member.Kimyongmin._02.Code
{
    public class BossHealthBar : MonoBehaviour
    {
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private Sprite[] barSprites;

        private float _t = 0;

        private void Update()
        {
            if (healthSystem.ReturnInvi())
                healthBarImage.sprite = barSprites[0];
            else
                healthBarImage.sprite = barSprites[1];
            
            float value = healthSystem.HealthBarValue();

            healthBarImage.fillAmount = Mathf.SmoothDamp(
                healthBarImage.fillAmount, value, ref _t, 0.15f);
        }
    }
}
