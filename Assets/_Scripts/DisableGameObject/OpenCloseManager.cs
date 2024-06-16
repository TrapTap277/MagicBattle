using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.DisableGameObject
{
    public class OpenCloseManager : MonoBehaviour, IOpenCloseManager
    {
        public void Open()
        {
            FadeOrShow(true);
        }

        public void Close()
        {
            FadeOrShow(false);
        }

        private void FadeOrShow(bool isShow)
        {
            gameObject.SetActive(isShow);
        }
    }
}