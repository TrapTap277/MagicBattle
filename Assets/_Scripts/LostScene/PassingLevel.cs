using System.Threading.Tasks;
using _Scripts.Staff;
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

        public void SetStaffAnimation(StaffAnimations animations)
        {
            _staffAnimationController?.SwitchAnimation(animations);
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