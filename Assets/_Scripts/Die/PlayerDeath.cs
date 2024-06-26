using System.Threading.Tasks;
using _Scripts.Health;
using _Scripts.MainMenu;
using UnityEngine;

namespace _Scripts.Die
{
    public class PlayerDeath : BaseDeath
    {
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;

        private VignetteLerp _vignetteLerp;

        protected override void Init()
        {
            _vignetteLerp = FindObjectOfType<VignetteLerp>();
            GiveLive = _giveLive;
            RoundsCounter = _roundsCounter;
        }

        protected override async void GiveWin()
        {
            var dieUI = new DieUI(RoundsCounter);

            dieUI.GiveWinRoundToEnemy();

            if (DieManager.IsGameEnded()) return;

            _vignetteLerp.StopAllCoroutines();
            _vignetteLerp.ShowVignette();
            await Task.Delay(2000);

            await Task.Delay(2000);
            RestoreHealth();
            await Task.Delay(6000);
            _vignetteLerp.StopAllCoroutines();
            _vignetteLerp.StartCoroutine(_vignetteLerp.FadeVignette());
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