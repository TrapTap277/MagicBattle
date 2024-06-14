using System;
using _Scripts.Attacks;
using _Scripts.Staff;

namespace _Scripts.Shooting
{
    public abstract class BaseShoot
    {
        public static event Action<float> OnTakenDamageToPlayer;
        public static event Action<float> OnTakenDamageToEnemy;
        public static event Action<Gem> OnChangedGemOnStaff;
        private readonly MagicAttackStorage _attackStorage;
        private Gem _gem;
        private int _attackIndex;
        private AttacksType _currentAttack;

        protected BaseShoot(MagicAttackStorage attackStorage)
        {
            _attackStorage = attackStorage;
        }

        public abstract void Shoot();

        protected void ShootBase(ShootIn shootIn, IEnemyStateSwitcher enemyStateSwitcher)
        {
            DeterminateAttack(shootIn);
            SwitchEnemyState(enemyStateSwitcher);
            RemoveAttackFromStorage();
        }

        private void DeterminateAttack(ShootIn shootIn)
        {
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