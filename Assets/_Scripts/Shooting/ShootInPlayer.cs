﻿using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : IShoot
    {        
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
            }

            else
            {
                //Передать посох
            }
        }
    }
}