using _Scripts.Move;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SwitchScene
{
    public class ChangeScene : MonoBehaviour
    {
        private CanvasGroup _darkness;
        private Animator _darknessAnimator;
        private const float TIME_TO_DARKNESS = 3f;

        private void Start()
        {
            _darkness = GameObject.FindGameObjectWithTag("Darkness").GetComponent<CanvasGroup>();
            _darknessAnimator = _darkness.GetComponent<Animator>();
        }

        public void ChangeCurrentScene(int index)
        {
            if (_darknessAnimator != null)
                _darknessAnimator.enabled = false;

            Sequence darkness = DOTween.Sequence();
            darkness.Append(_darkness.DOFade(1, TIME_TO_DARKNESS).SetEase(Ease.Linear)).OnComplete(() => ChangeCurrentSceneAfterDarkness(index));
        }

        private void ChangeCurrentSceneAfterDarkness(int index)
        {
            SceneManager.LoadScene(index);
        }

        private void OnEnable()
        {
            // PlayerLost.OnChangedScene += ChangeCurrentScene; Todo When lost
            GoToPortal.OnChangedScene += ChangeCurrentScene;
            //VignetteLerp.OnChengedScene += ChangeCurrentScene; Todo in pre scene
        }

        private void OnDisable()
        {
            // PlayerLost.OnChangedScene -= ChangeCurrentScene; Todo
            GoToPortal.OnChangedScene -= ChangeCurrentScene;
            // VignetteLerp.OnChengedScene -= ChangeCurrentScene; Todo
        }
    }
}