using _Scripts.Animations;
using _Scripts.Health;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Die
{
    public class EnemyDeath : BaseDeath
    {
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;

        private EnemyAnimationSwitcher _staffAnimationSwitcher;

        protected override void Init()
        {
            _staffAnimationSwitcher = FindObjectOfType<EnemyAnimationSwitcher>();
            GiveLive = _giveLive;
            RoundsCounter = _roundsCounter;
        }

        protected override void GiveWin()
        {
            var dieUI = new DieUI(RoundsCounter);

            dieUI.GiveWinRoundToPlayer();

            AnimationSwitcher<EnemyAnimations, ISwitchAnimation<EnemyAnimations>>.SwitchAnimation(
                _staffAnimationSwitcher, EnemyAnimations.Death);
        }

        private void OnEnable() => EnemyHealth.OnDied += Death;

        private void OnDisable() => EnemyHealth.OnDied -= Death;
    }
}