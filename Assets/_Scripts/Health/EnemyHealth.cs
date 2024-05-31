using System;
using _Scripts.Health;
using _Scripts.Items;
using _Scripts.Shooting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static event Action OnEnemyDied;
    public float Health { get; private set; }

    [SerializeField] private TextMeshProUGUI _healthInPercents;
    [SerializeField] private Image _frontHealthBar;
    [SerializeField] private Image _backHealthBar;

    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _chipSpeed = 2f;

    private DieCounter _dieCounter = new DieCounter();
    private float _lerpTimer;
    private float _damageСoefficient;
    private bool _isHasProtection;

    void Start()
    {
        Health = _maxHealth;
    }

    void Update()
    {
        Health = Mathf.Clamp(Health, 0, _maxHealth);

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = _frontHealthBar.fillAmount;
        float fillB = _backHealthBar.fillAmount;
        float hFraction = Health / _maxHealth;

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
        if (_isHasProtection == false)
        {
            Health -= damage * _damageСoefficient;
            _lerpTimer = 0;
        }

        if (Health <= 0)
        {
            _dieCounter.AddEnemyDies();
            
            OnEnemyDied?.Invoke();
        }

        _isHasProtection = false;
        _damageСoefficient = 1f;
    }

    public void RestoreHealth(float healthAmount)
    {
        Health += healthAmount;
        _lerpTimer = 0;
    }

    public void GetProtection()
    {
        _isHasProtection = true;
    }
    
    public void TakeMoreDamage()
    {
        _damageСoefficient = UnityEngine.Random.Range(1.1f, 2f);
    }

    private void OnEnable()
    {
        ShootInEnemy.OnTookDamage += TakeDamage;
        HealGemItem.OnHealedEnemy += RestoreHealth;
        ProtectionGemItem.OnGivenProtectionToEnemy += GetProtection;
        DamageGemItem.OnTakeMoreDamageToEnemy += TakeMoreDamage;
    }

    private void OnDisable()
    {
        ShootInEnemy.OnTookDamage -= TakeDamage;
        HealGemItem.OnHealedEnemy -= RestoreHealth;
        ProtectionGemItem.OnGivenProtectionToEnemy -= GetProtection;
        DamageGemItem.OnTakeMoreDamageToEnemy -= TakeMoreDamage;
    }
}