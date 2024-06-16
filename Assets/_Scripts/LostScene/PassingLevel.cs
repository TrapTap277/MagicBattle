using System.Threading.Tasks;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        private IOpenCloseManager _openCloseManager;
        private IDoorManager _doorManager;
        private IStaffAnimationController _staffAnimationController;

        private void Start()
        {
            FadeOrShowPortal(false);
        }

        public void SetDependencies(IOpenCloseManager openCloseManager, IDoorManager doorManager,
            IStaffAnimationController staffAnimationController, IUseMagic useMagic)
        {
            _openCloseManager = openCloseManager;
            _doorManager = doorManager;
            _staffAnimationController = staffAnimationController;
        }

        public void FadeOrShowPortal(bool isShow)
        {
            if(!isShow)
                _openCloseManager?.Close();

            else
                _openCloseManager?.Open();
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
    }
}