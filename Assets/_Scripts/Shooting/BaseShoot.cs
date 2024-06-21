using System;
using System.Threading.Tasks;
using _Scripts.Animations;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;

namespace _Scripts.Shooting
{
    public abstract class BaseShoot
    {
        public static event Action<ShootIn> OnSetShootIn;

        public static event Action<Gem> OnChangedGemOnStaff;
        public static event Action OnResetedItems;

        private readonly MagicAttackStorage _attackStorage;
        private readonly SecondMoveTurn _secondMoveTurn;
        private readonly EnemyAnimationSwitcher _enemyAnimationSwitcher;
        private readonly StaffAnimationSwitcher _staffAnimationSwitcher;
        private readonly ISetGem _setGem;
        private readonly IEnableDisableManager _attacksButtons;
        private readonly MoveTurn _move;

        private AttacksType _currentAttack;
        private Gem _gem;

        private int _attackIndex;

        protected BaseShoot(MagicAttackStorage attackStorage, SecondMoveTurn secondMoveTurn,
            EnemyAnimationSwitcher enemyAnimationSwitcher, StaffAnimationSwitcher staffAnimationSwitcher,
            ISetGem setGem, IEnableDisableManager attacksButtons, MoveTurn move)
        {
            _attackStorage = attackStorage;
            _secondMoveTurn = secondMoveTurn;
            _enemyAnimationSwitcher = enemyAnimationSwitcher;
            _staffAnimationSwitcher = staffAnimationSwitcher;
            _setGem = setGem;
            _attacksButtons = attacksButtons;
            _move = move;
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
                OnSetShootIn?.Invoke(shootIn);
                SetParameters(Gem.TrueAttack, 0);
                ChangeGemOnStaff();
                SetRandomAttackAnimation();
                await Task.Delay(2000);
                OnResetedItems?.Invoke();
                // Todo: Play True attack particles 
            }

            else
            {
                SetParameters(Gem.FalseAttack, 1);
                ChangeGemOnStaff();
                //Todo Maybe there need to add  OnResetItems?.Invoke();
                // Todo: Play False attack particles 
            }
        }

        private void SetRandomAttackAnimation()
        {
            if (_move == MoveTurn.Enemy)
                AnimationSwitcher<EnemyAnimations, ISwitchAnimation<EnemyAnimations>>
                    .SetRandomAnimation(_enemyAnimationSwitcher);

            else
                AnimationSwitcher<StaffAnimations, ISwitchAnimation<StaffAnimations>>
                    .SetRandomAnimation(_staffAnimationSwitcher);
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