using System.Threading.Tasks;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.DisableGameObject
{
    public class PortalManager : MonoBehaviour, IPortalManager
    {
        public async void Open()
        {
            await Task.Delay(2000);

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