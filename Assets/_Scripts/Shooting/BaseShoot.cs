using System;
using _Scripts.Attacks;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Shooting
{
    public abstract class BaseShoot
    {
        public static event Action<float> OnTakenDamageToPlayer;
        public static event Action<float> OnTakenDamageToEnemy;
        public static event Action<Gem> OnChangedGemOnStaff;
        private readonly MagicAttackStorage _attackStorage;
        private readonly Animator _animator;
        private Gem _gem;
        private int _attackIndex;
        private AttacksType _currentAttack;

        protected BaseShoot(Animator animator, MagicAttackStorage attackStorage)
        {
            _attackStorage = attackStorage;
            _animator = animator;
        }

        public abstract void Shoot();

        protected void ShootBase(ShootIn shootIn, IEnemyStateSwitcher enemyStateSwitcher)
        {
            SetAnimation();
            DeterminateAttack(shootIn);
            SwitchEnemyState(enemyStateSwitcher);
            RemoveAttackFromStorage();
        }

        private void DeterminateAttack(ShootIn shootIn)
        {
            // if (_attackStorage == null)
            // {
            //     Debug.LogError("Attack storage is null");
            //     return;
            // }


            _currentAttack = _attackStorage.GetFirstType();
            
            if (_currentAttack == AttacksType.Blue)
            {
                SetParameters(Gem.TrueAttack, 0);
                TakeDamage(shootIn);
            }

            else
            {
                SetParameters(Gem.FalseAttack, 1);

            }

            ChangeGemOnStaff();
        }

        private void RemoveAttackFromStorage()
        {
            _attackStorage.RemoveAttack(_currentAttack);
        }

        private void SwitchEnemyState(IEnemyStateSwitcher enemyStateSwitcher)
        {
            enemyStateSwitcher.SwitchState(_attackIndex);
        }

        private void SetAnimation()
        {
            _animator.CrossFade("Metalstaff01FastSpin", 2);
        }

        private void ChangeGemOnStaff()
        {
            OnChangedGemOnStaff?.Invoke(_gem);
        }

        private static void TakeDamage(ShootIn shootIn)
        {
            if (shootIn == ShootIn.Player) OnTakenDamageToPlayer?.Invoke(20);

            if (shootIn == ShootIn.Enemy) OnTakenDamageToEnemy?.Invoke(20);
        }

        private void SetParameters(Gem gem, int index)
        {
            _gem = gem;
            _attackIndex = index;
        }
    }

    public enum ShootIn
    {
        NoOne,
        Player,
        Enemy
    }
} 