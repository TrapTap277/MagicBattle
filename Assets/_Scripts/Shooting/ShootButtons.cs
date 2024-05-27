using _Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Shooting
{
    public class ShootButtons : MonoBehaviour
    {
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private MagicAttackStorage _storage;
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _buttonToShootInEnemy;
        [SerializeField] private Button _buttonToShootInYou;

        private void Awake()
        {
            _buttonToShootInEnemy.onClick.AddListener(() => ShootInEnemy());
            _buttonToShootInYou.onClick.AddListener(() => ShootInYou());
        }

        public void ShootInEnemy(bool isEnemy = false)
        {
            IShoot enemy = new ShootInEnemy(_animator, _storage, _stateMachine, isEnemy);
            enemy.Shoot();
        }

        public void ShootInYou(bool isEnemy = false)
        {
            IShoot player = new ShootInPlayer(_animator, _storage, _stateMachine, isEnemy);
            player.Shoot();
        }
    }
}