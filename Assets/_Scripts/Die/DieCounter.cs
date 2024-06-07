namespace _Scripts.Die
{
    public class DieCounter
    {
        private static int EnemyDieCount {get; set; }
        private static int PlayerDieCount {get; set; }
        
        public static void AddEnemyDies()
        {
            EnemyDieCount++;
        }
        
        public static void AddPlayerDies()
        {
            PlayerDieCount++;
        }
    }
}