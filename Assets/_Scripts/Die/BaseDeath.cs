using System;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public abstract class BaseDeath : MonoBehaviour
    {
        public static event Action OnReseted;

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
        }

        protected abstract void GiveWin();

        protected void RestoreHealth()
        {
            GiveLive.RestoreHealth(200);
        }

        protected static void SwitchEnemyState()
        {
            OnReseted?.Invoke();
        }
    }
}