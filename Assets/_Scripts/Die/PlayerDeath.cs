using System;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Die
{
    public class PlayerDeath : MonoBehaviour
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
            PlayerHealth.OnPlayerDied += Death;
        }

        private void OnDisable()
        {
            PlayerHealth.OnPlayerDied -= Death;
        }
    }
}