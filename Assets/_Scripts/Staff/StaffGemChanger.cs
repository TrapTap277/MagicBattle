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

        private Dictionary<Gem, Material> _items;

        private void Start()
        {
            InitDictionary();
        }

        private void DeterminateGemAndChangeMaterial(Gem gem)
        {
            if (_items.TryGetValue(gem, out var material))
                ChangeMaterial(material);
            else
                Debug.LogWarning($"No material found for gem type {gem}");
        }

        private void ChangeMaterial(Material material)
        {
            _gemMeshRenderer.material = material;
        }

        private void InitDictionary()
        {
            _items = new Dictionary<Gem, Material>
            {
                {Gem.None, _none},
                {Gem.TrueAttack, _trueAttack},
                {Gem.FalseAttack, _falseAttack},
                {Gem.Heal, _healItem},
                {Gem.Damage, _damageItem},
                {Gem.Protection, _protectionItem},
                {Gem.SecondMove, _secondMoveItem}
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