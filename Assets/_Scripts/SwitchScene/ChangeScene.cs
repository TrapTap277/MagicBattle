using System.Threading.Tasks;
using _Scripts.LostScene;
using _Scripts.MainMenu;
using _Scripts.Move;
using _Scripts.Stats;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SwitchScene
{
    public class ChangeScene : MonoBehaviour
    {
        private IEnableDisableManager _enableDisableManager;
        private IChangeAnimation _changeAnimation;

        private void Start()
        {
            _enableDisableManager = FindObjectOfType<StatsShowAndFade>();
            _changeAnimation = FindObjectOfType<Darkness>();
        }

        public async void ChangeCurrentScene(int index)
        {
            _enableDisableManager?.Fade();
            await Task.Delay(2000);
            _changeAnimation?.CrossFade();
            await Task.Delay(2000);
            SwitchScene(index);
        }

        private static void SwitchScene(int index)
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