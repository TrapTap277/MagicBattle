using System.Threading.Tasks;
using _Scripts.Animations;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        private IOpenCloseManager _openCloseManager;
        private IDoorManager _doorManager;
        private ISwitchAnimation<StaffAnimations> _switchAnimation;

        private void Start()
        {
            _switchAnimation = FindObjectOfType<StaffAnimationSwitcher>();
            FadeOrShowPortal(false);
        }

        public void SetDependencies(IOpenCloseManager openCloseManager, IDoorManager doorManager,
            ISwitchAnimation<StaffAnimations> switchAnimation)
        {
            _openCloseManager = openCloseManager;
            _doorManager = doorManager;
            _switchAnimation = switchAnimation;
        }

        public void FadeOrShowPortal(bool isShow)
        {
            if (!isShow)
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
            if (animations == StaffAnimations.None)
            {
                var randomAttackAnimation = _switchAnimation.SetRandomAttackAnimation();

                _switchAnimation?.SwitchAnimation(randomAttackAnimation);
                return;
            }
            
            _switchAnimation?.SwitchAnimation(animations);
        }

        private void DestroyDoor()
        {
            _doorManager?.PlayDissolvedAnimation();
        }
    }
}