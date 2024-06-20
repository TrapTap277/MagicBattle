using System;
using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class MoveTransition : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;

        private ISwitchAnimation<StaffAnimations> _switchAnimation;
        private IEnableDisableManager _enableDisableManager;
        private ISetGemPositions _setGemPositions;

        private void Awake()
        {
            InitManagers();
        }

        public async void TransitionToEnemy()
        {
            _setGemPositions?.SetPositions(MoveTurn.Enemy);
            _enableDisableManager?.Fade();
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
        }

        public async void TransitionToPlayer()
        {
            _setGemPositions?.SetPositions(MoveTurn.Player);
            _enableDisableManager?.Show();
            if (_attackStorage.AttackCount == 0) return;
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
        }

        private async Task DissolveOrUnDissolveStaff(StaffAnimations animations)
        {
            _switchAnimation?.SwitchAnimation(animations);
            await Task.Delay(1500);
        }

        private void InitManagers()
        {
            _setGemPositions = FindObjectOfType<ChangeGemPositions>();
            _switchAnimation = FindObjectOfType<StaffAnimationSwitcher>();
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();
        }
    }
}