using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public abstract class BaseDeath : MonoBehaviour
    {
        protected MagicAttackStorage AttackStorage;
        protected EnemyStateMachine StateMachine;
        protected GiveLive GiveLive;
        protected CanvasGroup RoundsCounter;

        protected abstract void Init();

        protected void Death()
        {
            GiveWin();
            GenerateNewAttacks();
            SwitchEnemyState();
            RestoreHealth();
        }

        private void GiveWin()
        {
            var dieUI = new DieUI(RoundsCounter);

            dieUI.GiveWinRoundToPlayer();
        }

        private void RestoreHealth()
        {
            GiveLive.RestoreHealth(200);
        }

        private void SwitchEnemyState()
        {
            StateMachine.SetMoveTurn(MoveTurn.Player);
            StateMachine.SwitchState(StateMachine.IdleState);
        }

        private void GenerateNewAttacks()
        {
            AttackStorage.GenerateMagicAttacks();
        }
    }
}