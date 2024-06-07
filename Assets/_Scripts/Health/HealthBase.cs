using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts.Health
{
    public abstract class HealthBase : MonoBehaviour
    {
        protected TextMeshProUGUI HealthInPercents;
        protected Image FrontHealthBar;
        protected Image BackHealthBar;
        private const float MAXHealth = 100;
        private const float ChipSpeed = 4f;
        private float _health;
        private float _lerpTimer;
        private static float _damageСoefficient;
        private bool _isHasProtection;

        private void Start()
        {
            _health = MAXHealth;
            _damageСoefficient = 1f;
            _health = Mathf.Clamp(_health, 0, MAXHealth);
        }

        public abstract void Init();

        public void TakeDamage(float damage)
        {
            if (_isHasProtection == false)
            {
                _health -= damage * _damageСoefficient;
                //_lerpTimer = 0;
            }

            if (_health <= 0)
            {
                Died();
            }

            SetHealth();
            HealthBarLerp();
            ResetProperties();
        }

        public void RestoreHealth(float healthAmount)
        {
            _health += healthAmount;
            //_lerpTimer = 0;
            SetHealth();
            HealthBarLerp();

            Debug.Log(_health);
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
            
            Debug.Log(_damageСoefficient);
        }

        protected abstract void Died();

        private void SetHealth()
        {
            _health = Mathf.Clamp(_health, 0, MAXHealth);
        }

        private void HealthBarLerp()
        {
            var fillFront = FrontHealthBar.fillAmount;
            var fillBack = BackHealthBar.fillAmount;
            var valueFraction = _health / MAXHealth;

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

        private void ChangeFillAmount(float valueFraction, Image healthBar)
        {
            healthBar.fillAmount = valueFraction;
        }

        private void ChangeColor(Color color)
        {
            BackHealthBar.color = color;
        }
    }
} 
