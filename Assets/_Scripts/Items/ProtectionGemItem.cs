using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/ProtectionGem", fileName = "ProtectionGem")]
    public class ProtectionGemItem : BaseItem, IInit
    {
        public static event Action OnGivenProtectionToPlayer;
        public static event Action OnGivenProtectionToEnemy;

        private bool _isUsedByEnemy;

        public void Init(bool isUsedByEnemy)
        {
            _isUsedByEnemy = isUsedByEnemy;
        }

        public override void Use()
        {
            if(!_isUsedByEnemy)
                OnGivenProtectionToPlayer?.Invoke();
            
            else 
                OnGivenProtectionToEnemy?.Invoke();
        }
    }
}