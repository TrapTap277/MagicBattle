using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Items;
using _Scripts.Shooting;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Health
{
    public abstract class HealthBase : MonoBehaviour, IShow, IInit
    {
        public static event Action<float> OnChangedDamage;
        public static event Action<bool> OnDied;
        public static event Action OnSetIsSomeoneDied;

        public float Health { get; private set; }
        protected readonly List<CanvasGroup> CanvasGroup = new List<CanvasGroup>();
        protected TextMeshProUGUI HealthInPercents;
        protected Image FrontHealthBar;
        protected Image BackHealthBar;
        private float _didDamage;
        private const float MAXHealth = 100;
        private const float ChipSpeed = 4f;
        private static float _damageСoefficient;
        private float _lerpTimer;
        private bool _isHasProtection;

        private const string AttackTag = "Attack";

        private readonly object _lockObject = new object();

        public void Init()
        {
            InitProperties();
        }

        public void Show()
        {
            foreach (var canvasGroup in CanvasGroup) canvasGroup.DOFade(1, 1);
        }

        private void Start()
        {
            Health = MAXHealth;
            _damageСoefficient = 1f;
            Health = Mathf.Clamp(Health, 0, MAXHealth);
        }

        protected abstract void InitProperties();


        public void TakeDamage(float damage)
        {
            lock (_lockObject)
            {
                OnDied?.Invoke(false);
                if (!_isHasProtection)
                {
                    var damageСoefficient = damage * _damageСoefficient;
                    Health -= damageСoefficient;
                    _didDamage += damageСoefficient;
                }

                _lerpTimer = 0;

                if (Health <= 0) Died();

                SetHealth();
                HealthBarLerp();
            }
        }

        public void RestoreHealth(float healthAmount)
        {
            lock (_lockObject)
            {
                Health += healthAmount;
                _lerpTimer = 0;
                SetHealth();
                HealthBarLerp();
            }
        }

        public void ResetProperties()
        {
            _isHasProtection = false;
            _damageСoefficient = 1f;
        }

        protected void GetProtection()
        {
            _isHasProtection = true;
        }

        protected static void TakeMoreDamage()
        {
            _damageСoefficient = Random.Range(1.1f, 2f);
        }

        protected virtual void Died()
        {
            OnDied?.Invoke(true);
            OnChangedDamage?.Invoke(_didDamage);
            OnSetIsSomeoneDied?.Invoke();
        }

        private void SetHealth()
        {
            Health = Mathf.Clamp(Health, 0, MAXHealth);
        }

        private void HealthBarLerp()
        {
            var fillFront = FrontHealthBar.fillAmount;
            var fillBack = BackHealthBar.fillAmount;
            var valueFraction = Health / MAXHealth;

            if (fillBack > valueFraction)
            {
                ChangeColor(Color.red);
                ChangeFillAmount(valueFraction, FrontHealthBar);
                StartCoroutine(SetFillWithLerp(fillBack, valueFraction, BackHealthBar));
            }

            if (fillFront < valueFraction)
            {
                ChangeColor(Color.green);
                ChangeFillAmount(valueFraction, BackHealthBar);
                StartCoroutine(SetFillWithLerp(fillFront, BackHealthBar.fillAmount, FrontHealthBar));
            }
        }

        private IEnumerator SetFillWithLerp(float fill, float value, Image healthBar)
        {
            _lerpTimer = 0f;
            while (_lerpTimer <= ChipSpeed)
            {
                yield return new WaitForEndOfFrame();
                _lerpTimer += Time.deltaTime * ChipSpeed;
                var percentComplete = _lerpTimer / ChipSpeed;
                percentComplete *= percentComplete;
                healthBar.fillAmount = Mathf.Lerp(fill, value, percentComplete);
                var percents = Mathf.RoundToInt(healthBar.fillAmount * 100).ToString();
                HealthInPercents.text = $"{percents}%";
                SetHealth();
            }
        }

        private static void ChangeFillAmount(float valueFraction, Image healthBar)
        {
            healthBar.fillAmount = valueFraction;
        }

        private void ChangeColor(Color color)
        {
            BackHealthBar.color = color;
        }

        private void OnEnable()
        {
            DamageGemItem.OnTakeMoreDamage += TakeMoreDamage;
        }

        private void OnDisable()
        {
            DamageGemItem.OnTakeMoreDamage -= TakeMoreDamage;
        }
    }
}