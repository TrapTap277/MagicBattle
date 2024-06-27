using System;
using System.Collections.Generic;
using _Scripts.Attacks;
using _Scripts.Items;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffGemChanger : MonoBehaviour, IInit
    {
        [SerializeField] private RFX4_EffectSettings _effectPrefab;

        private Dictionary<Gem, Color> _effectColor;
        private MeshRenderer _gemMeshRenderer;

        private Color _startColor;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _gemMeshRenderer = GetComponent<MeshRenderer>();
            InitDictionaries();
            ChangeColorMaterial(_effectColor[Gem.None]);
        }

        private void DeterminateGemAndChangeMaterial(Gem gem)
        {
            if (_effectColor.TryGetValue(gem, out var color))
            {
                ChangeColorMaterial(color);
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

            var newEffect = Instantiate(_effectPrefab, gemMeshTransform.position, Quaternion.identity,
                gemMeshTransform);
            return newEffect;
        }

        private void SetGemWithoutCreatingEffect(Gem gem)
        {
            ChangeColorMaterial(_effectColor[gem]);
        }

        private void ChangeColorMaterial(Color color)
        {
            _startColor = _gemMeshRenderer.material.GetColor(EmissionColor);
            _gemMeshRenderer.material.SetColor(EmissionColor, color);
        }

        private void InitDictionaries()
        {
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
            MagicAttackStorage.OnResetedGem += SetGemWithoutCreatingEffect;
        }

        private void OnDisable()
        {
            BaseShoot.OnChangedGemOnStaff -= DeterminateGemAndChangeMaterial;
            BaseItem.OnChangedGemOnStaff -= DeterminateGemAndChangeMaterial;
            MagicAttackStorage.OnResetedGem -= SetGemWithoutCreatingEffect;
        }

        private void OnApplicationQuit()
        {
            _gemMeshRenderer.material.SetColor("_Emission", _startColor);
        }
    }
}