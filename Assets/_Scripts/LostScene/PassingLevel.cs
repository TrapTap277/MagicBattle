using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        private const bool ClosePortal = false;

        private IUseMagic _useMagic;
        private IPortalManager _portalManager;
        private IDoorManager _doorManager;
        private IStaffAnimationController _staffAnimationController;

        private void Start()
        {
            FadeOrShowPortal(ClosePortal);
        }

        public void SetDependencies(IPortalManager portalManager, IDoorManager doorManager,
            IStaffAnimationController staffAnimationController, IUseMagic useMagic)
        {
            _portalManager = portalManager;
            _doorManager = doorManager;
            _staffAnimationController = staffAnimationController;
            _useMagic = useMagic;
        }

        public void FadeOrShowPortal(bool isShow)
        {
            if (isShow)
            {
                _portalManager?.Open();
            }

            if (!isShow) _portalManager?.Close();
        }

        public async Task DestroyDoorAndUseMagicAsync()
        {
            DestroyDoor();
            await Task.Delay(4000);
        }

        public void SetRandomAttackAnimation()
        {
            _staffAnimationController?.SwitchAnimation();
        }

        public void SetFadeAnimation()
        {
            _staffAnimationController?.SetFadeAnimation();
        }

        public void SetShowAnimation()
        {
            _staffAnimationController?.SetShowStaff();
        }

        public async Task SetCallPortalAnimation()
        {
            _staffAnimationController?.SetCallPortalAnimation();
            await Task.Delay(8500);
        }

        private void DestroyDoor()
        {
            _doorManager?.PlayDissolvedAnimation();
        }

        private void UseMagic()
        {
            _useMagic?.Use();
        }
    }
}