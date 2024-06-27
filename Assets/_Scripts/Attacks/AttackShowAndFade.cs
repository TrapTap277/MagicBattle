using _Scripts.Stats;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Attacks
{
    public class AttackShowAndFade : MonoBehaviour, IEnableDisableManager
    {
        private const int ShowEndValue = 1;
        private const int FadeEndValue = 0;
        private const int TimeToShow = 1;
        private const int TimeToFade = 2;

        public void Show()
        {
            FadeOrShowAttackButtons(ShowEndValue);
        }

        public void Fade()
        {
            FadeOrShowAttackButtons(FadeEndValue);
        }

        private void FadeOrShowAttackButtons(float endValue)
        {
            var move = DOTween.Sequence();
            move.Append(ShowEndValue == endValue
                ? gameObject.GetComponent<CanvasGroup>().DOFade(endValue, TimeToShow).OnComplete(Fade)
                : gameObject.GetComponent<CanvasGroup>().DOFade(endValue, TimeToFade));
        }
    }
}