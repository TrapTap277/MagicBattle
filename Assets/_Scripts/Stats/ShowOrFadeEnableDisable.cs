using DG.Tweening;
using UnityEngine;

namespace _Scripts.Stats
{
    public class ShowOrFadeEnableDisable : MonoBehaviour, IEnableDisableManager
    {
        private const float TimeToShowOrFade = 2f;
        private const float ShowEndValue = 1f;
        private const float FadeEndValue = 0f;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            _canvasGroup.DOFade(ShowEndValue, TimeToShowOrFade).OnComplete(AllowTap());
        }

        public void Fade()
        {
            _canvasGroup.DOFade(FadeEndValue, TimeToShowOrFade);
        }

        private TweenCallback AllowTap()
        {
            _canvasGroup.blocksRaycasts = true;
            return null;
        }
    }
}