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

        public void ChangeGemMaterial(Gem gem)
        {
            switch (gem)
            {
                case Gem.None:
                    _gemMeshRenderer.material = _noneMaterial;
                    break;
                
                case Gem.FalseAttack:
                    _gemMeshRenderer.material = _falseAttackMaterial;
                    break;
                
                case Gem.TrueAttack:
                    _gemMeshRenderer.material = _trueAttackMaterial;
                    break;
                
                case Gem.Heal:
                    _gemMeshRenderer.material = _healMaterial;
                    break;
                
                case Gem.Damage:
                    _gemMeshRenderer.material = _doubleDamageMaterial;
                    break;
                
                case Gem.SecondMove:
                    _gemMeshRenderer.material = _doubleMoveMaterial;
                    break;
                
                case Gem.Protect:
                    _gemMeshRenderer.material = _protectionMaterial;
                    break;
            }
        }

        private void OnEnable()
        {
            ShootInEnemy.OnChangedGemOnStaff += ChangeGemMaterial;
            ShootInPlayer.OnChangedGemOnStaff += ChangeGemMaterial;
            CurrentItem.OnChangedGemOnStaff += ChangeGemMaterial;
        }

        private void OnDisable()
        {
            ShootInEnemy.OnChangedGemOnStaff -= ChangeGemMaterial;
            ShootInPlayer.OnChangedGemOnStaff -= ChangeGemMaterial;
            CurrentItem.OnChangedGemOnStaff -= ChangeGemMaterial;
        }
    }
    public enum Gem
    {
        None,
        FalseAttack,
        TrueAttack,
        Heal,
        Damage,
        SecondMove,
        Protect
    }
}