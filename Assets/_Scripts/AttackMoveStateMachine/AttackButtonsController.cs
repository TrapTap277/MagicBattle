using _Scripts.Health;
using _Scripts.Stats;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class AttackButtonsController : MonoBehaviour, IEnableDisableManager
    {
        private CanvasGroup _attackPanel;

        private bool _isBlocked;

        private void Awake()
        {
            _attackPanel = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (_isBlocked) return;
            FadeOrShowAttackButtons(1, true);
        }

        public void Fade()
        {
            FadeOrShowAttackButtons(0, false);
        }

        private void FadeOrShowAttackButtons(float endValue, bool isBlock)
        {
            var move = DOTween.Sequence();
            EnablePanel(isBlock);
            move.Append(_attackPanel.DOFade(endValue, 1));
        }

        private void EnablePanel(bool isBlock)
        {
            if (_attackPanel != null)
                _attackPanel.blocksRaycasts = isBlock;
        }

        private void Block(bool isBlocked)
        {
            _isBlocked = isBlocked;

            if (_isBlocked)
            {
                DOTween.KillAll();
                Fade();
            }
        }

        private void OnEnable()
        {
            BoxWithItems.BoxWithItems.OnBlocked += Block;
            HealthBase.OnDied += Block;
        }

        private void OnDisable()
        {
            BoxWithItems.BoxWithItems.OnBlocked -= Block;
            HealthBase.OnDied -= Block;
        }
    }
}