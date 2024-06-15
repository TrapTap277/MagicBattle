using System;
using UnityEngine;

namespace _Scripts.Items
{
    [CreateAssetMenu(menuName = "Items/MoveGem", fileName = "MoveGem")]
    public class SecondMoveGemItem : BaseItem, IInit
    {
        public static event Action<SecondMoveTurn> OnGotSecondMove;
        private bool _isUsedByEnemy;
        private SecondMoveTurn _secondMoveTurn;

        public void Init(bool isUsedByEnemy)
        {
            _isUsedByEnemy = isUsedByEnemy;
            _secondMoveTurn = _isUsedByEnemy ? SecondMoveTurn.Enemy : SecondMoveTurn.Player;
        }

        public override void Use()
        {
            ChangeGem(Gem);

            OnGotSecondMove?.Invoke(_secondMoveTurn);
        }
    }
}