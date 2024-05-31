using System;
using _Scripts.Staff;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Items
{
    public class CurrentItem : MonoBehaviour
    {
        public static event Action<Gem> OnChangedGemOnStaff;
        
        private BaseItem _item;
        private void Awake()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() => UseItem(true));

            SetSkin();
        }

        public void SetItem(BaseItem item)
        {
            _item = item;
        }

        public void UseItem(bool isUsedByPlayer)
        {
            _item.Use(isUsedByPlayer);
        }

        private void SetSkin()
        {
            gameObject.GetComponent<Image>().sprite = _item.Sprite;
            
            OnChangedGemOnStaff?.Invoke(_item.Gem);
        }
    }
}