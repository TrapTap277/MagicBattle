using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Die;
using _Scripts.EndGame;
using TMPro;
using UnityEngine;

namespace _Scripts.DialogueSystem
{
    public class DialogueStorage : MonoBehaviour, ISwitchDialogue
    {
        [SerializeField] private List<string> _enemyGeneralAnswers = new List<string>();
        [SerializeField] private List<string> _enemyWinAnswers = new List<string>();
        [SerializeField] private List<string> _enemyLoseAnswers = new List<string>();
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _textShowingSpeed = 0.02f;

        private int _generalIndex;
        private int _enemyWinIndex;
        private int _enemyLoseIndex;

        private void Awake()
        {
            DieManager.SetDialogueSwitcher(this);

            StopAllCoroutines();
        }

        public void SwitchDialogue(WhoWon whoWon)
        {
            StartCoroutine(Switch(whoWon));
        }

        private IEnumerator Switch(WhoWon whoWon)
        {
            if (GetDialogueIndex(whoWon) > GetDialogueList(whoWon).Count && GetDialogueIndex(whoWon) < GetDialogueList(whoWon).Count) yield break;

            ResetText();

            foreach (var phrase in GetDialogueList(whoWon)[GetDialogueIndex(whoWon)])
            {
                yield return new WaitForSeconds(_textShowingSpeed);
                _text.text += phrase;
            }            
            
            UpdateDialogueIndex(whoWon);
            
            yield return new WaitForSeconds(2);
            ResetText();
        }

        private void ResetText()
        {
            _text.text = "";
        }

        private List<string> GetDialogueList(WhoWon whoWon)
        {
            return whoWon switch
            {
                WhoWon.NoOne => _enemyGeneralAnswers,
                WhoWon.Enemy => _enemyWinAnswers,
                WhoWon.Player => _enemyLoseAnswers,
                _ => null
            };
        }

        private int GetDialogueIndex(WhoWon whoWon)
        {
            return whoWon switch
            {
                WhoWon.NoOne => _generalIndex,
                WhoWon.Enemy => _enemyWinIndex,
                WhoWon.Player => _enemyLoseIndex,
                _ => 0
            };
        }

        private void UpdateDialogueIndex(WhoWon whoWon)
        {
            switch (whoWon)
            {
                case WhoWon.NoOne:
                    _generalIndex++;
                    break;
                case WhoWon.Enemy:
                    _enemyWinIndex++;
                    break;
                case WhoWon.Player:
                    _enemyLoseIndex++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(whoWon), whoWon, null);
            }
        }
    }
}