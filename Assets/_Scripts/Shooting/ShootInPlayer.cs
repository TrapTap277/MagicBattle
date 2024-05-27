using System;
using _Scripts.Staff;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : IShoot
    {       
        public static event Action<Gem> OnChangedGemOnStaff;
        public static event Action<float> OnTookDamage;
        
        private MagicAttackStorage _attackStorage;
        private Animator _animator;
        
        public ShootInPlayer(Animator animator, MagicAttackStorage attackStorage)
        {
            _animator = animator;
            _attackStorage = attackStorage;
        }
        
        public void Shoot()
        {
            _animator.CrossFade("Metalstaff01FastSpin", 2);
            
            if (_attackStorage.GetFirstType() == AttacksType.Blue)
            {
                Debug.Log("Attack in player");
                //Create Spell
                
                OnChangedGemOnStaff?.Invoke(Gem.Blue);
                OnTookDamage?.Invoke(20);
            }

            else
            {
                OnChangedGemOnStaff?.Invoke(Gem.Red);
                
                //Передать посох
            }
        }
    }
}