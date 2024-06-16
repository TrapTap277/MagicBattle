using System;
using _Scripts.Staff;
using UnityEngine.UI;

namespace _Scripts.Items
{
    public class BaseUseItem
    {
        public static event Action<Gem> OnAddedItem;

        private static bool _isBaseItemNotNull;
        private static BaseItem _baseItem;
        private static CurrentItem _currentItem;

        private int _healItemsUsedCounter;
        private int _damageItemsUsedCounter;
        private int _protectionItemsUsedCounter;
        private int _secondMoveItemsUsedCounter;

        public virtual void Init(BaseItem item, CurrentItem currentItem = null, Button useButton = null)
        {
            _baseItem = item;
        }

        protected virtual void Use(bool isUsedByEnemy)
        {
            if (!_isBaseItemNotNull) return;
            RemoveItem();
            var init = _baseItem as IInit;
            init?.Init(isUsedByEnemy);
            _baseItem.Use();
        }

        protected virtual void InitItemBoolProperties()
        {
            _isBaseItemNotNull = _baseItem != null;
        }

        private static void RemoveItem()
        {
            GenerateRandomItem.RemoveItem(_baseItem);
        }

        protected static void AddUsedItem()
        {
            OnAddedItem?.Invoke(_baseItem.Gem);
        }
    }
}