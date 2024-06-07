using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/MoveGem", fileName = "MoveGem")]
    public class SecondMoveGemItem : BaseItem, IInit, ISetSecondMove
    {
        public static event Action OnGotSecondMove;
        private bool _isUsedByEnemy;

        public void Init(bool isUsedByEnemy)
        {
            _isUsedByEnemy = isUsedByEnemy;
        }

        public SecondMoveTurn Get()
        {
            if (!_isUsedByEnemy)
                return SecondMoveTurn.Player;

            return SecondMoveTurn.Enemy;
        }

        public override void Use()
        {
            OnGotSecondMove?.Invoke();
        }
    }
}