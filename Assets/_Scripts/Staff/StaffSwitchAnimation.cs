﻿using System.Collections.Generic;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffSwitchAnimation : MonoBehaviour, IStaffAnimationController
    {
        private Animator _staffAnimator;

        private const string DefaultState = "StartStaffPositions";
        private const string FirstAttack = "StaffAttack";
        private const string SecondAttack = "StaffAttack2";
        private const string ThirdAttack = "StaffAttack3";
        private const string FadeStaff = "FadeStaff";
        private const string ShowStaff = "ShowStaff";
        private const string OpenPortal = "OpenPortal";
        private const string DissolveStaff = "DissolveStaff";
        private const string UnDissolveStaff = "UnDissolveStaff";

        private Dictionary<StaffAnimations, int> _staffAnimations = new Dictionary<StaffAnimations, int>();
        private List<int> _attackStates = new List<int>();

        private int _defaultState;
        private int _firstAttackState;
        private int _secondAttackState;
        private int _thirdAttackState;
        private int _fadeState;
        private int _showState;
        private int _openPortalState;
        private int _dissolveState;
        private int _unDissolveState;

        private void Awake()
        {
            _staffAnimator = GetComponent<Animator>();

            InitHashes();
            InitDictionary();
            InitList();
        }

        public void SwitchAnimation(StaffAnimations staffAnimations)
        {
            if (staffAnimations != StaffAnimations.None)
            {
                var value = GetStaffAnimation(staffAnimations);
                CrossFade(value);
                return;
            }

            var randomNumber = Random.Range(0, _attackStates.Count);
            CrossFade(randomNumber);
        }

        private int GetStaffAnimation(StaffAnimations staffAnimations)
        {
            _staffAnimations.TryGetValue(staffAnimations, out var value);
            return value;
        }

        private void CrossFade(int randomNumber)
        {
            _staffAnimator.CrossFade(
                randomNumber <= _attackStates.Count && randomNumber >= 0 ? _attackStates[randomNumber] : randomNumber,
                0);
        }

        private void InitHashes()
        {
            _defaultState = Animator.StringToHash(DefaultState);
            _firstAttackState = Animator.StringToHash(FirstAttack);
            _secondAttackState = Animator.StringToHash(SecondAttack);
            _thirdAttackState = Animator.StringToHash(ThirdAttack);
            _fadeState = Animator.StringToHash(FadeStaff);
            _showState = Animator.StringToHash(ShowStaff);
            _openPortalState = Animator.StringToHash(OpenPortal);
            _dissolveState = Animator.StringToHash(DissolveStaff);
            _unDissolveState = Animator.StringToHash(UnDissolveStaff);
        }

        private void InitDictionary()
        {
            _staffAnimations = new Dictionary<StaffAnimations, int>
            {
                {StaffAnimations.DefaultState, _defaultState},
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

        private void InitList()
        {
            _attackStates = new List<int> {_firstAttackState, _secondAttackState, _thirdAttackState};
        }
    }
}