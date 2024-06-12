using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffSwitchAnimation : MonoBehaviour, IStaffAnimationController
    {
        private Animator _staffAnimator;

        private const string FirstAttack = "StaffAttack";
        private const string SecondAttack = "StaffAttack2";
        private const string ThirdAttack = "StaffAttack3";
        private const string Idle = "Idle";
        private const string FadeStaff = "FadeStaff";
        private const string ShowStaff = "ShowStaff";

        private List<int> _attackStates = new List<int>();

        private int _firstAttackState;
        private int _secondAttackState;
        private int _thirdAttackState;
        private int _idleState;
        private int _fadeState;
        private int _showState;

        private void Awake()
        {
            _staffAnimator = GetComponent<Animator>();
            
            InitHashes();
            InitList();
        }

        public async void SwitchAnimation()
        {
            if (_staffAnimator == null) return;
            var randomNumber = Random.Range(0, _attackStates.Count);
            CrossFade(randomNumber);

            await Task.Delay(3000);

            CrossFade(_idleState);
        }

        public void SetFadeAnimation()
        {
            CrossFade(_fadeState);
        }

        public void SetShowStaff()
        {
            CrossFade(_showState);
        }

        private void CrossFade(int randomNumber)
        {
            _staffAnimator.CrossFade(randomNumber <= _attackStates.Count && randomNumber >= 0 ? _attackStates[randomNumber] : randomNumber,
                0);
        }

        private void InitHashes()
        {
            _firstAttackState = Animator.StringToHash(FirstAttack);
            _secondAttackState = Animator.StringToHash(SecondAttack);
            _thirdAttackState = Animator.StringToHash(ThirdAttack);
            _idleState = Animator.StringToHash(Idle);
            _fadeState = Animator.StringToHash(FadeStaff);
            _showState = Animator.StringToHash(ShowStaff);
        }

        private void InitList()
        {
            _attackStates = new List<int> {_firstAttackState, _secondAttackState, _thirdAttackState};
        }
    }
}