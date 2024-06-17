using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Attacks
{
    public class MagicAttackUI : MonoBehaviour
    {
        [SerializeField] private GameObject _attack;
        [SerializeField] private CanvasGroup _attackPanel;

        private readonly List<Image> _attacks = new List<Image>();

        private void CreateUI(List<AttacksType> types)
        {
            var isBlue = types.Select(attackType => attackType == AttacksType.Blue);

            ResetAttacks();
            ShowAttacks();

            foreach (var isAttackBlue in isBlue)
            {
                var newAttack = CreateAndAddToList();
                ChangeColor(isAttackBlue, newAttack);
            }

            FadeUI();
        }

        private static void ChangeColor(bool isBlue, GameObject newAttack)
        {
            var attackColor = isBlue ? Color.blue : Color.red;
            newAttack.GetComponent<Image>().color = attackColor;
        }

        private GameObject CreateAndAddToList()
        {
            var newAttack = Instantiate(_attack, _attackPanel.transform);
            _attacks.Add(newAttack.GetComponent<Image>());
            return newAttack;
        }

        private void ShowAttacks()
        {
            var fade = DOTween.Sequence();
            fade.Append(_attackPanel.GetComponent<CanvasGroup>().DOFade(1, 2));
        }

        private void ResetAttacks()
        {
            if (_attacks.Count == 0) return;
            foreach (var attack in _attacks) Destroy(attack.gameObject);

            _attacks.Clear();
        }

        private async void FadeUI()
        {
            await Task.Delay(6000);

            var fade = DOTween.Sequence();
            fade.Append(_attackPanel.GetComponent<CanvasGroup>().DOFade(0, 2));
        }

        private void OnEnable()
        {
            MagicAttackStorage.OnCreatedUI += CreateUI;
        }

        private void OnDisable()
        {
            MagicAttackStorage.OnCreatedUI -= CreateUI;
        }
    }
}