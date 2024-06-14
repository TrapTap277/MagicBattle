using System.Threading.Tasks;
using _Scripts.Attacks;
using _Scripts.LostScene;
using _Scripts.Staff;
using DG.Tweening;
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

        private IStaffAnimationController _staffAnimationController;

        private void Awake()
        {
            SetAnimationController();
        }

        private void Start()
        {
            EnablePanel(true);
        }

        public async void TransitionToEnemy()
        {
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
            SetStaffPositionsAndRotation(_endStaffPosition);
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
            FadeOrShowAttackButtons(0, false);
        }

        public async void TransitionToPlayer()
        {
            if(_attackStorage.AttackCount == 0 && _staff.position == _startStaffPosition.position) return;
            Debug.Log(_attackStorage.AttackCount);
            await DissolveOrUnDissolveStaff(StaffAnimations.DissolveStaff);
            SetStaffPositionsAndRotation(_startStaffPosition);
            await DissolveOrUnDissolveStaff(StaffAnimations.UnDissolveStaff);
            FadeOrShowAttackButtons(1, true);
        }

        private async Task DissolveOrUnDissolveStaff(StaffAnimations animations)
        {
            _staffAnimationController?.SwitchAnimation(animations);
            await Task.Delay(1000);
        }

        private void SetStaffPositionsAndRotation(Transform endPositions)
        {
            _staff.position = endPositions.position;
            _staff.rotation = endPositions.rotation;
        }

        private void FadeOrShowAttackButtons(float endValue, bool isBlock)
        {
            var move = DOTween.Sequence();
            move.Append(_attackButtons.DOFade(endValue, 2));
            EnablePanel(isBlock);
        }

        private void SetAnimationController()
        {
            _staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();
        }

        private void EnablePanel(bool isBlock)
        {
            _attackButtons.blocksRaycasts = isBlock;
        }
    }
}