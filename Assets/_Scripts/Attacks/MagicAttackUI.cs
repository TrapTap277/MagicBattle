using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Attacks
{
    public class MagicAttackUI : MonoBehaviour
    {
        [SerializeField] private GameObject _attack;
        [SerializeField] private CanvasGroup _attackPanel;

        private readonly List<Image> _attacks = new List<Image>();

        private IEnableDisableManager _attackShowAndFade;

        private void Awake()
        {
            _attackShowAndFade = FindObjectOfType<AttackShowAndFade>();
        }

        private void CreateUI(List<AttacksType> types)
        {
            var isBlue = types.Select(attackType => attackType == AttacksType.Blue);

            ResetAttacks();
            ChangeAttacksColor(isBlue);
            ShowAttacks();
        }

        private void ChangeAttacksColor(IEnumerable<bool> isBlue)
        {
            foreach (var isAttackBlue in isBlue)
            {
                var newAttack = CreateAndAddToList();
                ChangeColor(isAttackBlue, newAttack);
            }
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

        private async void ShowAttacks()
        {
            _attackShowAndFade?.Show();
            await Task.Delay(1000);
        }

        private void ResetAttacks()
        {
            if (_attacks.Count == 0) return;
            foreach (var attack in _attacks) Destroy(attack.gameObject);

            _attacks.Clear();
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