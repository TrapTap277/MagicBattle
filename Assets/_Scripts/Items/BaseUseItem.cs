using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Items
{
    public class BaseUseItem
    {
        private static bool _isBaseItemNotNull;
        private static BaseItem _baseItem;
        private static CurrentItem _currentItem;

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
            Debug.Log(_baseItem.Gem);
        }

        protected virtual void InitItemBoolProperties()
        {
            _isBaseItemNotNull = _baseItem != null;
        }

        private static void RemoveItem()
        {
            GenerateRandomItem.RemoveItem(_baseItem);
        }
    }
}