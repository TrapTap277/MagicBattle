using System;
using System.Threading.Tasks;
using _Scripts.EndGame;

namespace _Scripts.Die
{
    public static class DieCounter
    {
        public static event Action OnSetStats;
        public static event Action<WhoWon> OnSetText;
        public static event Action OnPlayedDemonicEffect;
        public static event Action OnResetBarriers;

        private static int _enemyDieCount;
        private static int _playerDieCount;

        public static async void AddEnemyDies()
        {
            _enemyDieCount++;

            IsSomeoneDied();
            if (IsGameEnded())
            {
                OnPlayedDemonicEffect?.Invoke();
                await Task.Delay(6000);
                OnSetText?.Invoke(WhoWon.Player);
                await Task.Delay(7000);
                OnSetStats?.Invoke();
            }
        }

        public static async void AddPlayerDies()
        {
            _playerDieCount++;

            IsSomeoneDied();
            if (IsGameEnded())
            {
                OnPlayedDemonicEffect?.Invoke();
                await Task.Delay(6000);
                OnSetText?.Invoke(WhoWon.Enemy);
                await Task.Delay(7000);
                OnSetStats?.Invoke();
            }
        }

        private static void IsSomeoneDied()
        {
            OnResetBarriers?.Invoke();
        }

        private static bool IsGameEnded()
        {
            return _enemyDieCount >= 3 || _playerDieCount >= 3;
        }
    }
}