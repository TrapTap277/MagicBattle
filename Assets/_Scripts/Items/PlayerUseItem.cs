using UnityEngine.UI;

namespace _Scripts.Items
{
    public class PlayerUseItem : BaseUseItem, IInitAndDeinitUseItem
    {
        private static Button _useButton;
        private static BaseItem _baseItem;
        private static CurrentItem _currentItem;

        private static bool _isCurrentItemNotNull;

        public override void Init(BaseItem item, CurrentItem currentItem = null, Button useButton = null)
        {
            base.Init(item);

            _useButton = useButton;
            _currentItem = currentItem;

            InitItemBoolProperties();
        }

        protected override void Use(bool isUsedByEnemy)
        {
            if (!_isCurrentItemNotNull) return;
            base.Use(false);
            AddUsedItem();
            DestroyItem();
        }

        public void Init()
        {
            _useButton.onClick.AddListener(() => Use(false));
        }

        public void Deinit()
        {
            _useButton.onClick.RemoveAllListeners();
        }

        protected override void InitItemBoolProperties()
        {
            base.InitItemBoolProperties();
            _isCurrentItemNotNull = _currentItem != null;
        }

        private static void DestroyItem()
        {
            _currentItem.DestroyItem();
        }
    }
}