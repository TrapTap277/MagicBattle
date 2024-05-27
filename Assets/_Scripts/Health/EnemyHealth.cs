using System;
using System.Collections;
using _Scripts.Shooting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float _health { get; private set; }
    private float _lerpTimer;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _chipSpeed = 2f;

    [SerializeField] private Image _frontHealthBar;
    [SerializeField] private Image _backHealthBar;

    [SerializeField] private TextMeshProUGUI _healthInPercents;

    void Start()
    {
        _health = _maxHealth;
    }

    void Update()
    {
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = _frontHealthBar.fillAmount;
        float fillB = _backHealthBar.fillAmount;
        float hFraction = _health / _maxHealth;

        if (fillB > hFraction)
        {
            _frontHealthBar.fillAmount = hFraction;
            _backHealthBar.color = Color.red;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;
            _backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);

            string percents = Mathf.RoundToInt(_backHealthBar.fillAmount * 100).ToString();

            _healthInPercents.text = $"{percents}%";
        }

        if (fillF < hFraction)
        {
            _backHealthBar.color = Color.green;
            _backHealthBar.fillAmount = hFraction;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;

            _frontHealthBar.fillAmount = Mathf.Lerp(fillF, _backHealthBar.fillAmount, percentComplete);

            string percents = Mathf.RoundToInt(_frontHealthBar.fillAmount * 100).ToString();

            _healthInPercents.text = $"{percents}%";
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _lerpTimer = 0;
    }

    public void RestoreHealth(float healthAmount)
    {
        _health += healthAmount;
        _lerpTimer = 0;
    }

    private void OnEnable()
    {
        ShootInEnemy.OnTookDamage += TakeDamage;
    }

    private void OnDisable()
    {
        ShootInEnemy.OnTookDamage -= TakeDamage;
    }
}