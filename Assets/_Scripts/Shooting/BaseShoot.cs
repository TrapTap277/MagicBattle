using System;
using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.Attacks;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.Shooting
{
    public abstract class BaseShoot
    {
        public static event Action<ShootIn> OnChangedStaffAttackPosition;
        public static event Action<Gem> OnChangedGemOnStaff;
        public static event Action<float> OnTakenDamageToPlayer;
        public static event Action<float> OnTakenDamageToEnemy;
        public static event Action OnResetedItems;

        private readonly MagicAttackStorage _attackStorage;
        private readonly SecondMoveTurn _secondMoveTurn;
        private readonly ISwitchAnimation<StaffAnimations> _switchAnimation;
        private readonly ISetGem _setGem;
        private readonly IEnableDisableManager _attacksButtons;

        private Gem _gem;
        private AttacksType _currentAttack;
        private int _attackIndex;

        protected BaseShoot(MagicAttackStorage attackStorage,
            ISwitchAnimation<StaffAnimations> switchAnimation, ISetGem setGem,
            IEnableDisableManager attacksButtons,
            SecondMoveTurn secondMoveTurn)
        {
            _switchAnimation = switchAnimation;
            _attacksButtons = attacksButtons;
            _secondMoveTurn = secondMoveTurn;
            _attackStorage = attackStorage;
            _setGem = setGem;
        }

        public abstract void Shoot();

        protected async void ShootBase(ShootIn shootIn, IEnemyStateSwitcher enemyStateSwitcher)
        {
            await DeterminateAttack(shootIn);
            SwitchEnemyState(enemyStateSwitcher);
            RemoveAttackFromStorage();
            ResetItems();
        }

        private async Task DeterminateAttack(ShootIn shootIn)
        {
            _currentAttack = _attackStorage.GetFirstType();

            if (_currentAttack == AttacksType.Blue)
            {
                //FadeAttackButtons();
                SetParameters(Gem.TrueAttack, 0);
                ChangeGemOnStaff();
                OnChangedStaffAttackPosition?.Invoke(shootIn);
                SetRandomAttackAnimation();
                await TakeDamage(shootIn);
                OnResetedItems?.Invoke();
                // Todo: Play True attack particles 
            }

            else
            {
                SetParameters(Gem.FalseAttack, 1);
                ChangeGemOnStaff();
                // Todo: Play False attack particles 
            }
        }

        private void SetRandomAttackAnimation()
        {
            var randomAttackAnimation = _switchAnimation.SetRandomAttackAnimation();

            _switchAnimation?.SwitchAnimation(randomAttackAnimation);
        }

        private async void FadeAttackButtons()
        {
            if (_secondMoveTurn != SecondMoveTurn.Player) return;
            _attacksButtons?.Fade();
            await Task.Delay(5000);
            _attacksButtons?.Show();
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

        private static void ResetItems()
        {
            OnResetedItems?.Invoke();
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