using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class PlayerDeath : BaseDeath
    {
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;

        protected override void Init()
        {
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