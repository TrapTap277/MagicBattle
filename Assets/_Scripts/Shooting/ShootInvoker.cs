using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Health;
using _Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Shooting
{
    public class ShootInvoker : MonoBehaviour
    {
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private MagicAttackStorage _storage;
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _buttonToShootInEnemy;
        [SerializeField] private Button _buttonToShootInYou;

        private BaseShoot _baseShoot;
        private SecondMoveTurn _secondMoveTurn;
        private HealthBase[] _healthBase;

        private void Start()
        {
            _healthBase = FindObjectsOfType<HealthBase>();
        }

        private void Awake()
        {
            _buttonToShootInEnemy.onClick.AddListener(() => ShootInEnemy());
            _buttonToShootInYou.onClick.AddListener(() => ShootInPlayer());

            _secondMoveTurn = SecondMoveTurn.None;
        }

        public void ShootInEnemy(bool isEnemy = false)
        {
            _baseShoot = new ShootInEnemy(_animator, _storage, _stateMachine, _secondMoveTurn, isEnemy);
            _baseShoot.Shoot();
            ResetSecondMove();
            ResetSkills();
            Debug.LogWarning("Shoot in Enemy");
        }

        public void ShootInPlayer(bool isEnemy = false)
        {
            Debug.LogWarning("Shoot in Player");
            _baseShoot = new ShootInPlayer(_animator, _storage, _stateMachine, _secondMoveTurn, isEnemy);
            _baseShoot.Shoot();
            ResetSecondMove();
            ResetSkills();
        }

        private void SetSecondMove(SecondMoveTurn secondMoveTurn)
        {
            if(_secondMoveTurn != secondMoveTurn)
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
        }

        private void OnDisable()
        {
            SecondMoveGemItem.OnGotSecondMove -= SetSecondMove;
        }
    }
}