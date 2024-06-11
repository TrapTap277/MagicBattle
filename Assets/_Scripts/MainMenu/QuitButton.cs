using UnityEditor;
using UnityEngine;

namespace _Scripts.MainMenu
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(); // Todo Maybe it will not work
#endif
        }
    }
}