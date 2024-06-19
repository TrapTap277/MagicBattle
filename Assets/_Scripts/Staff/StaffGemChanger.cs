using System.Collections.Generic;
using _Scripts.Items;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffGemChanger : MonoBehaviour, IInit
    {
        [SerializeField] private RFX4_EffectSettings _effectPrefab;
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


        public void Init()
        {
            _effectPrefab.gameObject.SetActive(true);
            InitDictionaries();
            DeterminateGemAndChangeMaterial(Gem.None);
        }

        private void DeterminateGemAndChangeMaterial(Gem gem)
        {
            if (_gemMaterial.TryGetValue(gem, out var material) && _effectColor.TryGetValue(gem, out var color))
            {
                ChangeMaterial(material);
                CreateGemEffectWithColor(color);
            }
            else
            {
                Debug.LogWarning($"No material found for gem type {gem}");
            }
        }

        private void CreateGemEffectWithColor(Color color)
        {
            var gemMeshTransform = _gemMeshRenderer.transform;
            
            var newEffect = CreateEffect(gemMeshTransform, color);

            Destroy(newEffect.gameObject, 1f);
        }

        private RFX4_EffectSettings CreateEffect(Transform gemMeshTransform, Color color)
        {
            _effectPrefab.EffectColor = color;
            
            var newEffect =
                Instantiate(_effectPrefab, gemMeshTransform.position, Quaternion.identity, gemMeshTransform);
            return newEffect;
        }

        private void ChangeMaterial(Material material)
        {
            _gemMeshRenderer.material = material;
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