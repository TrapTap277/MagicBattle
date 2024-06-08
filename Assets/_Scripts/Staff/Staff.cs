using System.Collections.Generic;
using _Scripts.Items;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Staff
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _gemMeshRenderer;
        
        [SerializeField] private Material _noneMaterial;
        [SerializeField] private Material _falseAttackMaterial;
        [SerializeField] private Material _trueAttackMaterial;
        [SerializeField] private Material _healMaterial;
        [SerializeField] private Material _doubleDamageMaterial;
        [SerializeField] private Material _doubleMoveMaterial;
        [SerializeField] private Material _protectionMaterial;
        
        private Dictionary<Gem, Material> _items;

        private void Start()
        {
            _items = new Dictionary<Gem, Material>
            {
                { Gem.None, _noneMaterial },
                { Gem.TrueAttack, _trueAttackMaterial },
                { Gem.FalseAttack, _falseAttackMaterial },
                { Gem.Heal, _healMaterial },
                { Gem.Damage, _doubleDamageMaterial },
                { Gem.Protection, _protectionMaterial },
                { Gem.SecondMove, _doubleMoveMaterial }
            };

            DeterminateGemAndChangeMaterial(Gem.SecondMove);
        }

        private void DeterminateGemAndChangeMaterial(Gem gem)
        {
            if (_items.TryGetValue(gem, out Material material))
            {
                ChangeMaterial(material);
            }
            else
            {
                Debug.LogWarning($"No material found for gem type {gem}");
            }
        }

        private void ChangeMaterial(Material material)
        {
            _gemMeshRenderer.material = material;
        }

        private void OnEnable()
        {
            BaseShoot.OnChangedGemOnStaff += DeterminateGemAndChangeMaterial;
        }

        private void OnDisable()
        {
            BaseShoot.OnChangedGemOnStaff -= DeterminateGemAndChangeMaterial;
        }
    }
    public enum Gem
    {
        None = 0,
        FalseAttack = 1,
        TrueAttack = 2,
        Heal = 3,
        Damage = 4,
        SecondMove = 5,
        Protection = 6
    }
}