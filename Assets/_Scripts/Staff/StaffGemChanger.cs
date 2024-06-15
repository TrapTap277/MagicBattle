using System.Collections;
using System.Collections.Generic;
using _Scripts.Items;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffGemChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _gemMeshRenderer;

        [SerializeField] private Material _none;
        [SerializeField] private Material _falseAttack;
        [SerializeField] private Material _trueAttack;
        [SerializeField] private Material _healItem;
        [SerializeField] private Material _damageItem;
        [SerializeField] private Material _secondMoveItem;
        [SerializeField] private Material _protectionItem;

        private Dictionary<Gem, Material> _gemMaterial;
        private Dictionary<Gem, Color> _effectColor;

        private RFX4_EffectSettings _effect;

        private void Start()
        {
            _effect = FindObjectOfType<RFX4_EffectSettings>();
            InitDictionaries();
            DeterminateGemAndChangeMaterial(Gem.None);
        }

        private void DeterminateGemAndChangeMaterial(Gem gem)
        {
            if (_gemMaterial.TryGetValue(gem, out var material) && _effectColor.TryGetValue(gem, out var color))
            {
                ChangeMaterial(material);
                ChangeEffectColor(color);
            }
            else
            {
                Debug.LogWarning($"No material found for gem type {gem}");
            }
        }

        private void ChangeEffectColor(Color color)
        {
            _effect.EffectColor = color;
            StartCoroutine(EnableOrDisable(true));
            StartCoroutine(EnableOrDisable(false));
        }

        private void ChangeMaterial(Material material)
        {
            _gemMeshRenderer.material = material;
        }

        private IEnumerator EnableOrDisable(bool isEnabled)
        {
            if(!isEnabled)
                yield return new WaitForSeconds(0.5f);
            
            _effect.gameObject.SetActive(isEnabled);
        }

        private void InitDictionaries()
        {
            _gemMaterial = new Dictionary<Gem, Material>
            {
                {Gem.None, _none},
                {Gem.TrueAttack, _trueAttack},
                {Gem.FalseAttack, _falseAttack},
                {Gem.Heal, _healItem},
                {Gem.Damage, _damageItem},
                {Gem.Protection, _protectionItem},
                {Gem.SecondMove, _secondMoveItem}
            };


            _effectColor = new Dictionary<Gem, Color>
            {
                {Gem.None, Color.gray},
                {Gem.TrueAttack, Color.blue},
                {Gem.FalseAttack, Color.red},
                {Gem.Heal, Color.green},
                {Gem.Damage, Color.magenta},
                {Gem.Protection, Color.cyan},
                {Gem.SecondMove, Color.black}
            };
        }

        private void OnEnable()
        {
            BaseShoot.OnChangedGemOnStaff += DeterminateGemAndChangeMaterial;
            BaseItem.OnChangedGemOnStaff += DeterminateGemAndChangeMaterial;
        }

        private void OnDisable()
        {
            BaseShoot.OnChangedGemOnStaff -= DeterminateGemAndChangeMaterial;
            BaseItem.OnChangedGemOnStaff -= DeterminateGemAndChangeMaterial;
        }
    }
}