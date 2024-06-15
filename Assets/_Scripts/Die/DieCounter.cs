using System;

namespace _Scripts.Die
{
    public static class DieCounter
    {
        public static event Action OnSetText;
        public static event Action OnResetBarriers;

        private static int _enemyDieCount;
        private static int _playerDieCount;

        public static void AddEnemyDies()
        {
            _enemyDieCount++;

            IsSomeoneDied();
            if (IsGameEnded()) OnSetText?.Invoke();
        }

        public static void AddPlayerDies()
        {
            _playerDieCount++;

            IsSomeoneDied();
            if (IsGameEnded()) OnSetText?.Invoke();
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