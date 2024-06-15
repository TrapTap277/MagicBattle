using System;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Items
{
    public abstract class BaseItem : ScriptableObject
    {
        [SerializeField] private Gem _gem;
        [SerializeField] private Sprite _sprite;

        public static event Action<Gem> OnChangedGemOnStaff;

        public Gem Gem => _gem;
        public Sprite Sprite => _sprite;

        public abstract void Use();

        protected static void ChangeGem(Gem gem)
        {
            OnChangedGemOnStaff?.Invoke(gem);
        }
    }
}