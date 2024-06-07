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
        
        private void Awake()
        {
            _buttonToShootInEnemy.onClick.AddListener(() => ShootInEnemy());
            _buttonToShootInYou.onClick.AddListener(() => ShootInYou());

            _secondMoveTurn = SecondMoveTurn.None;
        }

        public void ShootInEnemy(bool isEnemy = false)
        {
            _baseShoot = new ShootInEnemy(_animator, _storage, _stateMachine, _secondMoveTurn, isEnemy);
            _baseShoot.Shoot();
            ResetSecondMove();
            ResetSkills();
        }

        public void ShootInYou(bool isEnemy = false)
        {
            _baseShoot = new ShootInPlayer(_animator, _storage, _stateMachine, _secondMoveTurn, isEnemy);
            _baseShoot.Shoot();
            ResetSecondMove();
            ResetSkills();
        }

        public void SetSecondMove(SecondMoveTurn secondMoveTurn)
        {
            _secondMoveTurn = secondMoveTurn;
        }

        private void ResetSkills()
        {
            HealthBase[] healthBase = FindObjectsOfType<HealthBase>();

            foreach (var health in healthBase)
            {
                health.ResetProperties();
            }
        }

        private void ResetSecondMove()
        {
            _secondMoveTurn = SecondMoveTurn.None;
        }
    }
}