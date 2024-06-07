using UnityEngine.UI;

namespace _Scripts.Items
{
    public static class UseItem
    {
        private static Button _useButton;
        private static BaseItem _baseItem;
        private static CurrentItem _currentItem;
        private static bool _isBaseItemNotNull;
        private static bool _isCurrentItemNotNull;

        public static void Init(Button useButton, BaseItem baseItem, CurrentItem currentItem)
        {
            _useButton = useButton;
            _baseItem = baseItem;
            _currentItem = currentItem;

            InitItemBoolProperties();
        }

        public static void InitButton()
        {
            _useButton.onClick.AddListener(() => Use(false));
        }

        public static void DeInitButton()
        {
            _useButton.onClick.RemoveAllListeners();
        }

        private static void Use(bool isUsedByEnemy)
        {
            if (!_isBaseItemNotNull || !_isCurrentItemNotNull) return;
            var init = _baseItem as IInit;
            init?.Init(isUsedByEnemy);
            _baseItem.Use();
            DestroyItem();
        }

        private static void DestroyItem()
        {
            _currentItem.DestroyItem();
        }

        private static void InitItemBoolProperties()
        {
            _isCurrentItemNotNull = _currentItem != null;
            _isBaseItemNotNull = _baseItem != null;
        }
    }
}