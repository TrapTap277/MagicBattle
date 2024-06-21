using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        private IOpenCloseManager _openCloseManager;

        private void Start()
        {
            FadeOrShowPortal(false);
        }

        public void SetDependencies(IOpenCloseManager openCloseManager)
        {
            _openCloseManager = openCloseManager;
        }

        public void FadeOrShowPortal(bool isShow)
        {
            if (!isShow)
                _openCloseManager?.Close();

            else
                _openCloseManager?.Open();
        }
    }
}