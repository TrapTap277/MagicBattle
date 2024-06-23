using System.Collections.Generic;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Animations
{
    public class StaffAnimationSwitcher : BaseAnimationSwitcher, ISwitchAnimation<StaffAnimations>
    {
        private const string FirstAttack = "StaffAttack";
        private const string SecondAttack = "StaffAttack2";
        private const string ThirdAttack = "StaffAttack3";
        private const string FadeStaff = "FadeStaff";
        private const string ShowStaff = "ShowStaff";
        private const string OpenPortal = "OpenPortal";
        private const string DissolveStaff = "DissolveStaff";
        private const string UnDissolveStaff = "UnDissolveStaff";

        private int _firstAttackState;
        private int _secondAttackState;
        private int _thirdAttackState;
        private int _fadeState;
        private int _showState;
        private int _openPortalState;
        private int _dissolveState;
        private int _unDissolveState;

        private readonly List<StaffAnimations> _randomAttack = new List<StaffAnimations>();
        private Dictionary<StaffAnimations, int> _staffAnimations = new Dictionary<StaffAnimations, int>();
        private Animator _staffAnimator;

        private void Awake()
        {
            _staffAnimator = GetComponent<Animator>();

            Init(_staffAnimator);
        }

        public void SwitchAnimation(StaffAnimations staffAnimations, float timeToTransition = 0)
        {
            var value = GetStaffAnimation(staffAnimations);
            CrossFade(value, timeToTransition);
        }

        public StaffAnimations SetRandomAttackAnimation()
        {
            var randomIndex = Random.Range(0, _randomAttack.Count);
            return _randomAttack[randomIndex];
        }

        protected override void InitHashes()
        {
            _firstAttackState = Animator.StringToHash(FirstAttack);
            _secondAttackState = Animator.StringToHash(SecondAttack);
            _thirdAttackState = Animator.StringToHash(ThirdAttack);
            _fadeState = Animator.StringToHash(FadeStaff);
            _showState = Animator.StringToHash(ShowStaff);
            _openPortalState = Animator.StringToHash(OpenPortal);
            _dissolveState = Animator.StringToHash(DissolveStaff);
            _unDissolveState = Animator.StringToHash(UnDissolveStaff);
        }

        protected override void InitDictionary()
        {
            _staffAnimations = new Dictionary<StaffAnimations, int>
            {
                {StaffAnimations.FirstAttack, _firstAttackState},
                {StaffAnimations.SecondAttack, _secondAttackState},
                {StaffAnimations.ThirdAttack, _thirdAttackState},
                {StaffAnimations.FadeStaff, _fadeState},
                {StaffAnimations.ShowStaff, _showState},
                {StaffAnimations.OpenPortal, _openPortalState},
                {StaffAnimations.DissolveStaff, _dissolveState},
                {StaffAnimations.UnDissolveStaff, _unDissolveState}
            };
        }

        protected override void InitList()
        {
            _randomAttack.Add(StaffAnimations.FirstAttack);
            _randomAttack.Add(StaffAnimations.SecondAttack);
            _randomAttack.Add(StaffAnimations.ThirdAttack);
        }

        private int GetStaffAnimation(StaffAnimations staffAnimations)
        {
            _staffAnimations.TryGetValue(staffAnimations, out var value);
            return value;
        }
    }
}