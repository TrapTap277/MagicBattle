using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/DamageGem", fileName = "DamageGem")]
    public class DamageGemItem : BaseItem
    {
        public static event Action OnTakeMoreDamage;
        public override void Use()
        {
            ChangeGem(Gem);

            OnTakeMoreDamage?.Invoke();
        }
    }
}