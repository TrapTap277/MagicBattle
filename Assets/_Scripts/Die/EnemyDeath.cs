using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private GiveLive _giveLive;

        
        public void Death()
        {
            _attackStorage.GenerateMagicAttacks();
            _stateMachine.SwitchState(_stateMachine.IdleState);
            _giveLive.RestoreHealth(200);
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