using System;
using _Scripts.Enemy;
using _Scripts.Items;
using _Scripts.Staff;
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
        private bool _isHasSecondMoveForPlayer;
        private bool _isHasSecondMoveForEnemy;

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
                
                OnChangedGemOnStaff?.Invoke(Gem.TrueAttack); 
                OnTookDamage?.Invoke(20);
                //Create Spell
                
                if(_isHasSecondMoveForPlayer == false && !_isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
                
                if(_isHasSecondMoveForEnemy && _isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);

                _isHasSecondMoveForPlayer = false;
                _isHasSecondMoveForEnemy = false;
            }

            else
            {          
                OnChangedGemOnStaff?.Invoke(Gem.FalseAttack);  
                
                if(_isHasSecondMoveForPlayer == false)
                    _stateMachine.SwitchState(_stateMachine.AttackState);

                if(_isHasSecondMoveForEnemy && _isEnemy)
                    _stateMachine.SwitchState(_stateMachine.AttackState);
                
                //Передать посох

                _isHasSecondMoveForPlayer = false;
                _isHasSecondMoveForEnemy = false;
            }

        }

        public void GetSecondMoveForThePlayer()
        {
            _isHasSecondMoveForPlayer = true;
        }
        
        public void GetSecondMoveForTheEnemy()
        {
            _isHasSecondMoveForEnemy = true;
        }
        
        private void OnEnable()
        {
            DoubleMoveGemItem.OnGotSecondMoveToPlayer += GetSecondMoveForThePlayer;
            DoubleMoveGemItem.OnGotSecondMoveToEnemy += GetSecondMoveForTheEnemy;
        }

        private void OnDisable()
        {
            DoubleMoveGemItem.OnGotSecondMoveToPlayer -= GetSecondMoveForThePlayer;
            DoubleMoveGemItem.OnGotSecondMoveToEnemy -= GetSecondMoveForTheEnemy;
        }
    }
}