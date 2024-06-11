using _Scripts.Attacks;
using _Scripts.Enemy;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : BaseShoot
    {
        private readonly IEnemyStateSwitcher _enemyStateSwitcher;

        public ShootInPlayer(Animator animator, MagicAttackStorage attackStorage, EnemyStateMachine stateMachine,
            SecondMoveTurn secondMoveTurn, bool isEnemy) : base(animator, attackStorage)
        {
            _enemyStateSwitcher = new SwitchEnemyStateWithShootingInPlayer(isEnemy, secondMoveTurn, stateMachine, attackStorage);
        }

        public override void Shoot()
        {
            ShootBase(ShootIn.Player, _enemyStateSwitcher);
            // Debug.LogError($"Shoot in {GetType().Name}");
        }
    }
}