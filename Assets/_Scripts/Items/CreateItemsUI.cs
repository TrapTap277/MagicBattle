using UnityEngine;

namespace _Scripts.Items
{
    public class CreateItemsUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemParent;
        [SerializeField] private CurrentItem _currentItem;

        public HealGemItem HealGemItem;
        
        private void Start()
        {
            Create(HealGemItem);
        }

        public void Create(BaseItem item)
        {
            CurrentItem newItem = Instantiate(_currentItem, _itemParent.transform);
            newItem.SetItem(item);
        }
    }
}