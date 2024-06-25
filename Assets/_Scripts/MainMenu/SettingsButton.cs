using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MainMenu
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _otherButtonsCanvasGroup;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;
        [SerializeField] private Button _settingsButton;

        private const float TimeToFade = 1f;

        private void Start()
        {
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        public void CloseSettingsMenu()
        {
            ShowAndFadeButtons(_settingsCanvasGroup, _otherButtonsCanvasGroup);
            _otherButtonsCanvasGroup.blocksRaycasts = true;
        }

        private static void ShowAndFadeButtons(CanvasGroup firstButtons, CanvasGroup secondButtons, int firstValue = 0,
            int secondValue = 1)
        {
            var fade = DOTween.Sequence();

            fade.Append(firstButtons.DOFade(firstValue, TimeToFade));
            fade.Append(secondButtons.DOFade(secondValue, TimeToFade));
        }

        private void OpenSettingsMenu()
        {
            ShowAndFadeButtons(_otherButtonsCanvasGroup, _settingsCanvasGroup);
            _otherButtonsCanvasGroup.blocksRaycasts = false;
        }

        private void FadeAllButtons()
        {
            ShowAndFadeButtons(_settingsCanvasGroup, _otherButtonsCanvasGroup, 0, 0);
        }

        private void OnEnable()
        {
            VignetteLerp.OnFadeButtons += FadeAllButtons;
        }

        private void OnDisable()
        {
            VignetteLerp.OnFadeButtons -= FadeAllButtons;
        }
    }
}