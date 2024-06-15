using System;
using System.Collections;
using _Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Health
{
    public abstract class HealthBase : MonoBehaviour
    {
        public static event Action<float> OnChangedDamage;

        public float Health { get; private set; }
        protected TextMeshProUGUI HealthInPercents;
        protected Image FrontHealthBar;
        protected Image BackHealthBar;
        private float _didDamage;
        private const float MAXHealth = 100;
        private const float ChipSpeed = 4f;
        private static float _damageСoefficient;
        private float _lerpTimer;
        private bool _isHasProtection;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            Health = MAXHealth;
            _damageСoefficient = 1f;
            Health = Mathf.Clamp(Health, 0, MAXHealth);
        }

        protected abstract void Init();

        protected void TakeDamage(float damage)
        {
            if (_isHasProtection == false)
            {
                Health -= damage * _damageСoefficient;
                _didDamage += damage;
            }

            _lerpTimer = 0;

            if (Health <= 0) Died();

            SetHealth();
            HealthBarLerp();
        }

        public void RestoreHealth(float healthAmount)
        {
            Health += healthAmount;
            _lerpTimer = 0;
            SetHealth();
            HealthBarLerp();
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
            OnChangedDamage?.Invoke(_didDamage);
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
                //yield return null;
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