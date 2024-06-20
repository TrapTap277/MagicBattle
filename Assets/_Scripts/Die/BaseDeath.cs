using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public abstract class BaseDeath : MonoBehaviour
    {
        protected EnemyStateMachine StateMachine;
        protected GiveLive GiveLive;
        protected CanvasGroup RoundsCounter;

        private void Awake()
        {
            Init();
        }

        protected abstract void Init();

        protected void Death()
        {
            GiveWin();
            SwitchEnemyState();
            RestoreHealth();
        }

        protected abstract void GiveWin();

        private async void RestoreHealth()
        {
            await Task.Delay(2000);
            GiveLive.RestoreHealth(200);
        }

        private void SwitchEnemyState()
        {
            StateMachine.SetMoveTurn(MoveTurn.Player);
            StateMachine.SwitchState(StateMachine.IdleState);
        }
    }
}