using _Scripts.Stats;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class AttackButtonsController : MonoBehaviour, IEnableDisableManager
    {
        private CanvasGroup _attackPanel;

        private void Awake()
        {
            _attackPanel = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            FadeOrShowAttackButtons(1, true);
        }

        public void Fade()
        {
            FadeOrShowAttackButtons(0, false);
        }

        private void FadeOrShowAttackButtons(float endValue, bool isBlock)
        {
            var move = DOTween.Sequence();
            move.Append(_attackPanel.DOFade(endValue, 2));
            EnablePanel(isBlock);
        }

        private void EnablePanel(bool isBlock)
        {
            if (_attackPanel != null)
                _attackPanel.blocksRaycasts = isBlock;
        }
    }
}