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
        public static event Action OnDied;
        
        [SerializeField] private TextMeshProUGUI _healthInPercents;
        [SerializeField] private Image _frontHealthBar;
        [SerializeField] private Image _backHealthBar;

        private readonly DieCounter _dieCounter = new DieCounter();
        
        private void Awake()
        {
            Init();
        }
        
        public override void Init()
        {
            HealthInPercents = _healthInPercents;
            FrontHealthBar = _frontHealthBar;
            BackHealthBar = _backHealthBar;
        }

        protected override void Died()
        {
            DieCounter.AddPlayerDies();
            OnDied?.Invoke();
        }

        private void OnEnable()
        {
            BaseShoot.OnTakenDamageToPlayer += TakeDamage; 
            HealGemItem.OnHealedPlayer += RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToPlayer += GetProtection;
            DamageGemItem.OnTakeMoreDamage += TakeMoreDamage;
        }

        private void OnDisable()
        {
            BaseShoot.OnTakenDamageToPlayer -= TakeDamage;
            HealGemItem.OnHealedPlayer -= RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToPlayer -= GetProtection;
            DamageGemItem.OnTakeMoreDamage -= TakeMoreDamage;
        }
    }
}