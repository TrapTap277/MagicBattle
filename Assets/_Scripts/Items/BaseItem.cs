using System;
using _Scripts.Staff;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Scripts.Items
{
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField] private Gem _gem;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _maximumItemCount;

        public static event Action<Gem> OnChangedGemOnStaff;
        
        public Gem Gem => _gem;
        public Sprite Sprite => _sprite;

        public int MaximumItemCount
        {
            get => _maximumItemCount;
            set => _maximumItemCount = value;
        }

        protected static void ChangeGem(Gem gem)
        {
            OnChangedGemOnStaff?.Invoke(gem);
        }

        public abstract void Use();
    }
}