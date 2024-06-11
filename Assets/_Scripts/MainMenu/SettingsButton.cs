using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MainMenu
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _otherButtons;
        [SerializeField] private CanvasGroup _settingsButtons;
        [SerializeField] private Button _settingsButton;

        private const float TIME_TO_FADE = 2f;

        void Start()
        {
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        public void OpenSettingsMenu()
        {
            Sequence fade = DOTween.Sequence();

            fade.Append(_otherButtons.DOFade(0, TIME_TO_FADE));
            fade.Append(_settingsButtons.DOFade(1, TIME_TO_FADE));
        }

        public void Exit()
        {
            Sequence fade = DOTween.Sequence();

            fade.Append(_settingsButtons.DOFade(0, TIME_TO_FADE));
            fade.Append(_otherButtons.DOFade(1, TIME_TO_FADE));
        }
    }
}