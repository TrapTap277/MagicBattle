using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.Enemy;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class MoveTransition : MonoBehaviour
    {
        private StaffAnimationSwitcher _staffAnimationSwitcher;
        private GemAnimationSwitcher _gemAnimationSwitcher;
        private IEnableDisableManager _enableDisableManager;
        private readonly List<ISetPositions> _setPositions = new List<ISetPositions>();

        private bool _isDone;

        private void Awake()
        {
            InitManagers();
        }

        private void Start()
        {
            SetPositions(MoveTurn.Player);
        }

        public async void TransitionToEnemy()
        {
            _enableDisableManager?.Fade();
            await Task.Delay(500);
            Dissolve();

            await Task.Delay(1100);

            SetPositions(MoveTurn.Enemy);
        }

        public async void TransitionToPlayer()
        {
            if (_isDone == false)
            {
                UnDissolve();
                SetPositions(MoveTurn.Player);
                _isDone = true;
                return;
            }

            await Task.Delay(500);

            UnDissolve();
            await Task.Delay(500);
            SetPositions(MoveTurn.Player);

            _enableDisableManager?.Show();
        }

        private void Dissolve()
        {
            DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
            DissolveOrUnDissolveGem(GemAnimations.Dissolved);
        }

        private void UnDissolve()
        {
            DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
            DissolveOrUnDissolveGem(GemAnimations.Undissolved);
        }

        private void DissolveOrUnDissolveStaff(StaffAnimations animations)
        {
            AnimationSwitcher<StaffAnimations, ISwitchAnimation<StaffAnimations>>
                .SwitchAnimation(_staffAnimationSwitcher, animations);
        }

        private void DissolveOrUnDissolveGem(GemAnimations gemAnimations)
        {
            AnimationSwitcher<GemAnimations, ISwitchAnimation<GemAnimations>>
                .SwitchAnimation(_gemAnimationSwitcher, gemAnimations);
        }

        private void SetPositions(MoveTurn moveTurn)
        {
            foreach (var setPosition in _setPositions) setPosition.SetPositions(moveTurn);
        }

        private void InitManagers()
        {
            var changeGemPositions = FindObjectOfType<ChangeGemPositions>();
            var staffChangePositions = FindObjectOfType<StaffChangePositions>();
            
            _setPositions.Add(changeGemPositions);
            _setPositions.Add(staffChangePositions);
            
            _staffAnimationSwitcher = FindObjectOfType<StaffAnimationSwitcher>();
            _gemAnimationSwitcher = FindObjectOfType<GemAnimationSwitcher>();
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();
        }
    }
}