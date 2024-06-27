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
        [SerializeField] private Sprite _gemBlue;
        [SerializeField] private Sprite _gemRed;
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
                ChangeSprite(isAttackBlue, newAttack);
            }
        }

        private void ChangeSprite(bool isBlue, Image newAttack)
        {
            var attackSprite = isBlue ? _gemBlue : _gemRed;
            newAttack.GetComponent<Image>().sprite = attackSprite;
        }

        private Image CreateAndAddToList()
        {
            var attack = new GameObject("Attack");
            attack.transform.SetParent(_attackPanel.transform);
            var attackImage = attack.AddComponent<Image>();
            _attacks.Add(attackImage);
            return attackImage;
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