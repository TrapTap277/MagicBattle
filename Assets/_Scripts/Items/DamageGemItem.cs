using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/DamageGem", fileName = "DamageGem")]
    public class DamageGemItem : BaseItem
    {
        public static event Action OnTakeMoreDamageToPlayer;
        public static event Action OnTakeMoreDamageToEnemy;
        
        public override void Use(bool isUsedByPlayer)
        {
            if(isUsedByPlayer)
                OnTakeMoreDamageToPlayer?.Invoke();
            
            else 
                OnTakeMoreDamageToEnemy?.Invoke();
        }
    }
}