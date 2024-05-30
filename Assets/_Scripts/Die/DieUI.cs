using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Die
{
    public class DieUI
    {
        private CanvasGroup _roundsCounter;

        public DieUI(CanvasGroup roundsCounter)
        {
            _roundsCounter = roundsCounter;
        }
        
        public void GiveWinRoundToPlayer()
        {
            GameObject newRound = Resources.Load<GameObject>("AttackUI");
            GameObject newRoundPrefab = GameObject.Instantiate(newRound, _roundsCounter.transform);
            newRoundPrefab.GetComponent<Image>().color = Color.yellow;
        }

        public void GiveWinRoundToEnemy()
        {
            GameObject newRound = Resources.Load<GameObject>("AttackUI");
            GameObject newRoundPrefab = GameObject.Instantiate(newRound, _roundsCounter.transform);
            newRoundPrefab.GetComponent<Image>().color = Color.magenta;
        }
    }
}