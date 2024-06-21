﻿using System.Threading.Tasks;
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

        private StaffAnimationSwitcher _staffAnimationSwitcher;
        private IEnableDisableManager _enableDisableManager;
        private ISetPositions _setPositions;

        private void Awake()
        {
            InitManagers();
        }

        public async void TransitionToEnemy()
        {
            _setPositions?.SetPositions(MoveTurn.Enemy);
            _enableDisableManager?.Fade();
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
        }

        public async void TransitionToPlayer()
        {
            _setPositions?.SetPositions(MoveTurn.Player);
            _enableDisableManager?.Show();
            if (_attackStorage.AttackCount == 0) return;
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
        }

        private async Task DissolveOrUnDissolveStaff(StaffAnimations animations)
        {
            AnimationSwitcher<StaffAnimations, ISwitchAnimation<StaffAnimations>>
                .SwitchAnimation(_staffAnimationSwitcher, animations);
            await Task.Delay(1500);
        }

        private void InitManagers()
        {
            _setPositions = FindObjectOfType<ChangePositions>();
            _staffAnimationSwitcher = FindObjectOfType<StaffAnimationSwitcher>();
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();
        }
    }
}