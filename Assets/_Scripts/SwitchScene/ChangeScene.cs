using _Scripts.MainMenu;
using _Scripts.Move;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SwitchScene
{
    public class ChangeScene : MonoBehaviour
    {
        private const float TimeToDarkness = 3f;
        private const int DarknessEndValue = 1;
        
        private CanvasGroup _darkness;

        private void Start()
        {
            _darkness = GameObject.FindGameObjectWithTag("Darkness").GetComponent<CanvasGroup>();
        }

        private void ChangeCurrentScene(int index)
        {
            var darkness = DOTween.Sequence();
            darkness.Append(_darkness.DOFade(DarknessEndValue, TimeToDarkness).SetEase(Ease.Linear))
                .OnComplete(() => ChangeCurrentSceneAfterDarkness(index));
        }

        private static void ChangeCurrentSceneAfterDarkness(int index)
        {
            SceneManager.LoadScene(index);
        }

        private void OnEnable()
        {
            // PlayerLost.OnChangedScene += ChangeCurrentScene; Todo When lost
            GoToPortal.OnChangedScene += ChangeCurrentScene;
            VignetteLerp.OnChangedScene += ChangeCurrentScene;
        }

        private void OnDisable()
        {
            // PlayerLost.OnChangedScene -= ChangeCurrentScene; Todo When lost
            GoToPortal.OnChangedScene -= ChangeCurrentScene;
            VignetteLerp.OnChangedScene -= ChangeCurrentScene;
        }
    }
}