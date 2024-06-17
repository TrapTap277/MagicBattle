using System;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Health;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Shooting
{
    public class ShootInvoker : MonoBehaviour, IInit
    {
        public static event Action OnStoppedIsStopped;

        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private MagicAttackStorage _storage;
        [SerializeField] private Button _buttonToShootInEnemy;
        [SerializeField] private Button _buttonToShootInYou;

        private BaseShoot _baseShoot;
        private SecondMoveTurn _secondMoveTurn;
        private HealthBase[] _healthBase;

        private IStaffAnimationController _staffAnimationController;
        private ISetGem _setGem;
        private IEnableDisableManager _enableDisableManager;

        public void Init()
        {
            _buttonToShootInEnemy.onClick.AddListener(() => ShootInEnemy());
            _buttonToShootInYou.onClick.AddListener(() => ShootInPlayer());

            _secondMoveTurn = SecondMoveTurn.None;

            _enableDisableManager = FindObjectOfType<AttackButtonsController>();

            _healthBase = FindObjectsOfType<HealthBase>();
            _staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();
            _setGem = FindObjectOfType<UseMagic>();
        }

        public void ShootInEnemy(bool isEnemy = false)
        {
            if (isEnemy == false) SetIsStoppedFalse();
            _baseShoot = new ShootInEnemy(_storage, _stateMachine, _secondMoveTurn, isEnemy, _staffAnimationController,
                _setGem, _enableDisableManager);
            _baseShoot.Shoot();
            ResetSecondMove();
        }

        public void ShootInPlayer(bool isEnemy = false)
        {
            if (isEnemy == false) SetIsStoppedFalse();
            _baseShoot = new ShootInPlayer(_storage, _stateMachine, _secondMoveTurn, isEnemy, _staffAnimationController,
                _setGem, _enableDisableManager);
            _baseShoot.Shoot();
            ResetSecondMove();
        }

        private void SetIsStoppedFalse()
        {
            OnStoppedIsStopped?.Invoke();
        }

        private void SetSecondMove(SecondMoveTurn secondMoveTurn)
        {
            if (_secondMoveTurn != secondMoveTurn)
                _secondMoveTurn = secondMoveTurn;
        }

        private void ResetSkills()
        {
            foreach (var health in _healthBase) health.ResetProperties();
        }

        private void ResetSecondMove()
        {
            _secondMoveTurn = SecondMoveTurn.None;
        }

        private void OnEnable()
        {
            SecondMoveGemItem.OnGotSecondMove += SetSecondMove;
            BaseShoot.OnResetedItems += ResetSkills;
        }

        private void OnDisable()
        {
            SecondMoveGemItem.OnGotSecondMove -= SetSecondMove;
            BaseShoot.OnResetedItems -= ResetSkills;
        }
    }
}