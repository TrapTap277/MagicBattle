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
            var newRound = Resources.Load<GameObject>("AttackUI");
            var newRoundPrefab = Object.Instantiate(newRound, _roundsCounter.transform);
            newRoundPrefab.GetComponent<Image>().color = Color.yellow;
        }

        public void GiveWinRoundToEnemy()
        {
            var newRound = Resources.Load<GameObject>("AttackUI");
            var newRoundPrefab = Object.Instantiate(newRound, _roundsCounter.transform);
            newRoundPrefab.GetComponent<Image>().color = Color.magenta;
        }
    }
}