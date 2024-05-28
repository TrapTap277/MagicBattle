using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class MagicAttackUI : MonoBehaviour
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private GameObject _attack;
        [SerializeField] private CanvasGroup _attackPanel;
        
        private List<Image> _attacks = new List<Image>();
        
        public void CreateUI()
        {
            if (_attacks.Count != 0)
            {
                for (int i = 0; i < _attacks.Count; i++)
                {
                    Destroy(_attacks[i].gameObject);
                }
                _attacks.Clear();
            }

            int blueAttack = _attackStorage.BlueAttack;
            int redAttack = _attackStorage.RedAttack;
            int sum = blueAttack + redAttack;
            
            Sequence fade = DOTween.Sequence();
            fade.Append(_attackPanel.GetComponent<CanvasGroup>().DOFade(1, 2));

            for (int i = 0; i < _attackStorage.AttackCount; i++)
            {
                GameObject newBarrier = Instantiate(_attack, _attackPanel.transform);
                _attacks.Add(newBarrier.GetComponent<Image>());

                bool isBlue = false;
                if (blueAttack > 0 && redAttack > 0)
                    isBlue = Random.Range(0, 2) == 0;
                
                else if (blueAttack > 0)
                    isBlue = true;
                
                else
                    isBlue = false;

                Color attackColor = isBlue ? Color.blue : Color.red;
                newBarrier.GetComponent<Image>().color = attackColor;

                if (isBlue)
                    blueAttack--;
                else
                    redAttack--;
            }
            
            FadeUI();
        }

        private async void FadeUI()
        {
            await Task.Delay(6000);
            
            Sequence fade = DOTween.Sequence();
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