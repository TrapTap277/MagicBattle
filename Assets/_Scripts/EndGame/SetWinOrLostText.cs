using System.Collections;
using System.Collections.Generic;
using _Scripts.Die;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.EndGame
{
    public class SetWinOrLostText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private const float TimeToChangeColor = 2f;

        private WhoWon _who;
        
        private void Awake()
        {
            EnableOrDisable(false);
        }

        private void SetText(WhoWon won)
        {
            _who = won;
            
            var dictionary = new Dictionary<WhoWon, string>
            {
                {WhoWon.Player, "You Are Won"},
                {WhoWon.Enemy, "You Are Lost"}
            };

            _text.text = dictionary[won];

            StartCoroutine(EnableOrDisable(true));
        }

        private void ChangeColor()
        {
            if (_who == WhoWon.Enemy)
                _text.DOColor(Color.red, TimeToChangeColor);

            if (_who == WhoWon.Player)
                _text.DOColor(Color.green, TimeToChangeColor);
        }

        private IEnumerator EnableOrDisable(bool isEnabled)
        {
            _text.gameObject.SetActive(isEnabled);
            
            yield return new WaitForSeconds(3);
            
            ChangeColor();
        }

        private void OnEnable()
        {
            DieCounter.OnSetText += SetText;
        }

        private void OnDisable()
        {
            DieCounter.OnSetText -= SetText;
        }
    }
}