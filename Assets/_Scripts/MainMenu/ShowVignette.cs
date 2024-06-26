using UnityEngine;

namespace _Scripts.MainMenu
{
    public class ShowVignette : MonoBehaviour
    {
        private VignetteLerp _vignette;

        private void Awake()
        {
            _vignette = FindObjectOfType<VignetteLerp>();

            _vignette.ShowVignette();
        }
    }
}