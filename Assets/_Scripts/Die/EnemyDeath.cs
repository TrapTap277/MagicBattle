using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Die
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private EnemyStateMachine _stateMachine;

        public void Death()
        {
            _attackStorage.GenerateMagicAttacks();
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }

        private void OnEnable()
        {
            EnemyHealth.OnEnemyDied += Death;
        }

        private void OnDisable()
        {
            EnemyHealth.OnEnemyDied -= Death;
        }
    }
}