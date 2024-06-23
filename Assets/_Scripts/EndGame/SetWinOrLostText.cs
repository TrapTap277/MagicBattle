using System.Collections;
using System.Collections.Generic;
using _Scripts.DialogueSystem;
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

        private DialogueAnswerType _who;

        private void Awake()
        {
            EnableOrDisable(false);
        }

        private void SetText(DialogueAnswerType dialogueAnswerType)
        {
            _who = dialogueAnswerType;

            var dictionary = new Dictionary<DialogueAnswerType, string>
            {
                {DialogueAnswerType.Lose, "You Are Won"},
                {DialogueAnswerType.Win, "You Are Lost"}
            };

            _text.text = dictionary[dialogueAnswerType];

            StartCoroutine(EnableOrDisable(true));
        }

        private void ChangeColor()
        {
            if (_who == DialogueAnswerType.Win)
                _text.DOColor(Color.red, TimeToChangeColor).OnComplete(FadeText);

            if (_who == DialogueAnswerType.Lose)
                _text.DOColor(Color.green, TimeToChangeColor).OnComplete(FadeText);
        }

        private IEnumerator EnableOrDisable(bool isEnabled)
        {
            _text.gameObject.SetActive(isEnabled);

            if (!isEnabled) yield return null;

            yield return new WaitForSeconds(3);

            ChangeColor();
        }

        private void FadeText()
        {
            _text.DOFade(0, TimeToChangeColor);
        }

        private void OnEnable()
        {
            DieManager.OnSetText += SetText;
        }

        private void OnDisable()
        {
            DieManager.OnSetText -= SetText;
        }
    }
}