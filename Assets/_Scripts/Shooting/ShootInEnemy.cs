using _Scripts.Animations;
using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;

namespace _Scripts.Shooting
{
    public class ShootInEnemy : BaseShoot, IShoot
    {
        private readonly IEnemyStateSwitcher _enemyStateSwitcher;

        public ShootInEnemy(MagicAttackStorage attackStorage, EnemyStateMachine stateMachine,
            SecondMoveTurn secondMoveTurn, bool isEnemy, ISwitchAnimation<StaffAnimations> switchAnimation,
            ISetGem setGem, IEnableDisableManager attacksButtons) : base(attackStorage, switchAnimation, setGem, attacksButtons, secondMoveTurn)
        {
            _enemyStateSwitcher =
                new SwitchEnemyStateWithShootingInEnemy(isEnemy, secondMoveTurn, stateMachine, attackStorage);
        }

        public override void Shoot()
        {
            ShootBase(ShootIn.Enemy, _enemyStateSwitcher);
        }
    }
}