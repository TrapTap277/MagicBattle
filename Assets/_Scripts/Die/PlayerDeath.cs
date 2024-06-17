using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class PlayerDeath : BaseDeath
    {
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;

        private void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            StateMachine = _stateMachine;
            GiveLive = _giveLive;
            RoundsCounter = _roundsCounter;
        }

        protected override void GiveWin()
        {
            var dieUI = new DieUI(RoundsCounter);

            dieUI.GiveWinRoundToEnemy();
        }

        private void OnEnable()
        {
            PlayerHealth.OnDied += Death;
        }

        private void OnDisable()
        {
            PlayerHealth.OnDied -= Death;
        }
    }
}