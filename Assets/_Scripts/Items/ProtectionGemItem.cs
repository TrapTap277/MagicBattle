using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/ProtectionGem", fileName = "ProtectionGem")]
    public class ProtectionGemItem : BaseItem
    {
        public static event Action OnGivenProtectionToPlayer;
        public static event Action OnGivenProtectionToEnemy;
        
        public override void Use(bool isUsedByPlayer)
        {
            if(isUsedByPlayer)
                OnGivenProtectionToPlayer?.Invoke();
            
            else 
                OnGivenProtectionToEnemy?.Invoke();
        }
    }
}