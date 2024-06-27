using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Stats
{
    public class StatsShowAndFade : MonoBehaviour, IEnableDisableManager
    {
        [SerializeField] private RectTransform _buttonTransform;
        
        private const float TimeToShowOrFade = 2f;
        private const float ShowEndValue = 1f;
        private const float FadeEndValue = 0f;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public async void Show()
        {
            _canvasGroup.DOFade(ShowEndValue, TimeToShowOrFade).OnComplete(AllowTap());
            await Task.Delay(4000);
            _buttonTransform.transform.DOMoveY(500, TimeToShowOrFade);
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