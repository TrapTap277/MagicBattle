using DG.Tweening;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class MoveTransition : MonoBehaviour
    {
        public Transform Staff;
        public CanvasGroup AttackButtons;

        public Transform EndStaffPosition;
        public Transform StartStaffPosition;

        public void TransitionToEnemy()
        {
            Sequence move = DOTween.Sequence();

            move.Append(Staff.DOMove(EndStaffPosition.position, 2f));
            move.Join(AttackButtons.DOFade(0, 2));
            AttackButtons.blocksRaycasts = false;
        }
        
        public void TransitionToPlayer()
        {
            Sequence move = DOTween.Sequence();

            move.Append(Staff.DOMove(StartStaffPosition.position, 2f));
            move.Append(AttackButtons.DOFade(1, 2));
            AttackButtons.blocksRaycasts = true;
        }
    }
}