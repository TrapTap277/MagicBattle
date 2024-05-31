using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/MoveGem", fileName = "MoveGem")]

    public class DoubleMoveGemItem : BaseItem
    {
        public static event Action OnGotSecondMoveToPlayer;
        public static event Action OnGotSecondMoveToEnemy;
        
        public override void Use(bool isUsedByPlayer)
        {
            if(isUsedByPlayer)
                OnGotSecondMoveToPlayer?.Invoke();
            
            else 
                OnGotSecondMoveToEnemy?.Invoke();
        }
    }
}