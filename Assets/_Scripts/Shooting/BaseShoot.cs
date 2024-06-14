using System;
using System.Threading.Tasks;
using _Scripts.Attacks;
using _Scripts.LostScene;
using _Scripts.Staff;

namespace _Scripts.Shooting
{
    public abstract class BaseShoot
    {
        public static event Action<ShootIn> OnChangedStaffAttackPosition; 
        public static event Action<float> OnTakenDamageToPlayer;
        public static event Action<float> OnTakenDamageToEnemy;
        public static event Action<Gem> OnChangedGemOnStaff;

        private readonly MagicAttackStorage _attackStorage;
        private readonly IStaffAnimationController _staffAnimationController;
        private readonly ISetGem _setGem;

        private const StaffAnimations AttackAnimations = StaffAnimations.None;

        private Gem _gem;
        private AttacksType _currentAttack;
        private int _attackIndex;

        protected BaseShoot(MagicAttackStorage attackStorage,
            IStaffAnimationController staffAnimationController, ISetGem setGem)
        {
            _attackStorage = attackStorage;
            _staffAnimationController = staffAnimationController;
            _setGem = setGem;
        }

        public abstract void Shoot();

        protected async void ShootBase(ShootIn shootIn, IEnemyStateSwitcher enemyStateSwitcher)
        {
            await DeterminateAttack(shootIn);
            SwitchEnemyState(enemyStateSwitcher);
            RemoveAttackFromStorage();
        }

        private async Task DeterminateAttack(ShootIn shootIn)
        {
            _currentAttack = _attackStorage.GetFirstType();

            if (_currentAttack == AttacksType.Blue)
            {
                SetParameters(Gem.TrueAttack, 0);
                ChangeGemOnStaff();
                OnChangedStaffAttackPosition?.Invoke(shootIn);
                _staffAnimationController?.SwitchAnimation(AttackAnimations);
                await TakeDamage(shootIn);
                // Todo: Play True attack particles 
            }

            else
            {
                SetParameters(Gem.FalseAttack, 1);
                ChangeGemOnStaff();
                await Task.Delay(1000);
                _staffAnimationController?.SwitchAnimation(AttackAnimations);
                // Todo: Play False attack particles 
            }

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

        private static async Task TakeDamage(ShootIn shootIn)
        {
            await Task.Delay(2000);

            if (shootIn == ShootIn.Player) OnTakenDamageToPlayer?.Invoke(20);
            if (shootIn == ShootIn.Enemy) OnTakenDamageToEnemy?.Invoke(20);
        }

        private void SetParameters(Gem gem, int index)
        {
            _gem = gem;
            _attackIndex = index;
            _setGem?.SetGem(_gem);
        }
    }

    public enum ShootIn
    {
        NoOne,
        Player,
        Enemy
    }
}