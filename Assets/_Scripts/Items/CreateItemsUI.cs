using System.Collections.Generic;
using _Scripts.Staff;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Items
{
    public class CreateItemsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemParent;
        [SerializeField] private CurrentItem _currentItem;

        [SerializeField] private HealGemItem _healGemItem;
        [SerializeField] private DamageGemItem _damageGemItem;
        [SerializeField] private ProtectionGemItem _protectionGemItem;
        [SerializeField] private SecondMoveGemItem _secondMoveGemItem;

        private Dictionary<Gem, BaseItem> _items;

        private void Awake()
        {
            _items = new Dictionary<Gem, BaseItem>()
            {
                {Gem.Heal, _healGemItem},
                {Gem.Damage, _damageGemItem},
                {Gem.Protection, _protectionGemItem},
                {Gem.SecondMove, _secondMoveGemItem},
            };
        }

        public void Create(int itemsCount)
        {
            for (var i = 0; i < itemsCount; i++)
            {
                var currentItem = (Gem) Random.Range(3, 7);

                DeterminateItem(currentItem);
            }
        }

        private void DeterminateItem(Gem gem)
        {
            if (_items.TryGetValue(gem, out var item))
                InstantiateItem(item);
            else
                Debug.LogWarning($"No item found for gem type {gem}");
        }

        private void InstantiateItem(BaseItem item)
        {
            var newItem = Instantiate(_currentItem, _itemParent.transform);
            newItem.SetItem(item);
        }
    }
}