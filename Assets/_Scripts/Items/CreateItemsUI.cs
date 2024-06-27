using System;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Items
{
    public class CreateItemsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemParent;
        [SerializeField] private CurrentItem _currentItem;

        private const int MaximumItemsCount = 8;

        public void CreateWithItemsCount(int itemsCount)
        {
            var currentCount = _itemParent.transform.childCount;

            var itemsToAdd = Mathf.Clamp(itemsCount, 0, MaximumItemsCount - currentCount);

            for (var i = 0; i < itemsToAdd; i++)
            {
                var currentItem = GenerateRandomItem.Generate();
                EnemyUseItemState.AddItems(currentItem);
                InstantiateItem(currentItem);
            }
        }

        private void InstantiateItem(BaseItem item)
        {
            var newItem = Instantiate(_currentItem, _itemParent.transform);
            newItem.SetItem(item);
        }
    }
}