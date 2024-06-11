using System;
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

        private const float TimeToFade = 1f;

        private void Start()
        {
            _settingsButton.onClick.AddListener(OpenSettingsMenu);
        }

        public void CloseSettingsMenu()
        {
            ShowAndFadeButtons(_settingsButtons, _otherButtons);
        }

        private static void ShowAndFadeButtons(CanvasGroup firstButtons, CanvasGroup secondButtons, int firstValue = 0, int secondValue = 1)
        {
            var fade = DOTween.Sequence();

            fade.Append(firstButtons.DOFade(firstValue, TimeToFade));
            fade.Append(secondButtons.DOFade(secondValue, TimeToFade));
        }

        private void OpenSettingsMenu()
        {
            ShowAndFadeButtons(_otherButtons, _settingsButtons);
        }

        private void FadeAllButtons()
        {
            ShowAndFadeButtons(_settingsButtons, _otherButtons, 0, 0);
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