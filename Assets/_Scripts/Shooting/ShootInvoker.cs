using System;
using _Scripts.Animations;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Health;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
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

        private ISwitchAnimation<StaffAnimations> _switchAnimation;
        private ISetGem _setGem;
        private IEnableDisableManager _enableDisableManager;

        private void Awake()
        {
            RemoveButtonListeners();
            AddButtonListeners();

            _secondMoveTurn = SecondMoveTurn.None;
        }

        public void Init()
        {
            FindObjects();
        }

        public void ShootInEnemy(MoveTurn moveTurn = MoveTurn.Player)
        {
            if (moveTurn == MoveTurn.Player) SetIsStoppedFalse();
            
            _baseShoot = new ShootInEnemy(_storage, _stateMachine, _secondMoveTurn, moveTurn, _switchAnimation, _setGem,
                _enableDisableManager);
            _baseShoot.Shoot();
            ResetSecondMove();
        }

        public void ShootInPlayer(MoveTurn moveTurn = MoveTurn.Player)
        {
            if (moveTurn == MoveTurn.Player) SetIsStoppedFalse();
            
            _baseShoot = new ShootInPlayer(_storage, _stateMachine, _secondMoveTurn, moveTurn, _switchAnimation,
                _setGem, _enableDisableManager);
            _baseShoot.Shoot();
            ResetSecondMove();
        }

        private static void SetIsStoppedFalse()
        {
            OnStoppedIsStopped?.Invoke();
        }

        private void SetSecondMove(SecondMoveTurn secondMoveTurn)
        {
            if (_secondMoveTurn != secondMoveTurn) _secondMoveTurn = secondMoveTurn;
        }

        private void ResetSkills()
        {
            foreach (var health in _healthBase) health.ResetProperties();
        }

        private void ResetSecondMove()
        {
            _secondMoveTurn = SecondMoveTurn.None;
        }

        private void AddButtonListeners()
        {
            _buttonToShootInEnemy.onClick.AddListener(() => ShootInEnemy());
            _buttonToShootInYou.onClick.AddListener(() => ShootInPlayer());
        }

        private void RemoveButtonListeners()
        {
            _buttonToShootInEnemy.onClick.RemoveAllListeners();
            _buttonToShootInYou.onClick.RemoveAllListeners();
        }

        private void FindObjects()
        {
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();

            _healthBase = FindObjectsOfType<HealthBase>();
            _switchAnimation = FindObjectOfType<StaffAnimationSwitcher>();
            _setGem = FindObjectOfType<UseMagic>();
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