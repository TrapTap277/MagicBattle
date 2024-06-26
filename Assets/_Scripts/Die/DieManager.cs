using System;
using System.Threading.Tasks;
using _Scripts.DialogueSystem;

namespace _Scripts.Die
{
    public static class DieManager
    {
        public static event Action<DialogueAnswerType> OnSetText;

        public static event Action OnCreatedBoxWithItems;
        public static event Action OnEnteredEnemyInIdle;
        public static event Action OnSetStats;
        public static event Action OnPlayedDemonicEffect;
        public static event Action OnBlockedTransition;
        public static int EnemyDieCount { get; private set; }
        public static int PlayerDieCount { get; private set; }
        private static ISwitchDialogue _switch;

        private static int _counter;

        public static async void AddEnemyDies()
        {
            EnemyDieCount++;
            await IsSomeoneDied(DialogueAnswerType.Lose);
            if (IsGameEnded()) IfGameEnded(DialogueAnswerType.Lose);
        }

        public static async void AddPlayerDies()
        {
            PlayerDieCount++;
            await IsSomeoneDied(DialogueAnswerType.Win);
            if (IsGameEnded()) IfGameEnded(DialogueAnswerType.Win);
        }

        public static void SetDialogueSwitcher(ISwitchDialogue switchDialogue)
        {
            _switch = switchDialogue;
        }

        public static bool IsGameEnded()
        {
            return EnemyDieCount >= 3 || PlayerDieCount >= 3;
        }

        private static async Task IfGameEnded(DialogueAnswerType dialogueAnswerType)
        {
            await _switch?.SwitchDialogue(dialogueAnswerType, 1);
            await Task.Delay(1000);
            _switch?.Fade();
            OnPlayedDemonicEffect?.Invoke();
            await Task.Delay(6000);
            OnSetText?.Invoke(dialogueAnswerType);
            await Task.Delay(7000);
            OnSetStats?.Invoke();
        }

        private static async Task IsSomeoneDied(DialogueAnswerType dialogueAnswerType)
        {
            OnEnteredEnemyInIdle?.Invoke();
            await Task.Delay(5000);
            await _switch?.SwitchDialogue(dialogueAnswerType, 1);
            OnBlockedTransition?.Invoke();
            if (_counter <= 1)
            {
                await _switch?.SwitchDialogue(DialogueAnswerType.General, 1);
                _counter++;
            }

            if (!IsGameEnded())
            {
                _switch?.Fade();
                await Task.Delay(2000);
                OnCreatedBoxWithItems?.Invoke();
            }
        }
    }
}