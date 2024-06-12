using DG.Tweening;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class MoveTransition : MonoBehaviour
    {
        [SerializeField] private CanvasGroup AttackButtons;
        [SerializeField] private Transform Staff;
        [SerializeField] private Transform EndStaffPosition;
        [SerializeField] private Transform StartStaffPosition;

        private void Start()
        {
            EnablePanel();
        }

        public void TransitionToEnemy()
        {
            var move = DOTween.Sequence();

            move.Append(Staff.DOMove(EndStaffPosition.position, 2f));
            FadeOrShowAttackButtons(move, 0, false);
        }

        public void TransitionToPlayer()
        {
            var move = DOTween.Sequence();

            move.Append(Staff.DOMove(StartStaffPosition.position, 2f));
            FadeOrShowAttackButtons(move, 1, true);
        }

        private void FadeOrShowAttackButtons(Sequence move, float endValue, bool isBlock)
        {
            move.Append(AttackButtons.DOFade(endValue, 2));
            AttackButtons.blocksRaycasts = isBlock;
        }

        private void EnablePanel()
        {
            AttackButtons.interactable = true;
            AttackButtons.blocksRaycasts = true;
        }
    }
}