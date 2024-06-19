using System;
using System.Threading.Tasks;
using _Scripts.DialogueSystem;
using _Scripts.EndGame;

namespace _Scripts.Die
{
    public static class DieManager
    {
        public static event Action<WhoWon> OnSetText;

        public static event Action OnCreatedBoxWithItems;
        public static event Action OnEnteredEnemyInIdle;
        public static event Action OnSetStats;
        public static event Action OnPlayedDemonicEffect;
        public static event Action OnBlockedTransition;
        public static int EnemyDieCount { get; private set; }
        public static int PlayerDieCount { get; private set; }
        private static ISwitchDialogue _switch;

        public static async void AddEnemyDies()
        {
            EnemyDieCount++;
            await IsSomeoneDied(WhoWon.Player);
            if (IsGameEnded()) await IfGameEnded(WhoWon.Player);
        }

        public static async void AddPlayerDies()
        {
            PlayerDieCount++;
            await IsSomeoneDied(WhoWon.Enemy);
            if (IsGameEnded()) await IfGameEnded(WhoWon.Enemy);
        }

        public static void SetDialogueSwitcher(ISwitchDialogue switchDialogue)
        {
            _switch = switchDialogue;
        }

        public static bool IsGameEnded()
        {
            return EnemyDieCount >= 3 || PlayerDieCount >= 3;
        }

        private static async Task IfGameEnded(WhoWon whoWon)
        {
            OnPlayedDemonicEffect?.Invoke();
            await Task.Delay(6000);
            OnSetText?.Invoke(whoWon);
            await Task.Delay(7000);
            OnSetStats?.Invoke();
        }

        private static async Task IsSomeoneDied(WhoWon whoWon)
        {
            OnEnteredEnemyInIdle?.Invoke();
            await Task.Delay(3000);
            _switch?.SwitchDialogue(WhoWon.NoOne);
            await Task.Delay(2000);
            _switch?.SwitchDialogue(whoWon);
            await Task.Delay(3000);
            OnBlockedTransition?.Invoke();
            OnCreatedBoxWithItems?.Invoke();
        }
    }
}