namespace _Scripts.Health
{
    public class DieCounter
    {
        public static int _enemyDieCount {get; private set; }
        public static int _playerDieCount {get; private set; }
        
        public void AddEnemyDies()
        {
            _enemyDieCount++;
        }
        
        public void AddPlayerDies()
        {
            _playerDieCount++;
        }
    }
}