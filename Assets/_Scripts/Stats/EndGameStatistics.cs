using System;
using System.Globalization;
using _Scripts.Die;
using _Scripts.Health;
using _Scripts.Items;
using _Scripts.Staff;
using TMPro;
using UnityEngine;

namespace _Scripts.Stats
{
    public class EndGameStatistics : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _didDamageTextMeshProUGUI;
        [SerializeField] private TextMeshProUGUI _usedHealGemsTextMeshProUGUI;
        [SerializeField] private TextMeshProUGUI _usedDamageGemsTextMeshProUGUI;
        [SerializeField] private TextMeshProUGUI _usedProtectionGemsTextMeshProUGUI;
        [SerializeField] private TextMeshProUGUI _usedSecondMoveGemsTextMeshProUGUI;

        private IEnableDisableManager _enableDisableManager;

        private float _damage;
        private int _newItemHeal;
        private int _newItemDamage;
        private int _newItemProtection;
        private int _newItemSecondMove;

        private string _didDamage;
        private string _usedHealGems;
        private string _usedDamageGems;
        private string _usedProtectionGems;
        private string _usedSecondMoveGems;

        private void Awake()
        {
            InitStartText();
            InitStatsManager();
        }

        private void SetDamage(float damage)
        {
            _damage += damage;
        }

        private void SetStats()
        {
            _didDamageTextMeshProUGUI.text = $"{_didDamage} {_damage.ToString(CultureInfo.InvariantCulture)}";
            _usedHealGemsTextMeshProUGUI.text = $"{_usedHealGems} {_newItemHeal.ToString()}";
            _usedDamageGemsTextMeshProUGUI.text = $"{_usedDamageGems} {_newItemDamage.ToString()}";
            _usedProtectionGemsTextMeshProUGUI.text = $"{_usedProtectionGems} {_newItemProtection.ToString()}";
            _usedSecondMoveGemsTextMeshProUGUI.text = $"{_usedSecondMoveGems} {_newItemSecondMove.ToString()}";
            
            _enableDisableManager.Show();
        }

        private void AddItemCount(Gem gem)
        {
            switch (gem)
            {
                case Gem.Heal:
                    _newItemHeal++;
                    break;
                case Gem.Damage:
                    _newItemDamage++;
                    break;
                case Gem.SecondMove:
                {
                    _newItemSecondMove++;
                    break;
                }
                case Gem.Protection:
                    _newItemProtection++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gem), gem, null);
            }
        }

        private void InitStartText()
        {
            _didDamage = _didDamageTextMeshProUGUI.text;
            _usedHealGems = _usedHealGemsTextMeshProUGUI.text;
            _usedDamageGems = _usedDamageGemsTextMeshProUGUI.text;
            _usedProtectionGems = _usedProtectionGemsTextMeshProUGUI.text;
            _usedSecondMoveGems = _usedSecondMoveGemsTextMeshProUGUI.text;
        }

        private void InitStatsManager()
        {
            _enableDisableManager = FindObjectOfType<ShowOrFadeEnableDisable>();
        }

        private void OnEnable()
        {
            HealthBase.OnChangedDamage += SetDamage;
            BaseUseItem.OnAddedItem += AddItemCount;
            DieCounter.OnSetStats += SetStats;
        }

        private void OnDisable()
        {
            HealthBase.OnChangedDamage -= SetDamage;
            BaseUseItem.OnAddedItem -= AddItemCount;
            DieCounter.OnSetStats -= SetStats;
        }
    }
}