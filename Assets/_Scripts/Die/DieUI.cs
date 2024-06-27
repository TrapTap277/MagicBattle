using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Die
{
    public class DieUI
    {
        private readonly CanvasGroup _roundsCounter;

        public DieUI(CanvasGroup roundsCounter)
        {
            _roundsCounter = roundsCounter;
        }

        public void GiveWinRoundToPlayer()
        {
            CreateWinUI("PlayerWin", "WinGem");
        }

        public void GiveWinRoundToEnemy()
        {
            CreateWinUI("EnemyWin", "LoseGem");
        }

        private void CreateWinUI(string name, string spriteName)
        {
            var newRound = new GameObject(name);
            newRound.AddComponent<Image>().sprite = Resources.Load<Sprite>(spriteName);
            newRound.transform.SetParent(_roundsCounter.transform);
        }
    }
}