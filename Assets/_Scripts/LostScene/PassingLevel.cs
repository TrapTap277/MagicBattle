using System.Threading.Tasks;
using _Scripts.Animations;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        private IOpenCloseManager _openCloseManager;
        private IDoorManager _doorManager;
        private StaffAnimationSwitcher _switchAnimation;

        private void Start()
        {
            _switchAnimation = FindObjectOfType<StaffAnimationSwitcher>();
            FadeOrShowPortal(false);
        }

        public void SetDependencies(IOpenCloseManager openCloseManager, IDoorManager doorManager,
            StaffAnimationSwitcher switchAnimation)
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
                AnimationSwitcher<StaffAnimations, ISwitchAnimation<StaffAnimations>>
                    .SetRandomAnimation(_switchAnimation);
                return;
            }
            
            AnimationSwitcher<StaffAnimations, ISwitchAnimation<StaffAnimations>>
                .SwitchAnimation(_switchAnimation, animations);
        }

        private void DestroyDoor()
        {
            _doorManager?.PlayDissolvedAnimation();
        }
    }
}