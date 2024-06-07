using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/HealGem", fileName = "HealGem")]
    public class HealGemItem : BaseItem, IInit
    {
        public static event Action<float> OnHealedPlayer;
        public static event Action<float> OnHealedEnemy;

        private const float HealValue = 50f;
        
        private bool _isUsedByEnemy;

        public void Init(bool isUsedByEnemy)
        {
            _isUsedByEnemy = isUsedByEnemy;
        }
        
        public override void Use()
        {
            if(!_isUsedByEnemy)
                OnHealedPlayer?.Invoke(HealValue);
            
            else 
                OnHealedEnemy?.Invoke(HealValue);
            
            Debug.Log("Heal");
        }
    }
}