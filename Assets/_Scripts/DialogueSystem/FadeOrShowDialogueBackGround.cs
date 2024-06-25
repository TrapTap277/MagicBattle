using _Scripts.Stats;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.DialogueSystem
{
    public class FadeOrShowDialogueBackGround : MonoBehaviour, IEnableDisableManager
    {
        private const float TimeToFade = 2;

        private CanvasGroup _backGround;

        private void Awake()
        {
            _backGround = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            ShowOrFade(1);
        }

        public void Fade()
        {
            ShowOrFade(0);
        }

        private void ShowOrFade(float endValue)
        {
            _backGround.DOFade(endValue, TimeToFade);
        }
    }
}