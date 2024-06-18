using System;
using _Scripts.Die;
using _Scripts.Items;
using _Scripts.Shooting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Health
{
    public class PlayerHealth : HealthBase
    {
        public new static event Action OnDied;

        [SerializeField] private TextMeshProUGUI _healthInPercents;
        [SerializeField] private Image _frontHealthBar;
        [SerializeField] private Image _backHealthBar;

        protected override void InitProperties()
        {
            HealthInPercents = _healthInPercents;
            FrontHealthBar = _frontHealthBar;
            BackHealthBar = _backHealthBar;
            CanvasGroup.Add(gameObject.GetComponent<CanvasGroup>());
        }

        protected override void Died()
        {
            DieManager.AddPlayerDies();
            OnDied?.Invoke();

            base.Died();
        }

        private void OnEnable()
        {
            BaseShoot.OnTakenDamageToPlayer += TakeDamage;
            HealGemItem.OnHealedPlayer += RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToPlayer += GetProtection;
        }

        private void OnDisable()
        {
            BaseShoot.OnTakenDamageToPlayer -= TakeDamage;
            HealGemItem.OnHealedPlayer -= RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToPlayer -= GetProtection;
        }
    }
}