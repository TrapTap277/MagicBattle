using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/HealGem", fileName = "HealGem")]
    public class HealGemItem : BaseItem
    {
        public static event Action<float> OnHealedPlayer;
        public static event Action<float> OnHealedEnemy;

        private const float HealValue = 50f;
        
        public override void Use(bool isUsedByPlayer)
        {
            if(isUsedByPlayer)
                OnHealedPlayer?.Invoke(HealValue);
            
            else 
                OnHealedEnemy?.Invoke(HealValue);
        }
    }
}