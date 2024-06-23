using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Die;
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
        [SerializeField] private int _textShowingSpeed;

        private int _generalIndex;
        private int _enemyWinIndex;
        private int _enemyLoseIndex;

        private void Awake()
        {
            DieManager.SetDialogueSwitcher(this);

            StopAllCoroutines();
        }

        public async Task SwitchDialogue(DialogueAnswerType answerType, int count)
        {
            for (var i = 0; i < count; i++)
            {
                await Switch(answerType);
                await Task.Delay(1000);
            }
        }

        private async Task Switch(DialogueAnswerType answerType)
        {
            if (GetDialogueIndex(answerType) > GetDialogueList(answerType).Count &&
                GetDialogueIndex(answerType) < GetDialogueList(answerType).Count) return;

            ResetText();

            foreach (var phrase in GetDialogueList(answerType)[GetDialogueIndex(answerType)])
            {
                await Task.Delay(_textShowingSpeed);
                _text.text += phrase;
            }

            UpdateDialogueIndex(answerType);

            await Task.Delay(1000);

            ResetText();
        }

        private void ResetText()
        {
            _text.text = "";
        }

        private List<string> GetDialogueList(DialogueAnswerType answerType)
        {
            return answerType switch
            {
                DialogueAnswerType.General => _enemyGeneralAnswers,
                DialogueAnswerType.Win => _enemyWinAnswers,
                DialogueAnswerType.Lose => _enemyLoseAnswers,
                _ => null
            };
        }

        private int GetDialogueIndex(DialogueAnswerType answerType)
        {
            return answerType switch
            {
                DialogueAnswerType.General => _generalIndex,
                DialogueAnswerType.Win => _enemyWinIndex,
                DialogueAnswerType.Lose => _enemyLoseIndex,
                _ => 0
            };
        }

        private void UpdateDialogueIndex(DialogueAnswerType answerType)
        {
            switch (answerType)
            {
                case DialogueAnswerType.General:
                    _generalIndex++;
                    break;
                case DialogueAnswerType.Win:
                    _enemyWinIndex++;
                    break;
                case DialogueAnswerType.Lose:
                    _enemyLoseIndex++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(answerType), answerType, null);
            }
        }
    }
}