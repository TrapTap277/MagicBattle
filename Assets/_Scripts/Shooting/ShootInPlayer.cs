using System;
using _Scripts.Enemy;
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
        private EnemyStateMachine _stateMachine;

        private bool _isEnemy;
        
        public ShootInPlayer(Animator animator, MagicAttackStorage attackStorage, EnemyStateMachine stateMachine, bool isEnemy)
        {
            _attackStorage = attackStorage;
            _animator = animator;
            _stateMachine = stateMachine;
            _isEnemy = isEnemy;
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
                
                if(!_isEnemy)
                    _stateMachine.EnterInAttackState();
            }

            else
            {
                OnChangedGemOnStaff?.Invoke(Gem.Red);
                
                //Передать посох
            }
        }
    }
}