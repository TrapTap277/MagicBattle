using _Scripts.Animations;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using _Scripts.Staff;
using _Scripts.Stats;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : BaseShoot
    {
        private readonly IEnemyStateSwitcher _enemyStateSwitcher;

        public ShootInPlayer(MagicAttackStorage attackStorage, EnemyStateMachine stateMachine,
            SecondMoveTurn secondMoveTurn, MoveTurn moveTurn,
            ISetGem setGem, IEnableDisableManager attacksButtons,
            EnemyAnimationSwitcher enemyAnimationSwitcher, StaffAnimationSwitcher staffAnimationSwitcher) : base(
            attackStorage, secondMoveTurn, enemyAnimationSwitcher, staffAnimationSwitcher, setGem, attacksButtons,
            moveTurn)
        {
            _enemyStateSwitcher =
                new SwitchEnemyStateWithShootingInPlayer(moveTurn, secondMoveTurn, stateMachine, attackStorage);
        }

        public override void Shoot()
        {
            ShootBase(ShootIn.Player, _enemyStateSwitcher);
        }
    }
}