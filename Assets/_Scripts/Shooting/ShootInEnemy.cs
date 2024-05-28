using System;
using _Scripts.Enemy;
using _Scripts.Staff;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInEnemy : IShoot
    {
        public static event Action<Gem> OnChangedGemOnStaff;
        public static event Action<float> OnTookDamage;
        
        private MagicAttackStorage _attackStorage;
        private Animator _animator;
        private EnemyStateMachine _stateMachine;

        private bool _isEnemy;
        
        public ShootInEnemy(Animator animator, MagicAttackStorage attackStorage, EnemyStateMachine stateMachine, bool isEnemy)
        {
            _attackStorage = attackStorage;
            _animator = animator;
            _stateMachine = stateMachine;
            _isEnemy = isEnemy;
        }

        public void Shoot()
        {
            _animator.CrossFade("Metalstaff01SlowSpin", 2);

            if (_attackStorage.GetFirstType() == AttacksType.Blue)
            {
                Debug.Log("Attack in Enemy");
                
                OnChangedGemOnStaff?.Invoke(Gem.Blue); 
                OnTookDamage?.Invoke(20);
                //Create Spell
                
                if(!_isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
            }

            else
            {          
                OnChangedGemOnStaff?.Invoke(Gem.Red);  
                
                _stateMachine.SwitchState(_stateMachine.AttackState);
                //Передать посох
            }

        }
    }
}