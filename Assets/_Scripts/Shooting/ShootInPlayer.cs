using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : BaseShoot
    {
        private readonly IEnemyStateSwitcher _enemyStateSwitcher;

        public ShootInPlayer(MagicAttackStorage attackStorage, EnemyStateMachine stateMachine,
            SecondMoveTurn secondMoveTurn, bool isEnemy, IStaffAnimationController staffAnimationController,
            ISetGem setGem) : base(attackStorage, staffAnimationController, setGem)
        {
            _enemyStateSwitcher =
                new SwitchEnemyStateWithShootingInPlayer(isEnemy, secondMoveTurn, stateMachine, attackStorage);
        }

        public override void Shoot()
        {
            ShootBase(ShootIn.Player, _enemyStateSwitcher);
        }
    }
}