using System;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.Staff
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _gemMeshRenderer;
        
        [SerializeField] private Material _grey;
        [SerializeField] private Material _red;
        [SerializeField] private Material _blue;

        public void ChangeGemMaterial(Gem gem)
        {
            switch (gem)
            {
                case Gem.Grey:
                    _gemMeshRenderer.material = _grey;
                    break;
                
                case Gem.Red:
                    _gemMeshRenderer.material = _red;
                    break;
                
                case Gem.Blue:
                    _gemMeshRenderer.material = _blue;
                    break;
            }
            
        }

        private void OnEnable()
        {
            ShootInEnemy.OnChangedGemOnStaff += ChangeGemMaterial;
            ShootInPlayer.OnChangedGemOnStaff += ChangeGemMaterial;
        }

        private void OnDisable()
        {
            ShootInEnemy.OnChangedGemOnStaff -= ChangeGemMaterial;
            ShootInPlayer.OnChangedGemOnStaff -= ChangeGemMaterial;
        }
    }
    public enum Gem
    {
        Grey,
        Red,
        Blue
    }
}