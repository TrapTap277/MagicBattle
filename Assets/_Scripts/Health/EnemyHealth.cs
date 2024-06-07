using System;
using _Scripts.Die;
using _Scripts.Items;
using _Scripts.Shooting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Health
{
    public class EnemyHealth : HealthBase
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
            DieCounter.AddEnemyDies();
            OnDied?.Invoke();
        }

        private void OnEnable()
        {
            BaseShoot.OnTakenDamageToEnemy += TakeDamage; 
            HealGemItem.OnHealedEnemy += RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToEnemy += GetProtection;
            DamageGemItem.OnTakeMoreDamage += TakeMoreDamage;
        }

        private void OnDisable()
        {
            BaseShoot.OnTakenDamageToEnemy -= TakeDamage;
            HealGemItem.OnHealedEnemy -= RestoreHealth;
            ProtectionGemItem.OnGivenProtectionToEnemy -= GetProtection;
            DamageGemItem.OnTakeMoreDamage -= TakeMoreDamage;
        }
    }
}