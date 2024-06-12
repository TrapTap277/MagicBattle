using System.Threading.Tasks;
using _Scripts.LostScene;
using _Scripts.MainMenu;
using _Scripts.Move;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SwitchScene
{
    public class ChangeScene : MonoBehaviour
    {
        private Darkness _darkness;

        private void Start()
        {
            _darkness = FindObjectOfType<Darkness>();
        }

        private async void ChangeCurrentScene(int index)
        {
            _darkness.CrossFadeToDarknessShow();
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