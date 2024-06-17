using System.Threading.Tasks;
using _Scripts.Attacks;
using _Scripts.LostScene;
using _Scripts.Shooting;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.AttackMoveStateMachine
{
    public class MoveTransition : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private CanvasGroup _attackButtons;
        [SerializeField] private Transform _staff;
        [SerializeField] private Transform _endStaffPosition;
        [SerializeField] private Transform _startStaffPosition;

        [SerializeField] private Transform _enemyPositions;
        [SerializeField] private Transform _playerPositions;

        private IStaffAnimationController _staffAnimationController;
        private ISetStaffPositions _setStaffPositions;
        private IEnableDisableManager _enableDisableManager;

        private void Awake()
        {
            InitManagers();
        }

        public async void TransitionToEnemy()
        {
            _enableDisableManager?.Fade();
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
            SetStaffPositionsAndRotation(_endStaffPosition);
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
        }

        public async void TransitionToPlayer()
        {
            _enableDisableManager?.Show();
            if (_attackStorage.AttackCount == 0 && _staff.position == _startStaffPosition.position) return;
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
            SetStaffPositionsAndRotation(_startStaffPosition);
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
        }

        private void SetStaffPositions(ShootIn shootIn)
        {
            _setStaffPositions?.SetPositions(shootIn == ShootIn.Player ? _enemyPositions : _playerPositions);
        }

        private async Task DissolveOrUnDissolveStaff(StaffAnimations animations)
        {
            _staffAnimationController?.SwitchAnimation(animations);
            await Task.Delay(1500);
        }

        private void SetStaffPositionsAndRotation(Transform endPositions)
        {
            _staff.position = endPositions.position;
            _staff.rotation = endPositions.rotation;
        }

        private void InitManagers()
        {
            _staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();
            _setStaffPositions = FindObjectOfType<UseMagic>();
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();
        }

        private void OnEnable()
        {
            BaseShoot.OnChangedStaffAttackPosition += SetStaffPositions;
        }

        private void OnDisable()
        {
            BaseShoot.OnChangedStaffAttackPosition -= SetStaffPositions;
        }
    }
}