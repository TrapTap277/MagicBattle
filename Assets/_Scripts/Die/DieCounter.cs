namespace _Scripts.Die
{
    public class DieCounter
    {
        private static int _enemyDieCount;
        private static int _playerDieCount;

        public static void AddEnemyDies()
        {
            _enemyDieCount++;
        }

        public static void AddPlayerDies()
        {
            _playerDieCount++;
        }

        public static bool IsSomeoneDied()
        {
            return _enemyDieCount > 0 || _playerDieCount > 0;
        }
    }
}