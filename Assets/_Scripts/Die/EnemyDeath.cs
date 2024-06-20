using _Scripts.Animations;
using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class EnemyDeath : BaseDeath
    {
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;
        protected override void Init()
        {
            StateMachine = _stateMachine;
            GiveLive = _giveLive;
            RoundsCounter = _roundsCounter;
        }

        protected override void GiveWin()
        {
            var dieUI = new DieUI(RoundsCounter);

            dieUI.GiveWinRoundToPlayer();
            
            _stateMachine.EnemySwitchAnimation?.SwitchAnimation(EnemyAnimations.Death);
        }

        private void OnEnable()
        {
            EnemyHealth.OnDied += Death;
        }

        private void OnDisable()
        {
            EnemyHealth.OnDied -= Death;
        }
    }
}