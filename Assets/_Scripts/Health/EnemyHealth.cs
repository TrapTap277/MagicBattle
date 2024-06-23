using System;
using _Scripts.Die;
using _Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Health
{
    public class EnemyHealth : HealthBase
    {
        public new static event Action OnDied;

        [SerializeField] private CanvasGroup _healthBar;
        [SerializeField] private TextMeshProUGUI _healthInPercents;
        [SerializeField] private Image _frontHealthBar;
        [SerializeField] private Image _backHealthBar;

        protected override void InitProperties()
        {
            HealthInPercents = _healthInPercents;
            FrontHealthBar = _frontHealthBar;
            BackHealthBar = _backHealthBar;
            CanvasGroup.Add(_healthBar);
        }

        protected override void Died()
        {
            DieManager.AddEnemyDies();
            OnDied?.Invoke();
            base.Died();
        }

        protected override void OnEnable()
        {
            HealGemItem.OnHealedEnemy += RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToEnemy += GetProtection;
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            HealGemItem.OnHealedEnemy -= RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToEnemy -= GetProtection;
            base.OnDisable();
        }
    }
}