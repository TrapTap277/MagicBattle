using System;
using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private GiveLive _giveLive;
        [SerializeField] private CanvasGroup _roundsCounter;
        
        public void Death()
        {
            DieUI dieUI = new DieUI(_roundsCounter);

            _attackStorage.GenerateMagicAttacks();
            _stateMachine.SwitchState(_stateMachine.IdleState);
            _giveLive.RestoreHealth(200);
            dieUI.GiveWinRoundToEnemy();
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