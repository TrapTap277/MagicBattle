using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Items
{
    public class CreateItemsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemParent;
        [SerializeField] private CurrentItem _currentItem;

        public void CreateWithItemsCount(int itemsCount)
        {
            for (var i = 0; i < itemsCount; i++)
            {
                var currentItem = GenerateRandomItem.Generate();
                EnemyUseItemState.AddItems(GenerateRandomItem.Generate());
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