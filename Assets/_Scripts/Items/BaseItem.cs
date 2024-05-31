using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Items
{
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField] private Gem _gem;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Material _itemMaterial;
        [SerializeField] private int _maximumItemCount;

        public Gem Gem => _gem;
        public Sprite Sprite => _sprite;
        public Material ItemMaterial => _itemMaterial;
        public int MaximumItemCount
        {
            get => _maximumItemCount;
            set => _maximumItemCount = value;
        }
        
        public abstract void Use(bool isUsedByPlayer);
    }
}